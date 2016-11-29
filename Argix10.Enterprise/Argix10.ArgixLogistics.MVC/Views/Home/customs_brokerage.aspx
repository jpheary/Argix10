<%@ Page Title="Argix :: Supply Chain Management" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpContent" runat="server">
    <div class="header-supply-chain">
        <p><span class="headline">Where </span><span class="headline-red">compliance<br /></span><span class="headline">means everything</span></p>
    </div>
    <div class="content">
        <p class="body-nav">Home > Supply Chain Management &gt; <span class="sub-nav-highlight">Customs Brokerage</span></p>
        <span class="big_caps">C</span><span class="titles">ustoms Brokerage</span>
        <p class="body-copy">Argix understands that Customs Brokerage is one of the most important<br />
        elements in an importer's supply chain network; a necessary operation that<br />
        most organizations may not have the time or expertise to deal with. To an importer &quot;no news&quot; is the only news you want from a resource that manages your interests with the U.S. Department of Homeland Security's Customs and Border Patrol. There is only one rule – Compliance.<br />
        <br />
        Argix connects you with experts in this field familiar with the latest requirements and can help align your product for appropriate classification and documentation that streamlines entry into U.S. Commerce. Strategically allied with only C-TPAT certified partners Argix can assist you in navigating through compliance instruction and certification.</p>
    </div>
    <div class="side_bar_customs">
        <p class="sub-nav">
            <span class="sub-nav-bigger"><a href="<%: Url.Action("supply_chain", "Home")%>">Supply Chain <br />Management</a></span><br />
            <a href="<%: Url.Action("international", "Home")%>">International Consolidation</a><br />
            <a href="<%: Url.Action("ocean_freight", "Home")%>">Ocean Freight</a><br />
            <a href="<%: Url.Action("air_freight", "Home")%>">Air Freight</a><br />
            <span class="sub-nav-highlight">Customs Brokerage</span><br />
            <a href="<%: Url.Action("drayage", "Home")%>">Drayage</a><br />
            <a href="<%: Url.Action("domestic_deconsolidation", "Home")%>">Domestic Deconsolidation</a>
        </p>
    </div>
</asp:Content>
