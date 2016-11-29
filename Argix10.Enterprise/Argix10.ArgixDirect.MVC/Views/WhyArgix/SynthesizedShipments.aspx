<%@ Page Language="C#" MasterPageFile="~/Views/WhyArgix/WhyArgix.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="MetaContent">
    <meta name="keywords" content=" synthesized shipment" />
    <meta name="description" content="Argix Direct provides synthesized shipments."/>
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="PageContent">
	<div class="textContainer"> 
	    <h4> SYNTHESIZED SHIPMENTS </h4>
        <br />
        <img src="<%= Url.Content("~/content/images/synthesize.jpg") %>" alt="Synthesized Shipments" width="500" />
    </div>
</asp:Content>

