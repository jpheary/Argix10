<%@ Page Title="Argix :: Supply Chain Management" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpContent" runat="server">
    <div class="header-supply-chain">
        <p><span class="headline">When the longest</span><span class="headline-red"><br />
        </span><span class="headline">distance requires the</span><span class="headline"><br />
        </span><span class="headline-red">shortest time</span></p>
    </div>
    <div class="content">
        <p class="body-nav">Home > Supply Chain Management &gt; <span class="sub-nav-highlight">Air Freight</span></p>
        <span class="big_caps">A</span><span class="titles">ir Freight</span>
        <p class="body-copy">Your air freight needs are in good hands with Argix Logistics.<br /><br />Our experienced team 
        can coordinate the movement of your international and domestic cargo-whenever and from where ever it is. Our relationship 
        with many of the world's best forwarders combined with our negotiating skills and our ability to induct these shipments 
        into to the Argix Network at any point assures you the most reliable service at the best possible rates.</p>
    </div>
    <div class="side_bar_air">
        <p class="sub-nav">
            <span class="sub-nav-bigger"><a href="<%: Url.Action("supply_chain", "Home")%>">Supply Chain <br />Management</a></span><br />
            <a href="<%: Url.Action("international", "Home")%>">International Consolidation</a><br />
            <a href="<%: Url.Action("ocean_freight", "Home")%>">Ocean Freight</a><br />
            <span class="sub-nav-highlight">Air Freight</span><br />
            <a href="<%: Url.Action("customs_brokerage", "Home")%>">Customs Brokerage</a><br />
            <a href="<%: Url.Action("drayage", "Home")%>">Drayage</a><br />
            <a href="<%: Url.Action("domestic_deconsolidation", "Home")%>">Domestic Deconsolidation</a>
        </p>
    </div>
</asp:Content>
