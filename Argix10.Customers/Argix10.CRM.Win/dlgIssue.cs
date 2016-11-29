using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Word=Microsoft.Office.Interop.Word;

namespace Argix.Customers {
    //
    public partial class dlgIssue :Form {
        //Members
        private Issue mIssue = null;
        private System.Windows.Forms.ToolTip mToolTip = null;

        private const string SCOPE_NONE = "", SCOPE_DISTRICTS = "Districts", SCOPE_REGIONS = "Regions";
        private const string SCOPE_AGENTS = "Agents", SCOPE_STORES = "Stores", SCOPE_SUBSTORES = "Substores";

        //Interface
        public dlgIssue(Issue issue) {
            //Constructor
            try {
                InitializeComponent();
                this.mIssue = issue;
                this.Text = this.mIssue.ID == 0 ? "Issue (New)" : "Issue: " + this.mIssue.Subject;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new dlgIssue instance.",ex); }
        }
        private void OnFormLoad(object sender,EventArgs e) {
            //Event handler for form load event
            this.Cursor = Cursors.WaitCursor;
            try {
                this.mToolTip = new System.Windows.Forms.ToolTip();
                this.mToolTip.ShowAlways = true;
                this.mToolTip.SetToolTip(this.cboIssueType,"Select an issue type.");

                this.mCompanyDS.Clear(); this.mCompanyDS.Merge(CRMGateway.GetCompanies());
                this.mIssueCategorys.Clear(); this.mIssueCategorys.Merge(CRMGateway.GetIssueCategories());
                this.mActionsDS.Clear(); this.mActionsDS.Merge(CRMGateway.GetActionTypes(this.mIssue.ID));
                
                if(this.mIssue.ID == 0 && this.mIssue.CompanyID == -1)
                    this.cboCompany.SelectedIndex = -1;
                else 
                    this.cboCompany.SelectedValue = this.mIssue.CompanyID;
                OnCompanySelected(null,EventArgs.Empty);
                if (this.mIssue.DistrictNumber != null) {
                    this.cboScope.SelectedItem = SCOPE_DISTRICTS; OnScopeChanged(null,EventArgs.Empty);
                    this.cboLocation.SelectedValue = this.mIssue.DistrictNumber;
                }
                else if (this.mIssue.RegionNumber != null) {
                    this.cboScope.SelectedItem = SCOPE_REGIONS; OnScopeChanged(null,EventArgs.Empty);
                    this.cboLocation.SelectedValue = this.mIssue.RegionNumber;
                }
                else if (this.mIssue.AgentNumber != null) {
                    this.cboScope.SelectedItem = SCOPE_AGENTS; OnScopeChanged(null,EventArgs.Empty);
                    this.cboLocation.SelectedValue = this.mIssue.AgentNumber;
                }
                else if (this.mIssue.StoreNumber > -1) {
                    this.cboScope.SelectedItem = SCOPE_STORES; OnScopeChanged(null,EventArgs.Empty);
                    this.txtStore.Text = this.mIssue.StoreNumber.ToString();
                    showStoreDetail();
                }
                else
                    this.cboLocation.DataSource = null;
                this.cboIssueCategory.SelectedIndex = -1;
                OnIssueCategorySelected(this.cboIssueCategory,EventArgs.Empty);
                if(this.mIssue.TypeID > -1) this.cboIssueType.SelectedValue = this.mIssue.TypeID;
                this.txtContact.Text = this.mIssue.Contact;
                this.txtSubject.Text = this.mIssue.Subject;

                this.cboActionType.SelectedIndex = -1;  // this.mIssue.ID == 0 ? 0 : -1;
                this.txtComments.Text = getAllActionComments();
                this.chkShowAll.Checked = this.mIssue.ID > 0;
                OnShowAllCheckedChanged(null,EventArgs.Empty);
                
                this.cboCompany.Enabled = this.mIssue.ID == 0 && this.cboCompany.Items.Count > 0;
                this.cboScope.Enabled = this.mIssue.ID == 0;
                this.cboLocation.Enabled = this.txtStore.Enabled = this.mIssue.ID == 0;
                this.cboIssueCategory.Enabled = this.cboIssueType.Enabled = this.mIssue.ID == 0;
                this.txtContact.Enabled = this.mIssue.ID == 0; 
                this.txtSubject.Enabled = this.mIssue.ID == 0;
                this.chkShowAll.Enabled = this.mIssue.ID > 0 && this.mIssue.Actions.Count > 0;
            }
            catch(Exception ex) { App.ReportError(ex); }
            finally { OnValidateForm(null,EventArgs.Empty); this.Cursor = Cursors.Default; }
        }
        private void OnCompanySelected(object sender,EventArgs e) {
            //Event handler for change in comapny
            try {
                //Validate
                if(this.cboCompany.SelectedValue == null) return;

                //Set applicable scopes for companies (i.e. client, pre-paid vendor)
                this.cboScope.Items.Clear();
                this.cboScope.Items.AddRange(new object[] { SCOPE_AGENTS });
                CRMDataset.CompanyTableRow[] rows = (CRMDataset.CompanyTableRow[])this.mCompanyDS.CompanyTable.Select("CompanyID=" + this.cboCompany.SelectedValue.ToString());
                if(rows.Length > 0 && rows[0].CompanyType == "20") 
                    this.cboScope.Items.AddRange(new object[] { SCOPE_DISTRICTS,SCOPE_REGIONS,SCOPE_STORES,SCOPE_SUBSTORES });
                OnScopeChanged(null,EventArgs.Empty);
            }
            catch(Exception ex) { App.ReportError(new ApplicationException("Unexpected error when company selected.",ex)); }
        }
        private void OnScopeChanged(object sender,EventArgs e) {
            //Event handler for change in scope
            try {
                //Prepare a location selector for the specified scope
                string scope = this.cboScope.SelectedItem != null ? this.cboScope.SelectedItem.ToString() : SCOPE_NONE;
                this.cboLocation.Visible = (scope == SCOPE_NONE || scope == SCOPE_DISTRICTS || scope == SCOPE_REGIONS || scope == SCOPE_AGENTS);
                this.txtStore.Visible = (scope == SCOPE_STORES || scope == SCOPE_SUBSTORES);

                this.cboLocation.DataSource = null;
                CRMDataset locationDS = new CRMDataset();
                CRMDataset ds = null;
                int companyID = int.Parse(this.cboCompany.SelectedValue.ToString());
                string number = this.cboCompany.SelectedValue.ToString().PadLeft(8,'0').Substring(5,3);
                switch(scope) {
                    case SCOPE_DISTRICTS:
                        ds = CRMGateway.GetDistricts(number);
                        this.cboLocation.DisplayMember = "LocationTable.LocationName";
                        this.cboLocation.ValueMember = "LocationTable.Location";
                        break;
                    case SCOPE_REGIONS:
                        ds = CRMGateway.GetRegions(number);
                        this.cboLocation.DisplayMember = "LocationTable.LocationName";
                        this.cboLocation.ValueMember = "LocationTable.Location";
                        break;
                    case SCOPE_STORES:
                    case SCOPE_SUBSTORES:
                        this.txtStore.Text = "";
                        break;
                    case SCOPE_AGENTS: 
                        ds = CRMGateway.GetAgents(number);
                        this.cboLocation.DisplayMember = "AgentTable.AgentSummary";
                        this.cboLocation.ValueMember = "AgentTable.AgentNumber";
                        break;
                }
                if(ds != null) {
                    locationDS.Merge(ds);
                    this.cboLocation.DataSource = locationDS;
                }
                this.cboLocation.Enabled = this.cboLocation.Items.Count > 0;
                if(this.cboLocation.Items.Count > 0) this.cboLocation.SelectedIndex = 0;
                OnCompanyLocationChanged(null,EventArgs.Empty);
            }
            catch(Exception ex) { App.ReportError(new ApplicationException("Unexpected error when location scope changed.",ex)); }
        }
        private void OnCompanyLocationChanged(object sender,EventArgs e) {
            //Event handler for change in location (i.e. cboLocation.SelectionChangeCommitted, txtStore.TextChanged)
            try {
                this.txtStoreDetail.Clear();
                string scope = this.cboScope.SelectedItem != null ? this.cboScope.SelectedItem.ToString() : SCOPE_NONE;
                switch(scope) {
                    case SCOPE_DISTRICTS:
                    case SCOPE_REGIONS:
                    case SCOPE_AGENTS:
                        break;
                    case SCOPE_STORES:
                    case SCOPE_SUBSTORES:
                        break;
                }
            }
            catch(Exception ex) { App.ReportError(new ApplicationException("Unexpected error when company location changed.",ex)); }
        }
        private void OnStoreKeyUp(object sender,KeyEventArgs e) {
            //Event handler for store textbox key up event
            try {
                if(e.KeyCode == Keys.Enter) {
                    switch(this.cboScope.SelectedItem.ToString()) {
                        case SCOPE_DISTRICTS:
                        case SCOPE_REGIONS:
                        case SCOPE_AGENTS:
                            break;
                        case SCOPE_STORES:
                        case SCOPE_SUBSTORES:
                            showStoreDetail();
                            break;
                    }
                }
            }
            catch(Exception ex) { App.ReportError(new ApplicationException("Unexpected error when company location changed.",ex)); }
        }
        private void OnIssueCategorySelected(object sender,EventArgs e) {
            //Event handler for change in issue category
            this.mIssueTypes.Clear();
            string issueCategory = this.cboIssueCategory.SelectedValue != null ? this.cboIssueCategory.SelectedValue.ToString() : "";
            this.mIssueTypes.Merge(CRMGateway.GetIssueTypes(issueCategory));
            this.cboIssueType.SelectedIndex = -1;
            OnIssueTypeSelected(null,EventArgs.Empty);
        }
        private void OnIssueTypeSelected(object sender,EventArgs e) { OnValidateForm(null,EventArgs.Empty); }
        private void OnLocationChanged(object sender,EventArgs e) { OnValidateForm(null,EventArgs.Empty); }
        private void OnActionTypeSelected(object sender,EventArgs e) {
            //Event handler for change in selected action type
            OnValidateForm(null,EventArgs.Empty);
        }
        private void OnShowAllCheckedChanged(object sender,EventArgs e) {
            //Event handler for running checked
            this.txtComments.Visible = this.chkShowAll.Checked;
        }
        private void OnValidateForm(object sender,EventArgs e) {
            //Event handler for control value changes
            try {
                this.btnSpellCheck.Enabled = this.txtComment.Text.Length > 0;
                this.lblSpellCheck.Text = "";
                
                string scope = this.cboScope.SelectedItem != null ? this.cboScope.SelectedItem.ToString() : SCOPE_NONE;
                string locationNumber = "";
                switch(scope) {
                    case SCOPE_DISTRICTS: case SCOPE_REGIONS: case SCOPE_AGENTS:
                        locationNumber = (this.cboLocation.SelectedValue != null ? this.cboLocation.SelectedValue.ToString() : "");
                        break;
                    case SCOPE_STORES: case SCOPE_SUBSTORES:
                        locationNumber = this.txtStore.Text.Trim();
                        break;
                }
                bool locationValid = ((scope == SCOPE_AGENTS || scope == SCOPE_DISTRICTS || scope == SCOPE_REGIONS) && locationNumber.Length > 0) || 
                                     ((scope == SCOPE_STORES || scope == SCOPE_SUBSTORES) && this.txtStoreDetail.Text.Length > 0);
                this.btnOk.Enabled = (locationValid && this.cboIssueType.Text.Length > 0 && this.txtSubject.Text.Length > 0 && this.cboActionType.Text.Length > 0);
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnCmdClick(object sender,System.EventArgs e) {
            //Command button handler
            try {
                Button btn = (Button)sender;
                switch(btn.Name) {
                    case "btnCancel":
                        //Close the dialog
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                        break;
                    case "btnOk":
                        //Create new issue
                        this.Cursor = Cursors.WaitCursor;
                        this.DialogResult = DialogResult.OK;
                        this.mIssue.CompanyID = int.Parse(this.cboCompany.SelectedValue.ToString());
                        string scope = this.cboScope.SelectedItem != null ? this.cboScope.SelectedItem.ToString() : SCOPE_NONE;
                        string locationNumber = "";
                        switch(scope) {
                            case SCOPE_DISTRICTS: case SCOPE_REGIONS: case SCOPE_AGENTS:
                                locationNumber = (this.cboLocation.SelectedValue != null ? this.cboLocation.SelectedValue.ToString() : "");
                                break;
                            case SCOPE_STORES: case SCOPE_SUBSTORES:
                                locationNumber = this.txtStore.Text.Trim();
                                break;
                        }
                        switch(scope) {
                            case SCOPE_AGENTS:
                                this.mIssue.AgentNumber = (locationNumber!="All" ? locationNumber : "");
                                break;
                            case SCOPE_DISTRICTS:
                                this.mIssue.DistrictNumber = (locationNumber != "All" ? locationNumber : "");
                                break;
                            case SCOPE_REGIONS:
                                this.mIssue.RegionNumber = (locationNumber != "All" ? locationNumber : "");
                                break;
                            case SCOPE_STORES: case SCOPE_SUBSTORES:
                                this.mIssue.StoreNumber = Convert.ToInt32(locationNumber);
                                break;
                        }
                        this.mIssue.Contact = this.txtContact.Text;
                        this.mIssue.TypeID = (int)this.cboIssueType.SelectedValue;
                        this.mIssue.Subject = this.txtSubject.Text;
                        Action action = new Action();
                        action.IssueID = this.mIssue.ID;
                        action.TypeID = byte.Parse(this.cboActionType.SelectedValue.ToString());
                        action.UserID = Environment.UserName;
                        action.Created = DateTime.Now;
                        action.Comment = this.txtComment.Text;
                        this.mIssue.Actions.Add(action);
                        this.Close();
                        break;
                    case "btnSpellCheck":
                        spellCheck(this.txtComment,this.lblSpellCheck);
                        break;
                    default: break;
                }
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        #region Local Services: showStoreDetail(), getStoreDetailString(),  getAllActionComments(), spellCheck()
        private void showStoreDetail() {
            //
            this.txtStoreDetail.Clear();
            if(this.cboCompany.SelectedValue != null && this.txtStore.Text.Length > 0) {
                CRMDataset ds = new CRMDataset();
                switch(this.cboScope.SelectedItem.ToString()) {
                    case SCOPE_STORES:
                        ds.Merge(CRMGateway.GetStoreDetail(int.Parse(this.cboCompany.SelectedValue.ToString()),int.Parse(this.txtStore.Text))); 
                        break;
                    case SCOPE_SUBSTORES:
                        ds.Merge(CRMGateway.GetStoreDetail(int.Parse(this.cboCompany.SelectedValue.ToString()),this.txtStore.Text)); 
                        break;
                }
                if(ds.StoreTable.Rows.Count > 0) this.txtStoreDetail.Text = getStoreDetailString(ds.StoreTable[0]);
            }
        }
        private string getStoreDetailString(CRMDataset.StoreTableRow store) {
            //Return a string of store detail
            StringBuilder detail = new StringBuilder();
            detail.AppendLine(store.StoreName.Trim() + " (store #" + store.StoreNumber.ToString() + "; substore #" + store.SubStoreNumber.Trim() + ")");
            detail.AppendLine((!store.IsStoreAddressline1Null() ? store.StoreAddressline1.Trim() : ""));
            detail.AppendLine((!store.IsStoreAddressline2Null() ? store.StoreAddressline2.Trim() : ""));
            detail.AppendLine((!store.IsStoreCityNull() ? store.StoreCity.Trim() : "") + ", " + 
                                            (!store.IsStoreStateNull() ? store.StoreState.Trim() : "") + " " +
                                            (!store.IsStoreZipNull() ? store.StoreZip.Trim() : ""));
            detail.AppendLine((!store.IsContactNameNull() ? store.ContactName.Trim() : "") + ", " + (!store.IsPhoneNumberNull() ? store.PhoneNumber.Trim() : ""));
            detail.AppendLine((!store.IsRegionDescriptionNull() ? store.RegionDescription.Trim() : "") + 
                                            " (" + (!store.IsRegionNull() ? store.Region.Trim() : "") + "), " + 
                                            (!store.IsDistrictNameNull() ? store.DistrictName.Trim() : "") + 
                                            " (" + (!store.IsDistrictNull() ? store.District.Trim() : "") + ")");
            detail.AppendLine("Zone " + (!store.IsZoneNull() ? store.Zone.Trim() : "") + ", " +
                                            "Agent " + (!store.IsAgentNumberNull() ? store.AgentNumber.Trim() : "") + " " +
                                            (!store.IsAgentNameNull() ? store.AgentName.Trim() : ""));
            detail.AppendLine("Window " + (!store.IsWindowTimeStartNull() ? store.WindowTimeStart.ToString("HH:mm") : "") + " - " +
                                            (!store.IsWindowTimeEndNull() ? store.WindowTimeEnd.ToString("HH:mm") : "") + ", " +
                                            "Del Days " + getDeliveryDays(store) + ", " +
                                            (!store.IsScanStatusDescrptionNull() ? store.ScanStatusDescrption.Trim() : ""));
            detail.AppendLine("JA Transit " + (!store.IsStandardTransitNull() ? store.StandardTransit.ToString() : "") + ", " + "OFD " + getOFD(store));
            //detail.AppendLine("Special Inst: " + (!store.IsSpecialInstructionsNull() ? store.SpecialInstructions.Trim() : ""));
            return detail.ToString();
        }
        private string getDeliveryDays(CRMDataset.StoreTableRow store) {
            //Return delivery days from the dataset
            string ddays = "";

            //Check for overrides
            if (!store.IsIsDeliveryDayMondayNull()) ddays += (store.IsDeliveryDayMonday.Trim() == "Y" ? "M" : "");
            if (!store.IsIsDeliveryDayTuesdayNull()) ddays += (store.IsDeliveryDayTuesday.Trim() == "Y" ? "T" : "");
            if (!store.IsIsDeliveryDayWednesdayNull()) ddays += (store.IsDeliveryDayWednesday.Trim() == "Y" ? "W" : "");
            if (!store.IsIsDeliveryDayThursdayNull()) ddays += (store.IsDeliveryDayThursday.Trim() == "Y" ? "R" : "");
            if (!store.IsIsDeliveryDayFridayNull()) ddays += (store.IsDeliveryDayFriday.Trim() == "Y" ? "F" : "");

            //If no overrides, then all days are valid
            if (ddays.Trim().Length == 0) ddays = "MTWRF";
            return ddays;
        }
        private string getOFD(CRMDataset.StoreTableRow store) {
            //Return delivery days from the dataset
            string ofd = "";

            //OFD1, OFD2, or NONE
            if (!store.IsIsOutforDeliveryDay1Null())
                ofd += (store.IsOutforDeliveryDay1.Trim() == "Y" ? "DAY1" : "");
            else if (!store.IsIsOutforDeliveryDay2Null())
                ofd += (store.IsOutforDeliveryDay2.Trim() == "Y" ? "DAY2" : "");
            else
                ofd += "";
            return ofd;
        }
        private string getAllActionComments() {
            //Get a running comment for this action
            string comments="";
            Actions actions = this.mIssue.Actions;
            if(actions != null) {
                for(int i = 0;i < actions.Count;i++) {
                    Action action = actions[i];
                    if(i > 0) {
                        comments += "\r\n\r\n";
                        comments += "".PadRight(75,'-');
                        comments += "\r\n";
                    }
                    comments += action.Created.ToString("f") + ", " + action.UserID + ", " + action.TypeName;
                    comments += "\r\n\r\n";
                    comments += action.Comment;
                }
            }
            return comments;
        }
        private void spellCheck(TextBox txt,Label lbl) {
            //Spell check
            if(txt.Text.Length > 0) {
                //Create an instance of MS Word application
                Word.Application app = new Word.Application();
                app.Visible = false;

                //Setup spell checker with text to check and count errors
                //Setting these variables is comparable to passing null to the function
                //This is necessary because the C# null cannot be passed by reference
                object temp=Missing.Value,newTemp=Missing.Value,docType=Missing.Value,vis=true;
                Word.Document doc = app.Documents.Add(ref temp,ref newTemp,ref docType,ref vis);
                doc.Words.First.InsertBefore(txt.Text);
                int errCount = doc.SpellingErrors.Count;

                //Run the checker
                object opt = Missing.Value;
                doc.CheckSpelling(ref opt,ref opt,ref opt,ref opt,ref opt,ref opt,ref opt,ref opt,ref opt,ref opt,ref opt,ref opt);

                //Update original text
                object first = 0,last = doc.Characters.Count - 1;
                txt.Text = doc.Range(ref first,ref last).Text;
                txt.Text = txt.Text.Replace("\r","\r\n");
                lbl.Text = "Spelling OK. " + errCount + " error(s) corrected ";

                //Close MS Word application
                object save = false,format = Missing.Value,rtDoc = Missing.Value;
                app.Quit(ref save,ref format,ref rtDoc);
            }
        }
        #endregion
    }
}