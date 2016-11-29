<%@ Page Title="Argix :: Transportation" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpContent" runat="server">
    <div class="header-transportation">
        <p><span class="headline">A precision network<br />delivering </span><span class="headline-red">increased</span><span class="headline"><br /></span><span class="headline-red">value</span></p>
    </div>
    <div class="content">
        <p class="body-nav">Home > Transportation &gt;<span class="sub-nav-highlight"> Nationwide B2B Delivery</span></p> 
        <span class="big_caps">N</span><span class="titles">ationwide B2B Delivery.</span>
        <p class="body-copy">Argix Logistics provides nationwide B2B delivery solutions customized to improve efficiency and 
        eliminate risks. Our national footprint of 43 terminals gives you precision delivery with reduced handling and cost.<br /></p>
        <p class="body-copy">Argix Logistics can move your freight from single or multiple pick up/induct points, bypassing 
        some or all DC operations and combining everything into a single delivery and billing. The Argix Network enables day 
        and time definite delivery affording you with predictable planning and reliable execution. Flexible options such as EDI, 
        zone skipping, drop trailer/yard management and a customer-tailored tracking and reporting package all add up to increased 
        value.</p>
    </div>
    <div class="side_bar_b2b">
        <p class="sub-nav">
            <span class="sub-nav-bigger"><a href="<%: Url.Action("transportation", "Home")%>">Transportation</a></span><br />
            <span class="sub-nav-highlight">Nationwide B2B Delivery</span><br />
            <a href="<%: Url.Action("nationwideB2Cdelivery", "Home")%>">Nationwide B2C Delivery</a><br />
            <a href="<%: Url.Action("DCbypass", "Home")%>">DC Bypass</a><br />
            <a href="<%: Url.Action("LTDdelivery", "Home")%>">LTL Delivery</a><br />
            <a href="<%: Url.Action("regionaldelivery", "Home")%>">Regional Delivery</a>
        </p>
    </div>
</asp:Content>
