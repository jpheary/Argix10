<%@ Page Title="Argix :: Supply Chain Management" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpContent" runat="server">
    <div class="header-distribution"><p><span class="headline-red">Expand your capacity,</span><span class="headline"><br />
        </span><span class="headline">not your footprint</span></p>
    </div>
    <div class="content">
        <p class="body-nav">Home > Distribution &gt; <span class="sub-nav-highlight"> Sorting Services</span></p>
        <span class="big_caps">S</span><span class="titles">orting services</span>
        <p class="body-copy">When capacity in your facility becomes an issue Argix Logistics can help. By<br />
        using our Sort Services you can reclaim valuable real estate in your DC, avoid<br />
        large capital investment, and increase carton sort accuracy.</p>
        <p class="body-copy">
        The Argix Logistics 325,000 sq ft national sort center in Jamesburg, NJ is<br />
        equipped with 2 High-Speed Carton Sorters and a 3rd Carton sorting system for<br />
        smaller parcels. Our IT system is capable of receiving multiple file and EDI<br />
        types and has the ability to scan a variety of (label) barcodes.</p>
        <p class="body-copy">
        The Argix Sorting Services can be used to support a client's own delivery<br />
        system or cartons can induct directly into the Argix national Network to their<br />
        final destination.</p>
    </div>
    <div class="side_bar_sort">
        <p class="sub-nav">
            <a href="<%: Url.Action("distribution", "Home")%>"><span class="sub-nav-bigger">Distribution</span></a><br />
            <span class="sub-nav-highlight">Sorting Services</span><br />
            <a href="<%: Url.Action("consolidation", "Home")%>">Consolidation</a><br />
            <a href="<%: Url.Action("fulfillmentservices", "Home")%>">Fulfillment Services</a><br />
            <a href="<%: Url.Action("warehousing", "Home")%>">Warehousing</a>
        </p>
    </div>
</asp:Content>
