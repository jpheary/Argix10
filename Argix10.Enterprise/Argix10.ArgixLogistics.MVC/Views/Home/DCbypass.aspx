<%@ Page Title="Argix :: Transportation" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpContent" runat="server">
    <div class="header-transportation">
        <p><span class="headline">Now it's </span><span class="headline-red">your turn</span><span class="headline"><br />to maxim ize profits</span></p>
    </div>
    <div class="content">
        <p class="body-nav">Home > Transportation &gt;<span class="sub-nav-highlight"> DC Bypass</span></p> 
        <span class="big_caps">D</span><span class="titles">C Bypass</span>
        <p class="body-copy">Argix Logistics' DC Bypass services are all about moving your products to market faster. When 
        you bypass the distribution center, your products remain in motion, allowing them to reach their destination point 
        quicker and with fewer hassles.This means reduced labor; which minimizes handling that can relate to loss or damage 
        and overall transportation costs.<br /></p>
        <p class="body-copy">Our experts are ready to develop a unique solution for you based on your<br />allocation windows, 
        merchandising expectations, financial concerns regarding<br />ownership and more effective inventory turn.<br /></p>
        <p class="body-copy">In the end, it's all about higher margins based on turn.</p>
  </div>
    <div class="side_bar_dc">
        <p class="sub-nav">
            <span class="sub-nav-bigger"><a href="<%: Url.Action("transportation", "Home")%>">Transportation</a></span><br />
            <a href="<%: Url.Action("nationwideB2Bdelivery", "Home")%>">Nationwide B2B Delivery</a><br />
            <a href="<%: Url.Action("nationwideB2Cdelivery", "Home")%>">Nationwide B2C Delivery</a><br />
            <span class="sub-nav-highlight">DC Bypass</span><br />
            <a href="<%: Url.Action("LTDdelivery", "Home")%>">LTL Delivery</a><br />
            <a href="<%: Url.Action("regionaldelivery", "Home")%>">Regional Delivery</a>
        </p>
    </div>
</asp:Content>
