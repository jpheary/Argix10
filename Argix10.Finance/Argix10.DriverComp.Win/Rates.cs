//	File:	rates.cs
//	Author:	jheary
//	Date:	08/20/08
//	Desc:	Static object caches latest rates for a specified effective date; instance 
//          exposes driver rates for one set of route parameters (i.e. agent#, 
//          equipment type, route, & miles)
//	Rev:	02/26/09 (jph)- added new members MaximumAmount, MaximumTriggerField, & 
//                          MaximumTriggerValue to RouteRatings; revised method 
//                          AgentRates::GetRouteRatings() to use new RouteRatings members.
//	---------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Argix.Finance {
    //AgentRates provides driver rates for a single agents for an effective date
    //GetRouteRating() returns a rating for a single route
    public class AgentRates {
        //Members
        private string mAgentNumber = null;
        private string mAgentName = "All Agents";
        private DateTime mEffectiveDate = DateTime.Today;
        private DriverCompDataset mRates = null;

        //Interface
        public AgentRates(string agentNumber,string agentName,DateTime effectiveDate) { 
            //Constructor
            try {
                //Cache the rate tables for this rates date
                this.mAgentNumber = agentNumber;
                this.mAgentName = agentName;
                this.mEffectiveDate = effectiveDate;
                this.mRates = new DriverCompDataset();
                Refresh();
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException(ex.Message,ex); }
        }
        public void Refresh() {
            //Refresh the ratings cache for this instance
            try {
                this.mRates.Clear();
                this.mRates.Merge(FinanceGateway.ReadVehicleMileageRates(this.mEffectiveDate,this.mAgentNumber,-1));
                this.mRates.Merge(FinanceGateway.ReadVehicleUnitRates(this.mEffectiveDate,this.mAgentNumber,-1));
                this.mRates.Merge(FinanceGateway.ReadRouteMileageRates(this.mEffectiveDate,this.mAgentNumber,null));
                this.mRates.Merge(FinanceGateway.ReadRouteUnitRates(this.mEffectiveDate,this.mAgentNumber,null));
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message,ex); }
    }
        public RouteRates GetRates(int equipmentTypeID,string route,decimal miles) {
            //Determine rates for a single driver route
            RouteRates rates = new RouteRates();
            try {
                DriverCompDataset.RateMileageRouteTableRow ratesMR = getRouteMileRates(route,miles);
                if(ratesMR != null) {
                    rates.RateTypeID = RouteRates.RATETYPE_ROUTE;
                    rates.MileBaseRate = ratesMR.BaseRate;
                    rates.MileRate = ratesMR.Rate;
                }
                DriverCompDataset.RateUnitRouteTableRow ratesUR = getRouteUnitRates(route);
                if(ratesUR != null) {
                    rates.RateTypeID = RouteRates.RATETYPE_ROUTE;
                    rates.DayRate = ratesUR.DayRate;
                    rates.TripRate = ratesUR.MultiTripRate;
                    rates.StopRate = ratesUR.StopRate;
                    rates.CartonRate = ratesUR.CartonRate;
                    rates.PalletRate = ratesUR.PalletRate;
                    rates.PickupCartonRate = ratesUR.PickupCartonRate;
                    rates.MinimumAmount = ratesUR.MinimumAmount;
                    rates.MaximumAmount = ratesUR.MaximumAmount;
                    rates.MaximumTriggerField = ratesUR.MaximumTriggerField;
                    rates.MaximumTriggerValue = ratesUR.MaximumTriggerValue;
                    rates.FSBase = ratesUR.FSBase;
                }
                if (rates.RateTypeID == RouteRates.RATETYPE_NONE) {
                    rates.RateTypeID = RouteRates.RATETYPE_VEHICLE;
                    DriverCompDataset.RateMileageTableRow ratesMV = getVehicleMileRates(equipmentTypeID,miles);
                    if(ratesMV != null) {
                        rates.MileBaseRate = ratesMV.BaseRate;
                        rates.MileRate = ratesMV.Rate;
                    }
                    DriverCompDataset.RateUnitTableRow ratesUV = getVehicleUnitRates(equipmentTypeID);
                    if(ratesUV != null) {
                        rates.DayRate = ratesUV.DayRate;
                        rates.TripRate = ratesUV.MultiTripRate;
                        rates.StopRate = ratesUV.StopRate;
                        rates.CartonRate = ratesUV.CartonRate;
                        rates.PalletRate = ratesUV.PalletRate;
                        rates.PickupCartonRate = ratesUV.PickupCartonRate;
                        rates.MinimumAmount = ratesUV.MinimumAmount;
                        rates.MaximumAmount = ratesUV.MaximumAmount;
                        rates.MaximumTriggerField = ratesUV.MaximumTriggerField;
                        rates.MaximumTriggerValue = ratesUV.MaximumTriggerValue;
                        rates.FSBase = ratesUV.FSBase;
                    }
                }
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return rates;
        }
        #region Accessors\Modifiers: [Members...]
        public string AgentNumber { get { return this.mAgentNumber; } }
        public string AgentName { get { return this.mAgentName; } }
        public DateTime EffectiveDate { get { return this.mEffectiveDate; } }
        public DriverCompDataset Rates { get { return this.mRates; } }
        #endregion
        #region Local Services: getVehicleMileRates(), getVehicleUnitRates(), getRouteMileRates(), getRouteUnitRates()
        private DriverCompDataset.RateMileageTableRow getVehicleMileRates(int equipmentTypeID,decimal miles) {
            //Get a mileage rate
            DriverCompDataset.RateMileageTableRow rate = null;
            try {
                string filter = "AgentNumber=" + this.mAgentNumber + " AND EquipmentTypeID=" + equipmentTypeID + " AND Mile <= " + miles;
                DriverCompDataset.RateMileageTableRow[] rates = (DriverCompDataset.RateMileageTableRow[])this.mRates.RateMileageTable.Select(filter,"Mile DESC");
                if(rates.Length > 0) {
                    //Take rate for largest mile
                    rate = rates[0];
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return rate;
        }
        private DriverCompDataset.RateUnitTableRow getVehicleUnitRates(int equipmentTypeID) {
            //Get all unit rates
            DriverCompDataset.RateUnitTableRow rate = null;
            try {
                string filter = "AgentNumber=" + this.mAgentNumber + " AND EquipmentTypeID=" + equipmentTypeID.ToString();
                DriverCompDataset.RateUnitTableRow[] rates = (DriverCompDataset.RateUnitTableRow[])this.mRates.RateUnitTable.Select(filter);
                if(rates.Length > 0) {
                    //Should be a single set
                    if(rates.Length > 1) throw new RateRouteException("More than one rates exists for " + filter);
                    rate = rates[0];
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return rate;
        }
        private DriverCompDataset.RateMileageRouteTableRow getRouteMileRates(string route,decimal miles) {
            //Get a mileage rate
            DriverCompDataset.RateMileageRouteTableRow rate = null;
            try {
                string filter = "AgentNumber=" + this.mAgentNumber + " AND Route='" + route + "' AND Mile <= " + miles;
                DriverCompDataset.RateMileageRouteTableRow[] rates = (DriverCompDataset.RateMileageRouteTableRow[])this.mRates.RateMileageRouteTable.Select(filter,"Mile DESC");
                if(rates.Length > 0) {
                    //Take rate for largest mileage (assumes descending sort order)
                    rate = rates[0];
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return rate;
        }
        private DriverCompDataset.RateUnitRouteTableRow getRouteUnitRates(string route) {
            //Get all unit rates
            DriverCompDataset.RateUnitRouteTableRow rate = null;
            try {
                string filter = "AgentNumber=" + this.mAgentNumber + " AND Route='" + route + "'";
                DriverCompDataset.RateUnitRouteTableRow[] rates = (DriverCompDataset.RateUnitRouteTableRow[])this.mRates.RateUnitRouteTable.Select(filter);
                if(rates.Length > 0) {
                    //Should be a single set
                    if(rates.Length > 1) throw new RateRouteException("More than one rates exists for " + filter);
                    rate = rates[0];
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return rate;
        }
        #endregion
    }

    //Ratings structure with ratings for a single route (i.e. date, agent, route, & equipID)
    public class RouteRates {
        //Members
        private DateTime mEffectiveDate = DateTime.Today;
        private int mRateTypeID = RATETYPE_NONE;
        private decimal mMileBaseRate = 0.0M;
        private decimal mMileRate = 0.0M;
        private decimal mDayRate = 0.0M;
        private decimal mMultiTripRate = 0.0M;
        private decimal mStopRate = 0.0M;
        private decimal mCartonRate = 0.0M;
        private decimal mPalletRate = 0.0M;
        private decimal mPickupCartonRate = 0.0M;
        private decimal mMinimumAmount = 0.0M;
        private decimal mMaximumAmount = 0.0M;
        private string mMaximumTriggerField = "";
        private int mMaximumTriggerValue = 0;
        private decimal mFSBase = 0.0M;

        public const int RATETYPE_NONE = 0;
        public const int RATETYPE_VEHICLE = 1;
        public const int RATETYPE_ROUTE = 2;

        //Interface
        public RouteRates() { }
        public int RateTypeID { get { return this.mRateTypeID; } set { this.mRateTypeID = value; } }
        public decimal MileBaseRate { get { return this.mMileBaseRate; } set { this.mMileBaseRate = value; } }
        public decimal MileRate { get { return this.mMileRate; } set { this.mMileRate = value; } }
        public decimal DayRate { get { return this.mDayRate; } set { this.mDayRate = value; } }
        public decimal TripRate { get { return this.mMultiTripRate; } set { this.mMultiTripRate = value; } }
        public decimal StopRate { get { return this.mStopRate; } set { this.mStopRate = value; } }
        public decimal CartonRate { get { return this.mCartonRate; } set { this.mCartonRate = value; } }
        public decimal PalletRate { get { return this.mPalletRate; } set { this.mPalletRate = value; } }
        public decimal PickupCartonRate { get { return this.mPickupCartonRate; } set { this.mPickupCartonRate = value; } }
        public decimal MinimumAmount { get { return this.mMinimumAmount; } set { this.mMinimumAmount = value; } }
        public decimal MaximumAmount { get { return this.mMaximumAmount; } set { this.mMaximumAmount = value; } }
        public string MaximumTriggerField { get { return this.mMaximumTriggerField; } set { this.mMaximumTriggerField = value; } }
        public int MaximumTriggerValue { get { return this.mMaximumTriggerValue; } set { this.mMaximumTriggerValue = value; } }
        public decimal FSBase { get { return this.mFSBase; } set { this.mFSBase = value; } }
    }
}
