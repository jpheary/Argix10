//	File:	DriverComp.cs
//	Author:	jheary
//	Date:	07/18/13
//	Desc:	Manages driver compensation for a single agent (local terminal) for a 
//          period of time (i.e. 7 days) for all operators.
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Argix.Finance {
	//
	public class CompensationAgent {
		//Members
        private string mAgentNumber = "", mAgentName = "";
        private DateTime mBeginDate = DateTime.Today,mEndDate = DateTime.Today.AddDays(1).AddMinutes(-1);
        private DriverCompDataset mCompensation = null, mRoutes = null;
        private TerminalConfiguration mTerminalConfig=null;
        private AgentRates mRates = null;
        private decimal mFuelCost = 0.0M;

        public event EventHandler Changed = null;
        public event EventHandler RoutesChanged = null;

		//Interface
        public CompensationAgent(string agentNumber,string agentName,DateTime startDate,DateTime endDate) {
            //Constructor
            try {
                this.mAgentNumber = agentNumber;
                this.mAgentName = agentName;
                this.mBeginDate = startDate;
                this.mEndDate = endDate;
                this.mCompensation = new DriverCompDataset();
                this.mRoutes = new DriverCompDataset();
                this.mRates = new AgentRates(this.mAgentNumber,this.mAgentName,this.mEndDate);
                this.mTerminalConfig = FinanceGateway.GetTerminalConfiguration(this.mAgentNumber);
                this.mFuelCost = FinanceGateway.GetFuelCost(this.mEndDate,this.mAgentNumber);
                ViewCompensation();
                ImportRoutes();
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException(ex.Message,ex); }
        }
        #region Accessors\Modifiers: [Members...]
        public string AgentNumber { get { return this.mAgentNumber; } }
        public string AgentName { get { return this.mAgentName; } }
        public DateTime BeginDate { get { return this.mBeginDate; } }
        public DateTime EndDate { get { return this.mEndDate; } }
        public DriverCompDataset Compensation { get { return this.mCompensation; } }
        public DriverCompDataset Routes { get { return this.mRoutes; } }
        public AgentRates Rates { get { return this.mRates; } }
        public bool IsDirty { get { return this.mCompensation.HasChanges(); } }
        public string Title { get { return this.mAgentName.ToUpper().Trim() + " DRIVERS " + this.mBeginDate.ToString("MMddyy") + "-" + this.mEndDate.ToString("MMddyy"); } }
        #endregion
        public void ImportRoutes() {
            //Get a list of all Roadshow routes for this terminal and date range
            try {
                this.mRoutes.Clear();
                this.mRoutes.Merge(FinanceGateway.ImportRoutes(this.mAgentNumber, this.mBeginDate, this.mEndDate));

                //Check-off new routes (those that don't exist in the driver's compensation)
                for(int i = 0; i < this.mRoutes.RoadshowRouteTable.Rows.Count; i++) {
                    int termID = this.mRoutes.RoadshowRouteTable[i].DepotNumber;
                    string oper = this.mRoutes.RoadshowRouteTable[i].Operator;
                    string routeName = this.mRoutes.RoadshowRouteTable[i].Rt_Name;
                    DateTime routeDate = this.mRoutes.RoadshowRouteTable[i].Rt_Date;
                    DriverCompDataset.DriverRouteTableRow[] driverRoutes = (DriverCompDataset.DriverRouteTableRow[])this.mCompensation.DriverRouteTable.Select("AgentNumber=" + termID + " AND Operator='" + oper + "' AND RouteName='" + routeName + "' AND RouteDate='" + routeDate + "'");
                    this.mRoutes.RoadshowRouteTable[i].New = (driverRoutes.Length == 0);
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            finally { if(this.RoutesChanged != null) this.RoutesChanged(this, EventArgs.Empty); }
        }
        public void ViewCompensation() {
            //View all rated routes and the associated driver compensation
            try {
                //Read all rated routes for specified terminal and date range
                this.mCompensation.Clear();
                this.mCompensation.Merge(FinanceGateway.ReadDriverRoutes(this.mAgentNumber, this.mBeginDate, this.mEndDate));
                for(int i = 0; i < this.mCompensation.DriverRouteTable.Rows.Count; i++) {
                    //Build driver compensation for each driver
                    DriverCompDataset.DriverRouteTableRow driverRoute = (DriverCompDataset.DriverRouteTableRow)this.mCompensation.DriverRouteTable.Rows[i];
                    if(this.mCompensation.DriverCompTable.Select("Operator='" + driverRoute.Operator + "'").Length == 0) {
                        //Create driver compensation for driverRoute.Operator and initialize
                        DriverCompDataset.DriverCompTableRow driverComp = this.mCompensation.DriverCompTable.NewDriverCompTableRow();
                        #region Set members
                        driverComp.Select = driverRoute.IsExportedNull();
                        driverComp.IsNew = driverComp.IsCombo = driverComp.IsAdjust = false;
                        driverComp.AgentNumber = driverRoute.AgentNumber;
                        driverComp.FinanceVendorID = driverRoute.FinanceVendorID;
                        driverComp.FinanceVendor = driverRoute.Payee;
                        driverComp.Operator = driverRoute.Operator;
                        //driverComp.EquipmentTypeID = driverRoute.EquipmentTypeID;
                        driverComp.Miles = driverComp.Trip = driverComp.Stops = driverComp.Cartons = driverComp.Pallets = driverComp.PickupCartons = 0;
                        driverComp.MilesAmount = driverComp.DayAmount = driverComp.TripAmount = driverComp.StopsAmount = driverComp.CartonsAmount = driverComp.PalletsAmount = driverComp.PickupCartonsAmount = driverComp.Amount = 0.0M;
                        driverComp.FSCMiles = 0;
                        driverComp.FuelCost = driverComp.FSCGal = driverComp.FSCBaseRate = driverComp.FSC = 0.0M;
                        driverComp.MinimunAmount = driverComp.AdminCharge = driverComp.AdjustmentAmount1 = driverComp.AdjustmentAmount2 = driverComp.TotalAmount = 0.0M;
                        #endregion
                        this.mCompensation.DriverCompTable.AddDriverCompTableRow(driverComp);

                        //Calculate driver compensation for driverRoute.Operator for all rated routes
                        CalculateCompensation(driverRoute.Operator, false);
                    }
                }
                this.mCompensation.AcceptChanges();
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException(ex.Message,ex); }
            finally { if(this.Changed != null) this.Changed(this,EventArgs.Empty); }
        }
        public void CreateCompensation() {
            //Add all new (selected) imported routes to the driver compensation
            try {
                for(int i = 0; i < this.mRoutes.RoadshowRouteTable.Rows.Count; i++) {
                    //Check if a new route (i.e. selected by user)
                    DriverCompDataset.RoadshowRouteTableRow route = this.mRoutes.RoadshowRouteTable[i];
                    if(route.New) {
                        //Validate route depot matches this agent
                        if(route.DepotNumber.ToString("0000") == this.mAgentNumber) {
                            //Add driver compensation (parent) record if required
                            if(this.mCompensation.DriverCompTable.Select("Operator='" + route.Operator + "'").Length == 0) {
                                //Add a driver compensation for this operator
                                DriverCompDataset.DriverCompTableRow driverComp = this.mCompensation.DriverCompTable.NewDriverCompTableRow();
                                #region Initialize members
                                driverComp.Select = true;
                                driverComp.IsNew = driverComp.IsCombo = driverComp.IsAdjust = false;
                                driverComp.AgentNumber = route.DepotNumber.ToString("0000");
                                driverComp.FinanceVendorID = (!route.IsFinanceVendIDNull() ? route.FinanceVendID : "00000");
                                driverComp.FinanceVendor = (!route.IsPayeeNull() ? route.Payee : "");
                                driverComp.Operator = route.Operator;
                                //driverComp.EquipmentTypeID = route.EquipmentID;
                                driverComp.Miles = driverComp.Trip = driverComp.Stops = driverComp.Cartons = driverComp.Pallets = driverComp.PickupCartons = 0;
                                driverComp.MilesAmount = driverComp.DayAmount = driverComp.TripAmount = driverComp.StopsAmount = driverComp.CartonsAmount = driverComp.PalletsAmount = driverComp.PickupCartonsAmount = driverComp.Amount = 0.0M;
                                driverComp.FSCMiles = 0;
                                driverComp.FuelCost = driverComp.FSCGal = driverComp.FSCBaseRate = driverComp.FSC = 0.0M;
                                driverComp.MinimunAmount = driverComp.AdminCharge = driverComp.AdjustmentAmount1 = driverComp.AdjustmentAmount2 = driverComp.TotalAmount = 0.0M;
                                #endregion
                                this.mCompensation.DriverCompTable.AddDriverCompTableRow(driverComp);
                            }

                            //Validate driver route doesn't exist; add if doesn't exist
                            if(this.mCompensation.DriverRouteTable.Select("Operator='" + route.Operator + "' AND RouteDate='" + route.Rt_Date + "' AND RouteName='" + route.Rt_Name + "'").Length == 0) {
                                //Create a driver route from the Roadshow route
                                DriverCompDataset.DriverRouteTableRow driverRoute = this.mCompensation.DriverRouteTable.NewDriverRouteTableRow();
                                #region Set members
                                driverRoute.ID = 0;
                                driverRoute.IsNew = false;
                                driverRoute.IsCombo = (this.mRoutes.RoadshowRouteTable.Select("Operator='" + route.Operator + "' AND Rt_Date='" + route.Rt_Date + "'").Length > 1);
                                driverRoute.IsAdjust = route.Rt_Name.Contains("ADJUST");
                                driverRoute.AgentNumber = route.DepotNumber.ToString("0000");
                                driverRoute.FinanceVendorID = (!route.IsFinanceVendIDNull() ? route.FinanceVendID : "00000");
                                driverRoute.EquipmentTypeID = route.EquipmentID;
                                driverRoute.RouteIndex = route.RouteIndex;
                                driverRoute.RouteDate = route.Rt_Date;
                                driverRoute.RouteName = route.Rt_Name;
                                driverRoute.Operator = route.Operator;
                                driverRoute.OperatorHireDate = route.OperatorHireDate;
                                driverRoute.Payee = (!route.IsPayeeNull() ? route.Payee : "");
                                driverRoute.RateTypeID = RouteRates.RATETYPE_NONE;
                                driverRoute.Miles = (!route.IsTtlMilesNull()) ? route.TtlMiles : 0;
                                driverRoute.MilesBaseRate = 0.0M;
                                driverRoute.MilesRate = 0.0M;
                                driverRoute.MilesAmount = 0.0M;
                                driverRoute.DayRate = 0.0M;
                                driverRoute.DayAmount = 0.0M;
                                driverRoute.Trip = (!route.IsMultiTrpNull()) ? route.MultiTrp : 0;
                                driverRoute.TripRate = 0.0M;
                                driverRoute.TripAmount = 0.0M;
                                driverRoute.Stops = (!route.IsUniqueStopsNull()) ? route.UniqueStops : 0;
                                driverRoute.StopsRate = 0.0M;
                                driverRoute.StopsAmount = 0.0M;
                                driverRoute.Cartons = (!route.IsDelCtnsNull()) ? (int)route.DelCtns : 0;
                                driverRoute.CartonsRate = 0.0M;
                                driverRoute.CartonsAmount = 0.0M;
                                driverRoute.Pallets = (!route.IsDelPltsorRcksNull()) ? (int)route.DelPltsorRcks : 0;
                                driverRoute.PalletsRate = 0.0M;
                                driverRoute.PalletsAmount = 0.0M;
                                driverRoute.PickupCartons = (!route.IsRtnCtnNull()) ? (int)route.RtnCtn : 0;
                                driverRoute.PickupCartonsRate = 0.0M;
                                driverRoute.PickupCartonsAmount = 0.0M;
                                driverRoute.FSCMiles = 0;
                                driverRoute.FSCGal = 0.0M;
                                driverRoute.FSCBaseRate = 0.0M;
                                driverRoute.FSC = 0.0M;
                                driverRoute.MinimunAmount = 0.0M;
                                driverRoute.AdminCharge = 0.0M;
                                driverRoute.AdjustmentAmount1 = 0.0M;
                                driverRoute.AdjustmentAmount1TypeID = "";
                                driverRoute.AdjustmentAmount2 = 0.0M;
                                driverRoute.AdjustmentAmount2TypeID = "";
                                driverRoute.TotalAmount = 0.0M;
                                //driverRoute.Imported = ;
                                //driverRoute.Exported = ;
                                driverRoute.ArgixRtType = route.ArgixRtType;
                                driverRoute.LastUpdated = DateTime.Today;
                                driverRoute.UserID = Environment.UserName;

                                #endregion

                                //Apply rates and FSC to the current route
                                RouteRates rates = this.mRates.GetRates(driverRoute.EquipmentTypeID,driverRoute.RouteName,driverRoute.Miles);
                                if(!driverRoute.IsAdjust) applyRates(driverRoute,rates);
                                applyFSC(driverRoute,rates);
                                
                                //Add route to rated routes
                                this.mCompensation.DriverRouteTable.AddDriverRouteTableRow(driverRoute);

                                //Apply fees to the current route based upon all routes for this driver
                                applyFees(route.Operator);

                                //Apply bonus to the current route based upon all routes for this driver
                                applyBonus(route.Operator);

                                //Update driver compensation for all routes for the current driver
                                CalculateCompensation(route.Operator,false);
                            }
                            else {
                                //Route exists
                                MessageBox.Show("Route exists for " + route.Operator + " on " + route.Rt_Date.ToShortDateString());
                            }
                            route.New = false;
                        }
                        else {
                            //Wrong terminal
                            System.Windows.Forms.MessageBox.Show("Route " + route.Rt_Name + " belongs to " + route.Depot + " terminal.");
                        }
                    }
                }
                SaveCompensation();
                ImportRoutes();
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException(ex.Message,ex); }
        }
        public void CalculateCompensation(string _operator, bool validateRouteRatings) {
            //Calculate driver compensation for the specified operator
            try {
                //Find the driver compensation and driver routes for the specified operator from the data cache
                DriverCompDataset.DriverCompTableRow[] driverComps = (DriverCompDataset.DriverCompTableRow[])this.mCompensation.DriverCompTable.Select("Operator='" + _operator + "'");
                DriverCompDataset.DriverRouteTableRow[] driverRoutes = (DriverCompDataset.DriverRouteTableRow[])this.mCompensation.DriverRouteTable.Select("Operator='" + _operator + "'");

                //Reset driver compensation for this operator
                if(driverComps.Length > 1) throw new ApplicationException("When calculating compensation for " + _operator + ", " + driverComps.Length.ToString() + " records were found.");
                DriverCompDataset.DriverCompTableRow driverComp = driverComps[0];
                driverComp.IsNew = driverComp.IsCombo = driverComp.IsAdjust = false;
                driverComp.Miles = driverComp.Trip = driverComp.Stops = driverComp.Cartons = driverComp.Pallets = driverComp.PickupCartons = 0;
                driverComp.MilesAmount = driverComp.DayAmount = driverComp.TripAmount = driverComp.StopsAmount = driverComp.CartonsAmount = driverComp.PalletsAmount = driverComp.PickupCartonsAmount = driverComp.Amount = 0.0M;
                driverComp.FSC = 0.0M;
                driverComp.MinimunAmount = driverComp.AdminCharge = driverComp.AdjustmentAmount1 = driverComp.AdjustmentAmount2 = driverComp.TotalAmount = 0.0M;

                if(validateRouteRatings) {
                    //Re-assess fees and bonus for this driver
                    applyFees(_operator);
                    applyBonus(_operator);
                }

                //Compute driver compensation for this operator from all rated routes
                for(int i = 0; i < driverRoutes.Length; i++) {
                    DriverCompDataset.DriverRouteTableRow driverRoute = driverRoutes[i];
                    #region Sum all driver routes
                    driverComp.IsNew = driverRoute.IsNew ? true : driverComp.IsNew;
                    driverComp.IsCombo = driverRoute.IsCombo ? true : driverComp.IsCombo;
                    driverComp.IsAdjust = driverRoute.IsAdjust ? true : driverComp.IsAdjust;
                    driverComp.Miles += driverRoute.Miles;
                    driverComp.MilesAmount += driverRoute.MilesAmount;
                    driverComp.DayAmount += driverRoute.DayAmount;
                    driverComp.Trip += driverRoute.Trip;
                    driverComp.TripAmount += driverRoute.TripAmount;
                    driverComp.Stops += driverRoute.Stops;
                    driverComp.StopsAmount += driverRoute.StopsAmount;
                    driverComp.Cartons += driverRoute.Cartons;
                    driverComp.CartonsAmount += driverRoute.CartonsAmount;
                    driverComp.Pallets += driverRoute.Pallets;
                    driverComp.PalletsAmount += driverRoute.PalletsAmount;
                    driverComp.PickupCartons += driverRoute.PickupCartons;
                    driverComp.PickupCartonsAmount += driverRoute.PickupCartonsAmount;
                    driverComp.FSCMiles += driverRoute.FSCMiles;
                    driverComp.FSCGal = driverRoute.FSCGal;
                    driverComp.FuelCost = driverRoute.FuelCost;
                    driverComp.FSCBaseRate = this.mTerminalConfig.FSBase;
                    driverComp.FSC += driverRoute.FSC;
                    driverComp.MinimunAmount = driverRoute.MinimunAmount;
                    driverComp.AdminCharge += driverRoute.AdminCharge;
                    driverComp.AdjustmentAmount1 += driverRoute.AdjustmentAmount1;
                    driverComp.AdjustmentAmount2 += driverRoute.AdjustmentAmount2;
                    //driverComp.AdjustmentAmount3 += driverRoute.AdjustmentAmount3;
                    driverComp.Amount += driverRoute.TotalAmount;
                    #endregion
                }

                //Compute totals
                driverComp.TotalAmount = driverComp.Amount + driverComp.FSC + driverComp.AdjustmentAmount1 + driverComp.AdjustmentAmount2;
            }
            catch(ApplicationException ex) { throw ex; }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
        }
        public bool SaveCompensation() {
            //Save
            bool result = false;
            try {
                //Save all new routes
                for(int i = 0; i < this.mCompensation.DriverRouteTable.Rows.Count; i++) {
                    //Validate route as new (NULL Import date)
                    if(this.mCompensation.DriverRouteTable[i].IsImportedNull()) {
                        DriverCompDataset.DriverRouteTableRow driverRoute = this.mCompensation.DriverRouteTable[i];
                        driverRoute.Imported = DateTime.Now;
                        result = FinanceGateway.CreateDriverRoute(driverRoute);
                    }
                }
                ViewCompensation();
            }
            catch(ApplicationException ex) { throw ex; }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return result;
        }
        public bool UpdateCompensation() {
            //Update 
            bool result = false;
            try {
                //Updated routes
                DriverCompDataset driverRoutes = (DriverCompDataset)this.mCompensation.GetChanges(DataRowState.Modified);
                if(driverRoutes != null && driverRoutes.DriverRouteTable.Rows.Count > 0) {
                    //Update all modified driver compensations
                    foreach(DriverCompDataset.DriverRouteTableRow driverRoute in driverRoutes.DriverRouteTable.Rows) {
                        try {
                            driverRoute.LastUpdated = DateTime.Now;
                            driverRoute.UserID = Environment.UserName;
                            result = FinanceGateway.UpdateDriverRoute(driverRoute);
                            if(result) driverRoute.AcceptChanges(); else driverRoute.RejectChanges();
                        }
                        catch(Exception) {
                            driverRoute.RejectChanges();
                            System.Windows.Forms.MessageBox.Show("Failed to update route (" + driverRoute.RouteDate.ToShortDateString() + ", " + driverRoute.Operator, "Route Update", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        }
                    }
                }
                
                //Deleted routes- need to re-assess fees and bonus
                driverRoutes = (DriverCompDataset)this.mCompensation.GetChanges(DataRowState.Deleted);
                if(driverRoutes != null && driverRoutes.DriverRouteTable.Rows.Count > 0) {
                    //Delete all deleted driverRoutes
                    DriverCompDataset.DriverRouteTableRow driverRoute = null;
                    for(int i = driverRoutes.DriverRouteTable.Rows.Count - 1; i >= 0; i--) {
                        try {
                            driverRoute = (DriverCompDataset.DriverRouteTableRow)driverRoutes.DriverRouteTable.Rows[i];
                            driverRoute.RejectChanges();
                            if(!driverRoute.IsImportedNull())
                                result = FinanceGateway.DeleteDriverRoute(driverRoute.ID);
                            else
                                result = true;
                            if(result) { driverRoute.Delete(); driverRoute.AcceptChanges(); }
                        }
                        catch(Exception) {
                            System.Windows.Forms.MessageBox.Show("Failed to update route (" + driverRoute.RouteDate.ToShortDateString() + ", " + driverRoute.Operator, "Route Update", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                        }
                    }
                }
                this.mCompensation.AcceptChanges();
                ImportRoutes();
            }
            catch(Exception ex) {
                this.mCompensation.RejectChanges();
                throw new ApplicationException(ex.Message,ex);
            }
            return result;
        }
        public string ExportCompensation(bool updateAsExported) {
            //Export this driver compensation to file
            StringBuilder export = null;
            try {
                //Setup
                export = new StringBuilder();
                PayPeriod payPeriod = FinanceGateway.GetPayPeriod(this.mEndDate);

                //Export driver compensations and admin charges
                foreach(DriverCompDataset.DriverCompTableRow comp in this.mCompensation.DriverCompTable.Rows) {
                    if(comp.Select && comp.TotalAmount > 0) {
                        //Export driver compensation
                        export.AppendLine(formatDriverRecord(payPeriod,comp));

                        //Export Admin fees if applicable
                        DriverCompDataset.DriverRouteTableRow[] routes = (DriverCompDataset.DriverRouteTableRow[])this.mCompensation.DriverRouteTable.Select("AgentNumber=" + this.mAgentNumber + " AND Operator='" + comp.Operator + "'");
                        decimal adminFee = 0.0M;
                        for(int i = 0; i < routes.Length; i++) { adminFee += routes[i].AdminCharge; }
                        if(adminFee != 0)
                            export.AppendLine(formatAdminRecord(payPeriod,comp,adminFee));
                        
                        if(updateAsExported) {
                            //Update all routes as exported
                            for(int i = 0; i < routes.Length; i++) {
                                routes[i].Exported = DateTime.Today;
                                routes[i].LastUpdated = DateTime.Now;
                                routes[i].UserID = Environment.UserName;
                                FinanceGateway.UpdateDriverRoute(routes[i]);
                            }
                        }
                    }
                }
                //Refresh routes if they were updated (otherwise, don't change export selections)
                if(updateAsExported) ViewCompensation();
            }
            catch(ApplicationException ex) { throw ex; }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return export.ToString();
        }
        #region Local Services: applyRates(), applyFSC(), applyFees(), applyBonus(), formatDriverRecord(), formatAdminRecord()
        private void applyRates(DriverCompDataset.DriverRouteTableRow driverRoute,RouteRates rates) {
            //Apply rates to this route
            try {
                //1. Copy rates (for reference)
                driverRoute.RateTypeID = rates.RateTypeID;
                driverRoute.MilesBaseRate = rates.MileBaseRate;
                driverRoute.MilesRate = rates.MileRate;
                driverRoute.DayRate = rates.DayRate;
                driverRoute.TripRate = rates.TripRate;
                driverRoute.StopsRate = rates.StopRate;
                driverRoute.CartonsRate = rates.CartonRate;
                driverRoute.PalletsRate = rates.PalletRate;
                driverRoute.PickupCartonsRate = rates.PickupCartonRate;
                driverRoute.MinimunAmount = rates.MinimumAmount;

                //2. Calculate rates -------------------------------------------------------------
                //2.1 Standard computations
                driverRoute.MilesAmount = driverRoute.MilesBaseRate + driverRoute.Miles * driverRoute.MilesRate;
                driverRoute.DayAmount = driverRoute.DayRate;
                driverRoute.TripAmount = driverRoute.Trip * driverRoute.TripRate;
                driverRoute.StopsAmount = driverRoute.Stops * driverRoute.StopsRate;
                driverRoute.CartonsAmount = driverRoute.Cartons * driverRoute.CartonsRate;
                driverRoute.PalletsAmount = driverRoute.Pallets * driverRoute.PalletsRate;
                driverRoute.PickupCartonsAmount = driverRoute.PickupCartons * driverRoute.PickupCartonsRate;

                //2.2 Override: apply maximums to miles amount based upon trigger field (i.e. Trip, Stops, Cartons, Pallets)
                if(rates.MaximumAmount > 0) {
                    //Maximum applies: find the MaximumTriggerField and compare it's value to MaximumTriggerValue
                    if(driverRoute[rates.MaximumTriggerField] != null) {
                        int trigVal = Convert.ToInt32(driverRoute[rates.MaximumTriggerField]);
                        if(trigVal < rates.MaximumTriggerValue) driverRoute.MilesAmount = rates.MaximumAmount;
                    }
                }

                //3. Calculate totals and apply minimum amount
                decimal total = driverRoute.DayAmount + driverRoute.MilesAmount + driverRoute.TripAmount + driverRoute.StopsAmount + driverRoute.CartonsAmount + driverRoute.PalletsAmount + driverRoute.PickupCartonsAmount;
                driverRoute.TotalAmount = (total < driverRoute.MinimunAmount) ? driverRoute.MinimunAmount : total ;
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
        }
        private void applyFSC(DriverCompDataset.DriverRouteTableRow driverRoute,RouteRates rates) {
            //Calculate FSC if required
            try {
                //1. FSC applies only if miles rates are present in the rating
                if(rates.MileBaseRate > 0 || rates.MileRate > 0) driverRoute.FSCMiles = driverRoute.Miles;

                //2. Copy rates (for reference)
                driverRoute.FuelCost = this.mFuelCost;
                driverRoute.FSCGal = FinanceGateway.GetDriverEquipmentMPG(driverRoute.EquipmentTypeID);
                if(driverRoute.FSCGal <= 0.0M) throw new ApplicationException("FSCGal (" + driverRoute.FSCGal.ToString() + "MPG) is invalid.");
                driverRoute.FSCBaseRate = this.mTerminalConfig.FSBase;
                
                //3. Calculate FSC
                driverRoute.FSC = driverRoute.FSCMiles / driverRoute.FSCGal * (driverRoute.FuelCost - driverRoute.FSCBaseRate);
                if(driverRoute.FSC < 0) driverRoute.FSC = 0.0M;
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException(ex.Message,ex); }
        }
        private void applyFees(string _operator) {
            //Apply admin fee to this route for the specified operator
            try {
                //Get all driver routes for this operator and apply admin fee to one route only
                DriverCompDataset.DriverRouteTableRow[] driverRoutes = (DriverCompDataset.DriverRouteTableRow[])this.mCompensation.DriverRouteTable.Select("AgentNumber=" + this.mAgentNumber + " AND Operator='" + _operator + "'");
                foreach(DriverCompDataset.DriverRouteTableRow route in driverRoutes) { route.AdminCharge = 0.0M; }
                if(driverRoutes.Length > 0) driverRoutes[0].AdminCharge = this.mTerminalConfig.AdminFee;
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
        }
        private void applyBonus(string _operator) {
            //Apply bonus to applicable routes for the specified operator
            //NOTE: Existing driver routes do not have OperatorHireDate since it is not persisted
            try {
                //Get all driver routes for this operator and apply bonus to one route per day if the driver has worked beyond the probationary period
                if(this.mTerminalConfig.BonusRate > 0) {
                    //Get the routes
                    DriverCompDataset.DriverRouteTableRow[] driverRoutes = (DriverCompDataset.DriverRouteTableRow[])this.mCompensation.DriverRouteTable.Select("AgentNumber=" + this.mAgentNumber + " AND Operator='" + _operator + "'");
                    
                    //Find a hire date in the imported routes for this operator
                    DateTime hireDate = DateTime.MinValue;
                    DriverCompDataset.RoadshowRouteTableRow[] routes = (DriverCompDataset.RoadshowRouteTableRow[])this.mRoutes.RoadshowRouteTable.Select("DepotNumber=" + this.mAgentNumber + " AND Operator='" + _operator + "'");
                    foreach(DriverCompDataset.RoadshowRouteTableRow route in routes) {
                        if(!route.IsOperatorHireDateNull()) {
                            hireDate = route.OperatorHireDate;
                            break;
                        }
                    }
                    if(hireDate == DateTime.MinValue) throw new ApplicationException("Could not compute bonus for " + _operator + ", hire date was not found.");
                    bool bonusSu = false, bonusM = false, bonusT = false, bonusW = false, bonusR = false, bonusF = false, bonusSa = false;
                    foreach(DriverCompDataset.DriverRouteTableRow route in driverRoutes) {
                        //Apply bonus to one route per day once hire date + threshold >= route date; exclude adjustment routes
                        route.AdjustmentAmount1 = 0.0M;
                        route.AdjustmentAmount1TypeID = "";
                        if(hireDate.AddDays(global::Argix.Properties.Settings.Default.BonusThreshold).CompareTo(route.RouteDate) <= 0 && !route.IsAdjust) {
                            switch(route.RouteDate.DayOfWeek) {
                                case DayOfWeek.Sunday:
                                    if(!bonusSu) {
                                        route.AdjustmentAmount1 = this.mTerminalConfig.BonusRate;
                                        route.AdjustmentAmount1TypeID = "Bonus";
                                        bonusSu = true;
                                    }
                                    break;
                                case DayOfWeek.Monday:
                                    if(!bonusM) {
                                        route.AdjustmentAmount1 = this.mTerminalConfig.BonusRate;
                                        route.AdjustmentAmount1TypeID = "Bonus";
                                        bonusM = true;
                                    }
                                    break;
                                case DayOfWeek.Tuesday:
                                    if(!bonusT) {
                                        route.AdjustmentAmount1 = this.mTerminalConfig.BonusRate;
                                        route.AdjustmentAmount1TypeID = "Bonus";
                                        bonusT = true;
                                    }
                                    break;
                                case DayOfWeek.Wednesday:
                                    if(!bonusW) {
                                        route.AdjustmentAmount1 = this.mTerminalConfig.BonusRate;
                                        route.AdjustmentAmount1TypeID = "Bonus";
                                        bonusW = true;
                                    }
                                    break;
                                case DayOfWeek.Thursday:
                                    if(!bonusR) {
                                        route.AdjustmentAmount1 = this.mTerminalConfig.BonusRate;
                                        route.AdjustmentAmount1TypeID = "Bonus";
                                        bonusR = true;
                                    }
                                    break;
                                case DayOfWeek.Friday:
                                    if(!bonusF) {
                                        route.AdjustmentAmount1 = this.mTerminalConfig.BonusRate;
                                        route.AdjustmentAmount1TypeID = "Bonus";
                                        bonusF = true;
                                    }
                                    break;
                                case DayOfWeek.Saturday:
                                    if(!bonusSa) {
                                        route.AdjustmentAmount1 = this.mTerminalConfig.BonusRate;
                                        route.AdjustmentAmount1TypeID = "Bonus";
                                        bonusSa = true;
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
        }
        private string formatDriverRecord(PayPeriod payPeriod, DriverCompDataset.DriverCompTableRow comp) {
            //
            #region Driver record
            //All fields fixed 12 position and left justified
            //022         13418       09/13/08    WE091308    Mi          484                                 012         08          1424.23     18505000    N
            //------------============------------============------------============------------============------------============------------============------------
            //|           |           |           |           |           |           |           |           |           |           |           |           |
            //a           b           c           d           e           f           g           h           i           j           k           l           m

            //a) Company Code [12]; always 022
            //b) VendorFinanceID [12];
            //c) Date [12, MM/dd/yyyy];
            //d) Invoice# [12, WE + Date(MMddyy)];
            //e) Desc1 [12]; always blank
            //f) Desc2 [12]; always blank
            //g) Desc3 [12]; always blank
            //h) Desc4 [12]; always blank
            //i) Pay period month [12, MM];
            //j) Pay period year [12, yy];
            //k) Total amount [12];
            //l) General LG# [12];
            //m) TaxID [12]; always N
            #endregion
            string s = "";
            try {
                s = "022".PadRight(12,' ') +
                    comp.FinanceVendorID.PadRight(12,' ').Substring(0,12) +
                    this.EndDate.ToString("MM/dd/yy").PadRight(12,' ').Substring(0,12) +
                    "WE" + this.EndDate.ToString("MMddyy").PadRight(10,' ').Substring(0,10) +
                    "".PadRight(12,' ').Substring(0,12) +
                    "".PadRight(12,' ').Substring(0,12) +
                    "".PadRight(12,' ').Substring(0,12) +
                    "".PadRight(12,' ').Substring(0,12) +
                    payPeriod.Month.PadLeft(3, '0').PadRight(12,' ').Substring(0,12) +
                    payPeriod.Year.Substring(2,2).PadRight(12,' ').Substring(0,12) +
                    comp.TotalAmount.ToString("#0.00").PadRight(12,' ').Substring(0,12) +
                    this.mTerminalConfig.GLNumber.PadRight(12,' ').Substring(0,12) +
                    "N".PadRight(12,' ').Substring(0,12);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return s;
        }
        private string formatAdminRecord(PayPeriod payPeriod,DriverCompDataset.DriverCompTableRow comp,decimal adminFee) {
            //
            #region Admin record
            //All fields fixed 12 position and left justified
            //022         13418       09/13/08    AD091308    Ad                                              012         08          -10.00      18516000    N
            //------------============------------============------------============------------============------------============------------============------------
            //|           |           |           |           |           |           |           |           |           |           |           |           |
            //a           b           c           d           e           f           g           h           i           j           k           l           m

            //a) Company Code [12]; always 022
            //b) VendorFinanceID [12];
            //c) Date [12, MM/dd/yyyy];
            //d) Invoice# [12, AD + Date(MMddyy)];
            //e) Desc1 [12]; always blank
            //f) Desc2 [12]; always blank
            //g) Desc3 [12]; always blank
            //h) Desc4 [12]; always blank
            //i) Pay period month [12, mm];
            //j) Pay period year [12, yy];
            //k) Admin amount [12];
            //l) General LG# [12];
            //m) TaxID [12]; always N
            #endregion
            string s = "";
            try {
                s = "022".PadRight(12,' ') +
                    comp.FinanceVendorID.PadRight(12,' ').Substring(0,12) +
                    this.EndDate.ToString("MM/dd/yy").PadRight(12,' ').Substring(0,12) +
                    "AD" + this.EndDate.ToString("MMddyy").PadRight(10,' ').Substring(0,10) +
                    "".PadRight(12,' ').Substring(0,12) +
                    "".PadRight(12,' ').Substring(0,12) +
                    "".PadRight(12,' ').Substring(0,12) +
                    "".PadRight(12,' ').Substring(0,12) +
                    payPeriod.Month.PadLeft(3,'0').PadRight(12,' ').Substring(0,12) +
                    payPeriod.Year.Substring(2,2).PadRight(12,' ').Substring(0,12) +
                    adminFee.ToString("#0.00").PadRight(12,' ').Substring(0,12) +
                    this.mTerminalConfig.AdminGLNumber.PadRight(12,' ').Substring(0,12) +
                    "N".PadRight(12,' ').Substring(0,12);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return s;
        }
        #endregion
    }
}
