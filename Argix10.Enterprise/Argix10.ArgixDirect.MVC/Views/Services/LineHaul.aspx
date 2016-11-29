<%@ Page Language="C#" MasterPageFile="~/Views/Services/Services.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="MetaContent">
    <meta name="keywords" content="logistics, carriers, regional delivery, national delivery, line haul, competitive rates" />
    <meta name="description" content="Argix Direct utilizes an extensive network of carriers for regional and national delivery to provide our clients with line haul services at competitive rates."/>
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="PageContent">
	<img class="imgFloatRight" src="<%= Url.Content("~/content/images/linehaul.jpg") %>"  alt="Line Haul" width="240" />
	<div  class="textContainer"> 
        <h4> LINE HAUL</h4>
        <p>At Argix Direct we pride ourselves on our long-standing relationships with an extensive network of carriers. This provides our clients with excellent service for their store delivery needs.</p>
        <p>Argix Direct has extensive regional and national capabilities, assuring our clients the line haul support that they deserve at very competitive rates.</p>
	</div>	  
</asp:Content>
