<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="CTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Argix Direct - Thank You
</asp:Content>
<asp:Content ID="cTop" ContentPlaceHolderID="TopBannerContent" runat="server">
    <img src="<%= Url.Content("~/Content/Images/topBanner.jpg") %>"  alt="Banner." width="960" height="63" />
</asp:Content><asp:Content ID="cSub" ContentPlaceHolderID="SubBannerContent" runat="server">
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="MainContent" runat="server">
	<div id="navbarHoriz"></div>
    <div class="textContainer"> 
        <h4>  Thank you for contacting us. </h4>
        <p>     We will get back to you  soon  with the requested information. </p>
        <p>     Phone: 732.656.2550 </p>
        <p>     Email: info@argixdirect.com </p>

</div>	  <!--textContainer-->    
</asp:Content>