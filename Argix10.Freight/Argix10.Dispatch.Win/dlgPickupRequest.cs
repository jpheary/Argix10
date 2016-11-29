using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Argix.Security;

namespace Argix.Freight {
    //
    public partial class dlgPickupRequest:Form {
        //Members
        private DispatchDataset.PickupLogTableRow mRequest=null;
        private bool mIsTemplate = false;
        private bool mMapLoaded = false;
        private int mClientIndex=-2;
        private const int PALLETS_MAX=10, WEIGHT_MAX=10000;


        //Interface
        public dlgPickupRequest(DispatchDataset.PickupLogTableRow request,bool isTemplate = false) {
            try {
                InitializeComponent();
                this.wbAddress.Url = new Uri(global::Argix.Properties.Settings.Default.MapUrl);
                this.mRequest = request;
                if(this.mRequest.IsRequestIDNull()) this.mRequest.RequestID = 0;
                this.mIsTemplate = isTemplate;
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message,ex); }
        }
        private void OnFormLoad(object sender,EventArgs e) {
            //Event handler for form load event
            this.Cursor = Cursors.WaitCursor;
            try {
                //Get lists
                this.bsClients.DataSource = FreightGateway.GetClients();
                this.bsAgents.DataSource = FreightGateway.GetAgents();

                //Load controls
                this.Text = "Pickup Request" + "(" + (this.mRequest.RequestID > 0 ? this.mRequest.RequestID.ToString() : "New") + (this.mIsTemplate ? " Template)" : ")");
                this.lblRequestID.Text = this.mRequest.RequestID > 0 ? this.mRequest.RequestID.ToString("00000000") : "";
                this.dtpScheduleDate.Value = !this.mRequest.IsScheduleDateNull() ? this.mRequest.ScheduleDate : DateTime.Today;

                if(!this.mRequest.IsClientNumberNull()) 
                    this.cboClient.SelectedValue = this.mRequest.ClientNumber;
                else {
                    this.cboClient.SelectedIndex = -1;
                    this.cboClient.Text = !this.mRequest.IsClientNull() ? this.mRequest.Client : "";
                }
                this.txtCaller.Text = !this.mRequest.IsCallerNameNull() ? this.mRequest.CallerName : "";
                
                if(!this.mRequest.IsShipperNumberNull() && this.mRequest.ShipperNumber.Trim().Length > 0) 
                    this.cboShipper.SelectedValue = this.mRequest.ShipperNumber;
                else {
                    this.cboShipper.SelectedIndex = -1;
                    this.cboShipper.Text = !this.mRequest.IsShipperNull() ? this.mRequest.Shipper : "";
                    this.txtShipperAddress.Text = !this.mRequest.IsShipperAddressNull() ? this.mRequest.ShipperAddress : "";
                }
                OnShipperAddressChanged(null,EventArgs.Empty);
                this.txtShipperPhone.Text = !this.mRequest.IsShipperPhoneNull() ? this.mRequest.ShipperPhone : "";
                this.mtbOpen.Text = !this.mRequest.IsWindowOpenNull() ? this.mRequest.WindowOpen.ToString().PadLeft(4,'0') : "";
                this.mtbClose.Text = !this.mRequest.IsWindowCloseNull() ? this.mRequest.WindowClose.ToString().PadLeft(4,'0') : "";

                if (!this.mRequest.IsTerminalNumberNull())
                    this.cboTerminal.SelectedValue = this.mRequest.TerminalNumber.Trim();
                else {
                    this.cboTerminal.SelectedIndex = -1;
                    this.cboTerminal.Text = !this.mRequest.IsTerminalNull() ? this.mRequest.Terminal : "";
                }
                OnTerminalSelectedIndexChanged(null,EventArgs.Empty);
                if (!this.mRequest.IsDriverNameNull())
                    this.cboDriver.Text = this.mRequest.DriverName;
                else
                    this.cboDriver.SelectedIndex = -1;
                this.dtpActual.Checked = !this.mRequest.IsActualPickupNull();
                if (this.dtpActual.Checked) this.dtpActual.Value = this.mRequest.ActualPickup;
                this.dtpActual.Enabled = true;
                this.cboOrderType.SelectedIndex = this.mRequest.OrderType == "B" ? 1 : 0;
                
                this.txtQuantity.Text = !this.mRequest.IsAmountNull() ? this.mRequest.Amount.ToString() : "0";
                if (!this.mRequest.IsAmountTypeNull()) this.cboContainer.Text = this.mRequest.AmountType; else this.cboContainer.SelectedIndex = 0;
                this.cboFreightType.Text = !this.mRequest.IsFreightTypeNull() ? this.mRequest.FreightType : "Tsort";

                this.txtWeight.Text = !this.mRequest.IsWeightNull() ? this.mRequest.Weight.ToString() : "0";
                this.txtComments.Text = !this.mRequest.IsCommentsNull() ? this.mRequest.Comments : "";

                this.cboClient.Focus();
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { OnValidateForm(null,null); this.Cursor = Cursors.Default; }
        }
        private void OnClientSelectedIndexChanged(object sender,EventArgs e) {
            //Event handler for client SelectedIndexChanged event- filter shippers for selected client
            this.Cursor = Cursors.WaitCursor;
            try {
                if(this.cboClient.SelectedIndex != this.mClientIndex) {
                    this.mClientIndex = this.cboClient.SelectedIndex;
                    this.cboShipper.SelectedItem = null;
                    if (this.cboClient.SelectedIndex > -1)
                        this.bsShippers.DataSource = Argix.Terminals.TerminalGateway.GetCustomers2(this.cboClient.SelectedValue.ToString());
                    else
                        this.bsShippers.DataSource = typeof(Argix.Terminals.Customers2);
                }
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { OnValidateForm(null,null); this.Cursor = Cursors.Default; }
        }
        private void OnClientTextChanged(object sender,EventArgs e) {
            //Event handler for client TextChanged event- need to unfilter shippers if user clears client text
            if(this.cboClient.SelectedIndex != this.mClientIndex) OnClientSelectedIndexChanged(null,EventArgs.Empty);
        }
        private void OnShipperTextChanged(object sender,EventArgs e) {
            //Event handler for shipper TextChanged event- clear bound fields when user clears shipper text
            this.Cursor = Cursors.WaitCursor;
            try {
                if (this.cboShipper.SelectedIndex == -1) {
                    this.txtShipperAddress.Text = "";
                    this.lblAcctID.Text = "";
                    this.txtShipperPhone.Text = "";
                    this.mtbOpen.Text = this.mtbClose.Text = "";
                    this.cboTerminal.SelectedItem = null;
                    this.cboTerminal.Text = "";
                }
                else if (this.cboShipper.SelectedValue != null) {
                    string shipperNumber = this.cboShipper.SelectedValue.ToString();
                    this.cboFreightType.Text = shipperNumber.Trim().Length == 14 ? "ISA" : "Tsort";
                }
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { OnValidateForm(null,null); this.Cursor = Cursors.Default; }
        }
        private void OnShipperCurrentChanged(object sender,EventArgs e) {
            //Event handler for change in shipper binding source current item 
            this.Cursor = Cursors.WaitCursor;
            try {
                if (this.bsShippers.Current != null) {
                    //Update open close
                    Argix.Terminals.Customer2 c = (Argix.Terminals.Customer2)this.bsShippers.Current;
                    this.mtbOpen.Text = c != null ? c.WindowOpen.ToString().PadLeft(4,'0') : "0000";
                    this.mtbClose.Text = c != null ? c.WindowClose.ToString().PadLeft(4,'0') : "0000";

                    //Specify a default pickup terminal
                    if (c.TerritoryID.ToString().Length > 0) {
                        if(c.TerritoryID >= 2000 && c.TerritoryID < 3000)
                            this.cboTerminal.SelectedValue = "0001";
                        else if(c.TerritoryID >= 3000 && c.TerritoryID < 4000)
                            this.cboTerminal.SelectedValue = "0044";
                        else if(c.TerritoryID >= 4200 && c.TerritoryID < 4300)
                            this.cboTerminal.SelectedValue = "0001";
                        else if(c.TerritoryID >= 4300 && c.TerritoryID < 4400)
                            this.cboTerminal.SelectedValue = "0044";
                        else if(c.TerritoryID >= 20000 && c.TerritoryID < 30000)
                            this.cboTerminal.SelectedValue = "0001";
                        else {
                            this.cboTerminal.SelectedItem = null;
                            this.cboTerminal.Text = "";
                        }
                    }
                }
                else {
                    this.mtbOpen.Text = this.mtbClose.Text = "0000";
                    this.cboTerminal.SelectedItem = null;
                    this.cboTerminal.Text = "";
                }
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { OnValidateForm(null,null); this.Cursor = Cursors.Default; }
        }
        private void OnShipperAddressChanged(object sender,EventArgs e) {
            //Event handler for shipper address (text) changed event
            try {
                //Display the address in a map
                if(this.mMapLoaded && this.wbAddress.Document != null)
                    this.wbAddress.Document.InvokeScript("MapLocation",new object[] { this.txtShipperAddress.Text });
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnWindowValidating(object sender,CancelEventArgs e) {
            //Event handler for delivery window validating
            try {
                MaskedTextBox mtb = (MaskedTextBox)sender;
                string value = mtb.Text.Replace(":","").Trim();

                if(value == String.Empty) return;

                int hour=-1;
                if(int.TryParse(value.Substring(0,2),out hour) == false) { e.Cancel = true; return; }
                else { if(hour < 0 || hour > 23) { e.Cancel = true; return; } }

                int minute=-1;
                if(int.TryParse(value.Substring(2,2),out minute) == false) { e.Cancel = true; return; }
                else { if(minute < 0 || minute > 59) { e.Cancel = true; return; } }
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnTerminalSelectedIndexChanged(object sender,EventArgs e) {
            //Event handler for terminal SelectedIndexChanged event- filter drivers for selected terminal
            this.Cursor = Cursors.WaitCursor;
            try {
                this.cboDriver.SelectedItem = null;
                this.cboDriver.Text = "";
                if (this.cboTerminal.SelectedIndex > -1) 
                    this.bsDrivers.DataSource = Argix.Terminals.TerminalGateway.GetDrivers(this.cboTerminal.SelectedValue != null ? int.Parse(this.cboTerminal.SelectedValue.ToString()) : 0);
                else
                    this.bsDrivers.DataSource = typeof(Argix.Terminals.Drivers);
                this.cboDriver.SelectedItem = null;
                this.cboDriver.Text = "";
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { OnValidateForm(null,null); this.Cursor = Cursors.Default; }
        }
        private void OnTerminalTextChanged(object sender,EventArgs e) {
            //Event handler for terminal TextChanged event- need to update drivers
            //OnTerminalSelectedIndexChanged(null,EventArgs.Empty);
        }
        private void OnDocumentCompleted(object sender,WebBrowserDocumentCompletedEventArgs e) { this.mMapLoaded = true; OnShipperAddressChanged(null,EventArgs.Empty); }
        private void OnValidateForm(object sender,EventArgs e) {
            //Set user services
            try {
                //Set services
                bool access =   RoleServiceGateway.IsDispatchSupervisor || RoleServiceGateway.IsDispatchClerk || RoleServiceGateway.IsWindowClerk || 
                                RoleServiceGateway.IsClientRep || RoleServiceGateway.IsBBBClerk;

                this.dtpScheduleDate.Enabled = !this.mIsTemplate && (RoleServiceGateway.IsDispatchSupervisor || RoleServiceGateway.IsClientRep);
                this.cboClient.Enabled = true;
                this.txtCaller.Enabled = true;
                this.cboShipper.Enabled = true;
                this.txtShipperAddress.Enabled = true;
                this.txtShipperPhone.Enabled = true;
                this.mtbOpen.Enabled = true;
                this.mtbClose.Enabled = true;
                this.cboTerminal.Enabled = true;
                this.cboDriver.Enabled = true;
                this.dtpActual.Enabled = this.mRequest.RequestID > 0;
                this.cboOrderType.Enabled = true;
                this.txtQuantity.Enabled = true;
                this.cboContainer.Enabled = true;
                this.cboFreightType.Enabled = true;
                this.txtWeight.Enabled = true;
                this.txtComments.Enabled = true;

                bool cancelled = !this.mRequest.IsCancelledNull() && this.mRequest.Cancelled.CompareTo(DateTime.MinValue) > 0;
                this.btnOk.Enabled = (access && !cancelled &&
                                        this.cboClient.Text.Trim().Length > 0 && 
                                        this.cboShipper.Text.Trim().Length > 0 &&
                                        this.txtQuantity.Text.Trim().Length > 0 &&
                                        this.cboContainer.Text.Trim().Length > 0);
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnClick(object sender,EventArgs e) {
            //Event handler for button click
            this.Cursor = Cursors.WaitCursor;
            try {
                Button btn = (Button)sender;
                switch(btn.Name) {
                    case "btnCancel":
                        this.DialogResult = DialogResult.Cancel;
                        this.Close(); 
                        break;
                    case "btnOk": 
                        //Validate servicing terminal
                        int amount = int.TryParse(this.txtQuantity.Text.Trim(), out amount) ? amount : 0;
                        int weight = int.TryParse(this.txtWeight.Text.Trim(), out weight) ? weight : 0;
                        string terminal = this.cboTerminal.Text.Trim();
                        if (((this.cboContainer.Text.ToLower() == "pallets" && amount > PALLETS_MAX) || weight > WEIGHT_MAX) && terminal.ToLower() != "argix logistics national")
                            MessageBox.Show(this,"This order may exceed capacity restrictions of a Straight truck (i.e. >" + PALLETS_MAX + " pallets or >" + WEIGHT_MAX + "lbs); please confirm serviceability with the local delivery facility?",App.Product,MessageBoxButtons.OK,MessageBoxIcon.Warning);

                        if(this.mRequest.RequestID == 0) {
                             this.mRequest.Created = DateTime.Now;
                             this.mRequest.CreateUserID = Environment.UserName;
                         }

                         this.mRequest.ScheduleDate = this.dtpScheduleDate.Value;
                        if(this.cboClient.SelectedValue != null)
                            this.mRequest.ClientNumber = this.cboClient.SelectedValue.ToString();
                        else
                            this.mRequest.SetClientNumberNull();
                        this.mRequest.Client = this.cboClient.Text;
                        this.mRequest.CallerName = this.txtCaller.Text;

                        if(this.cboShipper.SelectedValue != null)
                            this.mRequest.ShipperNumber = this.cboShipper.SelectedValue.ToString();
                        else
                            this.mRequest.SetShipperNumberNull();
                        this.mRequest.Shipper = this.cboShipper.Text;
                        this.mRequest.ShipperAddress = this.txtShipperAddress.Text;
                        if(this.txtShipperPhone.Text.Length > 0) this.mRequest.ShipperPhone = this.txtShipperPhone.Text;
                        this.mRequest.WindowOpen = this.mtbOpen.Text.Length > 0 ? short.Parse(this.mtbOpen.Text) : (short)0;
                        this.mRequest.WindowClose = this.mtbClose.Text.Length > 0 ? short.Parse(this.mtbClose.Text) : (short)0;
                        
                        if(this.cboTerminal.SelectedValue != null) 
                            this.mRequest.TerminalNumber = this.cboTerminal.SelectedValue.ToString().PadLeft(4,'0');
                        else
                            this.mRequest.SetTerminalNumberNull();
                        this.mRequest.Terminal = this.cboTerminal.Text.Trim();
                        this.mRequest.DriverName = this.cboDriver.Text;
                        this.mRequest.ActualPickup = this.dtpActual.Checked ? this.dtpActual.Value : DateTime.MinValue;
                        this.mRequest.OrderType = this.cboOrderType.Text == "Backhaul" ? "B" : "R";

                        this.mRequest.Amount = int.Parse(this.txtQuantity.Text);
                        this.mRequest.AmountType = this.cboContainer.Text;
                        this.mRequest.Weight = this.txtWeight.Text.Trim().Length > 0 ? int.Parse(this.txtWeight.Text) : 0;
                        this.mRequest.FreightType = !this.mRequest.IsShipperNumberNull() ? (this.mRequest.ShipperNumber.Length == 14 ? "ISA" : this.cboFreightType.Text) : this.cboFreightType.Text;
                        this.mRequest.Comments = this.txtComments.Text;
                        if (this.mIsTemplate) this.mRequest.IsTemplate = true;

                        this.mRequest.LastUpdated = DateTime.Now;
                        this.mRequest.UserID = Environment.UserName;

                        this.DialogResult = DialogResult.OK;
                        this.Close(); 
                        break;
                }
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
    }
}
