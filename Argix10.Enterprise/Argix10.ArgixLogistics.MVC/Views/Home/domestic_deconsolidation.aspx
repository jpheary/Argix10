<%@ Page Title="Argix :: Supply Chain Management" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpContent" runat="server">
    <div class="header-supply-chain">
        <p><span class="headline-red">Unloading, sorting</span><span class="headline"> and</span><span class="headline-red">distributing</span><span class="headline"> your<br />foreign-sourced product</span></p>
    </div>
    <div class="content">
        <p class="body-nav">Home > Supply Chain Management &gt; <span class="sub-nav-highlight">Domestic Deconsolidation</span></p>
        <span class="big_caps">D</span><span class="titles">omestic Deconsolidation</span>
        <p class="body-copy">Deconsolidation – the process of unloading, sorting, verifying, and distributing<br />
        your foreign-sourced product – is essential to the logistics process. As part of<br />
        our Supply Chain Management services, Argix will coordinate timely and<br />
        accurate deconsolidation of your direct imported products, and/or product<br />
        sourced through vendors who are domestic importers, into your domestic operations.</p>
    </div>
    <div class="side_bar_domestic">
        <p class="sub-nav">
            <span class="sub-nav-bigger"><a href="<%: Url.Action("supply_chain", "Home")%>">Supply Chain <br />Management</a></span><br />
            <a href="<%: Url.Action("international", "Home")%>">International Consolidation</a><br />
            <a href="<%: Url.Action("ocean_freight", "Home")%>">Ocean Freight</a><br />
            <a href="<%: Url.Action(">air_freight", "Home")%>">Air Freight</a><br />
            <a href="<%: Url.Action("customs_brokerage", "Home")%>">Customs Brokerage</a><br />
            <a href="<%: Url.Action("drayage", "Home")%>">Drayage</a><br />
            <span class="sub-nav-highlight">Domestic Deconsolidation</span>
        </p>
    </div>
</asp:Content>
