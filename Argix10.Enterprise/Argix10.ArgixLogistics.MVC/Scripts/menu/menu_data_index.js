fixMozillaZIndex = true; //Fixes Z-Index problem  with Mozilla browsers but causes odd scrolling problem, toggle to see if it helps
_menuCloseDelay = 500;
_menuOpenDelay = 150;
_subOffsetTop = 4;
_subOffsetLeft = 0;



with (AllImageStyle = new mm_style()) {
    styleid = 1;
}


with (SubMenuStyle = new mm_style()) {
    itemheight = 30;
    fontfamily = "Helvetica, Arial, sans serif";
    fontsize = "11px";
    fontstyle = "normal";
    offbgcolor = "#c5c7c9";
    offcolor = "#000000";
    onbgcolor = "#c5c7c9";
    oncolor = "#ee2a24";
    outfilter = "randomdissolve(duration=0.3)";
    overfilter = "Fade(duration=0.2);Alpha(opacity=90);Shadow(color=#777777', Direction=135, Strength=3)";
    pagebgcolor = "#c5c7c9";
    pagecolor = "#ee2a24";
    rawcss = "padding:0px 16px 0px 3px;";

}


with (milonic = new menuname("The_Argix_Difference")) {
    orientation = "horizontal";
    left = "offset=2";
    menuwidth = 642;
    style = SubMenuStyle;
    aI("text=The Argix Network;url=home/network;");
    aI("text=About Argix;url=home/about;");
}


with (milonic = new menuname("Transportation")) {
    orientation = "horizontal";
    left = "offset=-78";
    menuwidth = 642;
    style = SubMenuStyle;
    aI("text=National B2B Delivery;url=home/nationwideB2Bdelivery;");
    aI("text=National B2C Delivery;url=home/nationwideB2Cdelivery;");
    aI("text=DC Bypass;url=home/DCbypass;");
    aI("text=LTL Delivery;url=home/LTDdelivery;");
    aI("text=Regional Delivery;url=home/regionaldelivery;");
}

with (milonic = new menuname("Distribution")) {
    orientation = "horizontal";
    left = "offset=-187";
    menuwidth = 642;
    style = SubMenuStyle;
    aI("text=Sorting Services;url=home/sortingservies;");
    aI("text=Consolidation;url=home/consolidation;");
    aI("text=Fulfillment Services;url=home/fulfillmentservices;");
    aI("text=Warehousing;url=home/warehousing;");
}

with (milonic = new menuname("Supply_Chain_Management")) {
    orientation = "horizontal";
    left = "offset=-284";
    menuwidth = 642;
    style = SubMenuStyle;
    aI("text=International Consolidation;url=home/international;");
    aI("text=Ocean Freight;url=home/ocean_freight;");
    aI("text=Air Freight;url=home/air_freight;");
    aI("text=Customs Brokerage;url=home/customs_brokerage;");
    aI("text=Drayage;url=home/drayage;");
    aI("text=Domestic Deconsolidation;url=home/domestic_deconsolidation;");
}



