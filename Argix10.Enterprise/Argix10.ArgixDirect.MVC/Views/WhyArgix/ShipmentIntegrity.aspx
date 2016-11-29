<%@ Page Language="C#" MasterPageFile="~/Views/WhyArgix/WhyArgix.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="MetaContent">
    <meta name="keywords" content=" shipment, shipment integrity, security, VisTrak, Visitor Management, biometric" />
    <meta name="description" content=" Argix Direct provides shipment integrity and security with VisTrak, a biometric Visitor Management solution."/>
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="PageContent">
	<img class="imgFloatRight" src="<%= Url.Content("~/content/images/integrity.jpg") %>" alt="Shipment Integrity" width="240" />
	<div  class="textContainer"> 
        <h4>SHIPMENT INTEGRITY &amp; SECURITY</h4>
        <p>The VisTrak Visitor Management Solution is a biometric system that Argix Direct uses to ensure security for shipments and safety for drivers. This program keeps track of who is entering or exiting an Argix Direct facility and verifies that access is permitted, all with the touch of a finger. This results in enhanced security along with faster entry and egress times. </p>
        <p>The <a href="<%: Url.Action("SmartScan", "WhyArgix")%>"><span class="redBoldLink">Smartscan</span> </a> System keeps track of individual packages throughout the sorting and delivery system. This system helps to make sure that shipments are both timely and accurate. Each package can be tracked either using your own or Argix Direct’s item/code number.<br />
      </p>
	</div>	  
</asp:Content>
