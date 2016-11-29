<%@ Page Title="Argix :: Supply Chain Management" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpContent" runat="server">
    <div class="header-supply-chain">
        <p><span class="headline">Save your</span><span class="headline-red"><br />best decision</span><span class="headline"><br />for first</span></p>
    </div>
    <div class="content">
        <p class="body-nav">Home > Supply Chain Management &gt; <span class="sub-nav-highlight">International Consolidation</span></p>
        <span class="big_caps">I</span><span class="titles">nternational Consolidation</span>
        <p class="body-copy">To get the final supply chain results you expect, the initial decisions you make are the most critical. Argix Logistics leverages strong global connections to help you carry out these all-important first steps.<br />
          <br />
          At Argix we can manage, negotiate and coordinate the consolidation of your import activities from multiple vendors at multiple ports into container shipments. The result is a seamless flow of product into your domestic operation-the way you need it to be.<br />
          <br />
          We can also introduce carton labeling at origin to begin directional pointers for<br />
          each carton to its intended final destination.<br />
          <br />
          For every journey there are important first steps- steps that start you in the right direction.</p>
    </div>
    <div class="side_bar_international">
        <p class="sub-nav">
            <span class="sub-nav-bigger"><a href="<%: Url.Action("supply_chain", "Home")%>">Supply Chain <br />Management</a></span><br />
            <span class="sub-nav-highlight">International Consolidation</span><br />
            <a href="<%: Url.Action("ocean_freight", "Home")%>">Ocean Freight</a><br />
            <a href="<%: Url.Action("air_freight", "Home")%>">Air Freight</a><br />
            <a href="<%: Url.Action("customs_brokerage", "Home")%>">Customs Brokerage</a><br />
            <a href="<%: Url.Action("drayage", "Home")%>">Drayage</a><br />
            <a href="<%: Url.Action("domestic_deconsolidation", "Home")%>">Domestic Deconsolidation</a>
        </p>
    </div>
</asp:Content>
