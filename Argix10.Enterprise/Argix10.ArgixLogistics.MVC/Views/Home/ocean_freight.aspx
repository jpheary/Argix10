<%@ Page Title="Argix :: Supply Chain Management" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpContent" runat="server">
    <div class="header-supply-chain">
        <p><span class="headline">Ocean freight service</span><span class="headline-red"><br /></span><span class="headline">that meets your </span><span class="headline-red">unique</span><span class="headline"><br /></span><span class="headline-red">needs and budget</span></p>
    </div>
    <div class="content">
        <p class="body-nav">Home > Supply Chain Management &gt; <span class="sub-nav-highlight">Ocean Freight</span></p>
        <span class="big_caps">O</span><span class="titles">cean Freight</span>
        <p class="body-copy">Argix Supply Chain Management understands that an organization's ocean freight requirements are rarely a one-size-fits-all proposition. However, it is often the case that the largest volumes get the most attention. We offer reliable ocean freight services that are designed to fit your import program.<br />
        <br />
        Our team of experienced negotiators will help you secure what you need, when you need it. By consolidating volumes from across the industry, Argix can negotiate on your behalf with NVOs, freight forwarders and the Steam Ship lines directly to create an Ocean Freight program that can improve service at the best rates available.</p>
    </div>
    <div class="side_bar_ocean">
        <p class="sub-nav">
            <span class="sub-nav-bigger"><a href="<%: Url.Action("supply_chain", "Home")%>">Supply Chain <br />Management</a></span><br />
            <a href="<%: Url.Action("international", "Home")%>">International Consolidation</a><br />
            <span class="sub-nav-highlight">Ocean Freight</span><br />
            <a href="<%: Url.Action("air_freight", "Home")%>">Air Freight</a><br />
            <a href="<%: Url.Action("customs_brokerage", "Home")%>">Customs Brokerage</a><br />
            <a href="<%: Url.Action("drayage", "Home")%>">Drayage</a><br />
            <a href="<%: Url.Action("domestic_deconsolidation", "Home")%>">Domestic Deconsolidation</a>
        </p>
    </div>
</asp:Content>
