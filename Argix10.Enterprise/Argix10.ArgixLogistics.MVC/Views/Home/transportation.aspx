<%@ Page Title="Argix :: Transportation" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpContent" runat="server">
    <div class="header-transportation">
        <p><span class="headline">If there's a more direct<br />route to your destination,<br /></span><span class="headline-red">why not take it?</span></p>
    </div>
    <div class="content"> <span class="big_caps_t">T</span><span class="titles">here's a better way.</span>
        <p class="body-copy">Your delivery needs are unique. At Argix Logistics, we treat them that way. Our national and 
        regional transportation services are intended for flexibility. With Argix, you get a customized delivery solution 
        for reduced costs, fewer touches and less risk. </p>
        <p class="body-copy"> It just makes sense.  If there's a more direct route to your destination, why not take it?  
        We make your planning and execution easier with precision time and day definite capability. We save time and money 
        with DC Bypass, zone skip and other smart shipping, tracking and customer reporting options.</p>
        <p class="body-copy">Whether your needs are large or small, seasonal or year-around, simple or complex, count on 
        Argix Logistics for reliable transportation solutions customized to maximize efficiency and control costs.</p>
    </div>
    <div class="side_bar_trans">
        <p class="sub-nav">
            <span class="sub-nav-bigger-red">Transportation</span><br />
            <a href="<%: Url.Action("nationwideB2Bdelivery", "Home")%>">Nationwide B2B Delivery</a><br />
            <a href="<%: Url.Action("nationwideB2Cdelivery", "Home")%>">Nationwide B2C Delivery</a><br />
            <a href="<%: Url.Action("DCbypass", "Home")%>">DC Bypass</a><br />
            <a href="<%: Url.Action("LTDdelivery", "Home")%>">LTL Delivery</a><br />
            <a href="<%: Url.Action("regionaldelivery", "Home")%>">Regional Delivery</a>
        </p>
    </div>
</asp:Content>
