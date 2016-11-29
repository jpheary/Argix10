<%@ Page Title="Argix :: Supply Chain Management" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpContent" runat="server">
    <div class="header-supply-chain">
        <p><span class="headline">Keep your cargo</span><span class="headline-red"><br /></span><span class="headline-red">on the move</span></p>
    </div>
    <div class="content">
        <p class="body-nav">Home > Supply Chain Management &gt; <span class="sub-nav-highlight">Drayage</span></p>
        <span class="big_caps">D</span><span class="titles">rayage</span>
        <p class="body-copy">Argix Logistics keeps your cargo on the move, offering reliable drayage or<br />
        container movement services from U.S. ports inland to your company's<br />
        facilities.<br />
        <br />
        Argix can also interact on your behalf at this point to induct directly into the<br />
        Argix Network for warehousing, fulfillment, DC bypass and delivery.</p>
    </div>
    <div class="side_bar_drayage">
        <p class="sub-nav">
            <span class="sub-nav-bigger"><a href="<%: Url.Action("supply_chain", "Home")%>">Supply Chain <br />Management</a></span><br />
            <a href="<%: Url.Action("international", "Home")%>">International Consolidation</a><br />
            <a href="<%: Url.Action("ocean_freight", "Home")%>">Ocean Freight</a><br />
            <a href="<%: Url.Action("air_freight", "Home")%>">Air Freight</a><br />
            <a href="<%: Url.Action("customs_brokerage", "Home")%>">Customs Brokerage</a><br />
            <span class="sub-nav-highlight">Drayage</span><br />
            <a href="<%: Url.Action("domestic_deconsolidation", "Home")%>">Domestic Deconsolidation</a>
        </p>
    </div>
</asp:Content>
