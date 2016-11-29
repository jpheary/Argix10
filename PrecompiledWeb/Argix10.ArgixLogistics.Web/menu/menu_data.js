fixMozillaZIndex=true; //Fixes Z-Index problem  with Mozilla browsers but causes odd scrolling problem, toggle to see if it helps
_menuCloseDelay=500;
_menuOpenDelay=150;
_subOffsetTop=4;
_subOffsetLeft=0;



with(AllImageStyle=new mm_style()){
styleid=1;
}


with (SubMenuStyle = new mm_style()) {
	itemheight=30;
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
	orientation="horizontal";
	left="offset=2";
	menuwidth=642;
    style = SubMenuStyle;
	aI("text=The Argix Network;url=network.html;");
	aI("text=About Argix;url=about.html;");
	aI("text=Contact;url=contact.aspx;");
}


with (milonic = new menuname("Transportation")) {
	orientation="horizontal";
	left="offset=-78";
	menuwidth=642;
    style = SubMenuStyle;
	aI("text=National B2B Delivery;url=nationwideB2Bdelivery.html;");
	aI("text=National B2C Delivery;url=nationwideB2Cdelivery.html;");
    aI("text=DC Bypass;url=DCbypass.html;");
	aI("text=Regional Delivery;url=regionaldelivery.html;");
}

with (milonic = new menuname("Distribution")) {
	orientation="horizontal";
	left="offset=-187";
	menuwidth=642;
    style = SubMenuStyle;
	aI("text=Sorting Services;url=sortingservies.html;");
	aI("text=Consolidation;url=consolidation.html;");
	aI("text=Fulfillment Services;url=fulfillmentservices.html;");
	aI("text=Warehousing;url=warehousing.html;");
}

with (milonic = new menuname("Supply_Chain_Management")) {
	orientation="horizontal";
	left="offset=-284";
	menuwidth=642;
    style = SubMenuStyle;
	aI("text=International Consolidation;url=international.html;");
	aI("text=Ocean Freight;url=ocean-freight.html;");
	aI("text=Air Freight;url=air-freight.html;");
	aI("text=Customs Brokerage;url=customs-brokerage.html;");
	aI("text=Drayage;url=drayage.html;");
	aI("text=Domestic Deconsolidation;url=domestic-deconsolidation.html;");
}



