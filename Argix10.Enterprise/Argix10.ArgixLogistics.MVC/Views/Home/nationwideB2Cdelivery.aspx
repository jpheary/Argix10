<%@ Page Title="Argix :: Transportation" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpContent" runat="server">
    <div class="header-transportation">
        <p><span class="headline">Where your customer<br /></span><span class="headline-red">is at home</span></p>
    </div>
    <div class="content">
        <p class="body-nav">Home > Transportation &gt;<span class="sub-nav-highlight"> Nationwide B2C Delivery</span></p> 
        <span class="big_caps">N</span><span class="titles">ationwide B2C Delivery.</span>
        <p class="body-copy">Tired of the same old small package delivery choices? There is another way!<br /></p>
        <p class="body-copy">Argix Logistics offers customized direct-to-consumer delivery solutions that save time, limit 
        handling and control costs. Whether it's catalog, ecommerce, seasonal or small parcel shipping and delivery, you'll 
        get the reliability, security and system support that's synonymous with the Argix name.</p>
        <p class="body-copy">
            We combine our work-share partnership and certifications with the U.S. Postal<br />
            Service with our other specialized delivery services to leverage better efficiency, flexibility and savings for 
            your direct-to-consumer delivery needs. Your customers will enjoy full parcel tracking, a convenience expected 
            by today's sophisticated consumer.</p>
    </div>
    <div class="side_bar_b2c">
        <p class="sub-nav">
            <span class="sub-nav-bigger"><a href="<%: Url.Action("transportation", "Home")%>">Transportation</a></span><br />
            <a href="<%: Url.Action("nationwideB2Bdelivery", "Home")%>">Nationwide B2B Delivery</a><br />
            <span class="sub-nav-highlight">Nationwide B2C Delivery</span><br />
            <a href="<%: Url.Action("DCbypass", "Home")%>">DC Bypass</a><br />
            <a href="<%: Url.Action("LTDdelivery", "Home")%>">LTL Delivery</a><br />
            <a href="<%: Url.Action("regionaldelivery", "Home")%>">Regional Delivery</a>
        </p>
    </div>
</asp:Content>
