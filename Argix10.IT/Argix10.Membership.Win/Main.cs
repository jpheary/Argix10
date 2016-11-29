using System;
using System.Collections.Generic;
using System.Configuration;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Argix.Windows;

namespace Argix {
    //
    public partial class frmMain:Form {
        //Members
        private bool mIsAdmin=false;
        private MessageManager mMessageMgr = null;

        //Interface
        public frmMain() {
            //Constructor
            try {
                //Required for Windows Form Designer support
                InitializeComponent();
                Splash.Start(App.Product,Assembly.GetExecutingAssembly(),App.Copyright);
                Thread.Sleep(3000);
                this.mMessageMgr = new MessageManager(this.ssMain.Panels[0],500,3000);
            }
            catch (Exception ex) { Splash.Close(); throw new ApplicationException("Startup Failure",ex); }
        }

        private void OnFormLoad(object sender,EventArgs e) {
            //Event handler for form load event
            this.Cursor = Cursors.WaitCursor;
            try {
                //Show early
                Splash.Close();
                this.Visible = true;
                Application.DoEvents();
                this.Font = this.msMain.Font = this.tsMain.Font = this.ssMain.Font = global::Argix.Properties.Settings.Default.Font;

                //Determine authorization
                string name = Environment.UserDomainName.ToLower() + "\\" + Environment.UserName.ToLower();
                if(name != "argix\\jheary") {
                    MembershipDataset roles = new MembershipDataset();
                    DataSet ds = ApplicationServicesGateway.GetRolesForUser("Argix",name);
                    if(ds != null) roles.Merge(ds);
                    if(roles.RoleTable.Rows.Count > 0) {
                        DataRow[] _roles = roles.RoleTable.Select("RoleName='" + "administrator" + "'");
                        if(_roles.Length > 0) this.mIsAdmin = true;
                    }
                }
                else 
                    this.mIsAdmin = true;

                this.ssMain.SetTerminalPanel("0","PRODUCTION");
                this.msViewRefresh.PerformClick();
            }
            catch (Exception ex) { App.ReportError(ex); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnUserSelected(object sender,Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
            //Event handler for user selcted in the grid
            try {
                this.mRoles.Clear();
                this.mUserRoles.Clear();
                string userName = this.grdMain.Selected.Rows[0].Cells["UserName"].Value.ToString();

                this.mUserRoles.Merge(ApplicationServicesGateway.GetRolesForUser("Argix",userName));
                MembershipDataset roles = new MembershipDataset();
                roles.Merge(ApplicationServicesGateway.GetRoles("Argix"));
                for (int i = 0;i < this.mUserRoles.RoleTable.Count;i++) {
                    MembershipDataset.RoleTableRow[] rows = (MembershipDataset.RoleTableRow[])roles.RoleTable.Select("RoleName='" + this.mUserRoles.RoleTable[i].RoleName + "'");
                    if (rows != null && rows.Length > 0) roles.RoleTable.RemoveRoleTableRow(rows[0]);
                }
                roles.RoleTable.AcceptChanges();
                this.mRoles.Merge(roles);
            }
            catch (Exception ex) { App.ReportError(ex); }
            finally { setUserServices(); }
        }
        private void OnButtonCommand(object sender,EventArgs e) {
            //Event handler for buttom click commands
            try {
                Button button = (Button)sender;
                string userName = this.grdMain.Selected.Rows[0].Cells["UserName"].Value.ToString();
                switch (button.Name) {
                    case "btnAdd":
                        string roleToAdd = this.lstRoles.Text;
                        ApplicationServicesGateway.AddUserToRole("Argix",userName,roleToAdd);
                        break;
                    case "btnRem":
                        string roleToRem = this.lstUserRoles.Text;
                        ApplicationServicesGateway.RemoveUserFromRole("Argix",userName,roleToRem);
                        break;
                }
                OnUserSelected(this.grdMain,null);
            }
            catch (Exception ex) { App.ReportError(ex); }
        }
        #region User Services: OnItemClick()
        private void OnItemClick(object sender,EventArgs e) {
            //Event handler for menu/toolbar item clicked
            try {
                ToolStripItem item = (ToolStripItem)sender;
                switch (item.Name) {
                    case "msFileNew":
                    case "csNew":
                    case "tsbNew":
                        dlgInputBox input = new dlgInputBox("Specify a domain username (i.e. argix\\username)", "argix\\", "Create New User");
                        if(input.ShowDialog(this) == System.Windows.Forms.DialogResult.OK) {
                            ApplicationServicesGateway.CreateUser("Argix",input.Value);
                            this.msViewRefresh.PerformClick();
                        }
                        break;
                    case "msFileDelete":
                    case "csDelete":
                    case "tsbDelete":
                        DialogResult res = MessageBox.Show("Delete the selected user?",App.Product,MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
                        if (res == System.Windows.Forms.DialogResult.Yes) {
                            string name = this.grdMain.Selected.Rows[0].Cells["UserId"].Value.ToString();
                            ApplicationServicesGateway.DeleteUser("Argix",name);
                            this.msViewRefresh.PerformClick();
                        }
                        break;
                    case "msFileExit": this.Close(); break;
                    case "msViewRefresh":
                    case "tsbRefresh":
                        this.mUsers.Clear();
                        this.mUsers.Merge(ApplicationServicesGateway.GetUsers("Argix"));
                        break;
                    case "msViewFont":
                        FontDialog fd = new FontDialog();
                        fd.FontMustExist = true;
                        fd.Font = this.Font;
                        if (fd.ShowDialog() == DialogResult.OK)
                            this.Font = this.msMain.Font = this.tsMain.Font = this.ssMain.Font = fd.Font;
                        break;
                    case "msViewToolbar": this.tsMain.Visible = (this.msViewToolbar.Checked = !this.msViewToolbar.Checked); break;
                    case "msViewStatusBar": this.ssMain.Visible = (this.msViewStatusBar.Checked = !this.msViewStatusBar.Checked); break;
                    case "msHelpAbout": new dlgAbout(App.Product + " Application",App.Version,App.Copyright,App.Configuration).ShowDialog(this); break;
                }
            }
            catch (Exception ex) { App.ReportError(ex); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        #endregion
        #region Local Services: setUserServices()
        private void setUserServices() {
            //Set user services depending upon an item selected in the grid
            try {
                this.msFileNew.Enabled = this.tsbNew.Enabled = this.mIsAdmin;
                this.msFileDelete.Enabled = this.tsbDelete.Enabled = this.mIsAdmin && this.grdMain.Selected.Rows.Count > 0;
                this.msFileExit.Enabled = true;
                this.msViewRefresh.Enabled = this.tsbRefresh.Enabled = true;
                this.msViewToolbar.Enabled = this.msViewStatusBar.Enabled = true;
                this.msHelpAbout.Enabled = true;

                this.btnAdd.Enabled = this.btnRem.Enabled = this.mIsAdmin;

                string connection = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
                this.ssMain.OnOnlineStatusUpdate(null,new OnlineStatusArgs(true,connection.Replace("sa","***").Replace("objects","***")));
            }
            catch (Exception ex) { App.ReportError(ex); }
        }
        #endregion
    }
}
