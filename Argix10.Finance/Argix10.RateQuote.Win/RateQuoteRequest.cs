using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Argix.Finance {
    //
    public class RateQuoteRequest {
        //Members
        private DataModule mTariff = null;
        private string mOriginPostalCode = "";
        private string[] mDestinationPostalCodes;
        private string mClassCode = "";
        private string mUseDiscounts = "N";
        private string mDiscountApplication = "R";
        private string mMinChargeDiscount = "";
        private string mSurchargeApplication = "G";
        private string mUserMinimumChargeFloor = "";

        //Interface
        public RateQuoteRequest(DataModule tariff) {
            //Constructor
            this.mTariff = tariff;
        }
        #region Modifiers/Accessors [Members...]
        [Category("General"), Description("Tariff")]
        public DataModule Tariff { get { return this.mTariff; } }
        [Category("General"), Description("Tariff name (8 characters)")]
        public string TariffName { get { return this.mTariff.tariffNameField; } }
        [Category("General"), Description("Shipment Date (CCYYMMDD); this field is used to select the desired tariff module for rating. If the actual shipment date is passed in this field, RateWareXL will select the appropriate tariff module in effect based on the shipment date and Tariff name passed. If the user wants to specify a specific tariff module for rating (regardless the of actual shipment date), passing the effective date of the tariff module will rate the shipment based on that effective date.")]
        public string ShipmentDate { get { return this.mTariff.effectiveDateField; } }
        [Category("General"), Description("Origin Postal Code")]
        public string OriginPostalCode { get { return this.mOriginPostalCode; } set { this.mOriginPostalCode = value; } }
        [Category("General"), Description("Destination Postal Code")]
        public string[] DestinationPostalCodes { get { return this.mDestinationPostalCodes; } set { this.mDestinationPostalCodes = value; } }
        [Category("General"), Description("Class Code")]
        [System.ComponentModel.TypeConverter(typeof(ClassCodeConverter))]
        public string ClassCode { get { return this.mClassCode; } set { this.mClassCode = value.Substring(0, 2); } }
        [Category("General"), Description("(Y/N); N indicates that discounts should not be applied. Y indicates that discounts should be applied. The default for this field is N.")]
        public string UseDiscounts { get { return this.mUseDiscounts; } set { this.mUseDiscounts = value; } }
        [Category("General"), Description("R=Rates, C=Charges. R means apply discount to the rates. C means apply discounts to the Charges. Default is C.")]
        public string DiscountApplication { get { return this.mDiscountApplication; } set { this.mDiscountApplication = value; } }
        [Category("General"), Description("Minimum Charge Discount (2 decimal places)")]
        public string MCDiscount { get { return this.mMinChargeDiscount; } set { this.mMinChargeDiscount = value; } }
        [Category("General"), Description("G=Gross, N=Net. G means a surcharge applied to gross charges; N means a surcharge applied to net charges")]
        public string SurchargeApplication { get { return this.mSurchargeApplication; } set { this.mSurchargeApplication = value; } }
        [Category("General"), Description("Minimum floor charge entered by user. (Maximum value of 99999.99)")]
        public string UserMinimumChargeFloor { get { return this.mUserMinimumChargeFloor; } set { this.mUserMinimumChargeFloor = value; } }
        #endregion
    }
    public class ClassCodeConverter : TypeConverter {
        private List<string> mCodes = new List<string>();
        public ClassCodeConverter() {
            mCodes.Add("60 Books");
            mCodes.Add("70 China");
            mCodes.Add("85 Auto Parts");
        }
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context) { return true; }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context) { return true; }
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context) { return new StandardValuesCollection(mCodes); }
    }
    public class ClassCode {
        private string _code = "", _description = "";
        public ClassCode(string code, string description) { _code = code; _description = description; }
        public string Code { get { return _code; } set { _code = value; } }
        public string Description { get { return _description; } set { _description = value; } }
    }

}
