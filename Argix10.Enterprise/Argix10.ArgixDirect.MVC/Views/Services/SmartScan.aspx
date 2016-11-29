<%@ Page Language="C#" MasterPageFile="~/Views/Services/Services.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="MetaContent">
    <meta name="keywords" content=" Smartscan, barcode, shipment integrity, zone, delivery zone, sort, routing, track" />
    <meta name="description" content=" Argix Direct utilizes Smartscan technology to track individual packages throughout the sorting and delivery system."/>
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="PageContent">
	<div class="textContainer"> 
	    <h4> SMARTSCAN </h4>
        <br />
        <img src="<%= Url.Content("~/content/images/scanlabel.jpg") %>" alt="Smart Scan" width="600" />
    </div>  
</asp:Content>

