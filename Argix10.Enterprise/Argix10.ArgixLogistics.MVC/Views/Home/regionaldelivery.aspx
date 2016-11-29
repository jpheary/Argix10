<%@ Page Title="Argix :: Transportation" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpContent" runat="server">
    <div class="header-transportation">
        <p><span class="headline">Maximize </span><span class="headline-red">efficiency</span><span class="headline"><br />and reduce </span><span class="headline-red">cost</span></p>
    </div>
    <div class="content">
        <p class="body-nav">Home > Transportation &gt;<span class="sub-nav-highlight"> Regional Delivery</span></p> 
        <span class="big_caps">R</span><span class="titles">egional Delivery</span>
        <p class="body-copy">Get all of the benefits of our customized national delivery solutions for your regional delivery 
        needs with Argix Logistics' Regional Delivery Program. We maximize efficiency and reduce costs by leveraging the same 
        processes used in our national model and receiving shipments directly to a localized region.<br />
        </p>
        <p class="body-copy">Whether B2B or B2C, Argix Logistics can flow your freight from single or multiple pick up/induct 
        points, bypassing some or all DC operations and combining the aggregate into a single delivery and billing. The Argix 
        Network enables day and time definite delivery precision affording you with predictable planning and reliable execution.
        <br /><br />Flexible options such as EDI, zone skipping, drop trailer/yard management and a customer-tailored tracking and 
        reporting package all add up to increased value.</p>
    </div>
    <div class="side_bar_regional">
        <p class="sub-nav">
            <span class="sub-nav-bigger"><a href="<%: Url.Action("transportation", "Home")%>">Transportation</a></span><br />
            <a href="<%: Url.Action("nationwideB2Bdelivery", "Home")%>">Nationwide B2B Delivery</a><br />
            <a href="<%: Url.Action("nationwideB2Cdelivery", "Home")%>">Nationwide B2C Delivery</a><br />
            <a href="<%: Url.Action("DCbypass", "Home")%>">DC Bypass</a><br />
            <a href="<%: Url.Action("LTDdelivery", "Home")%>">LTL Delivery</a><br />
            <span class="sub-nav-highlight">Regional Delivery</span>
        </p>
    </div>
</asp:Content>
