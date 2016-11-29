<%@ Page Language="C#" MasterPageFile="~/Views/WhyArgix/WhyArgix.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="MetaContent">
    <meta name="keywords" content=" customer, customer service, customer service representative, Smartscan, tracking, reporting, shipment details" />
    <meta name="description" content=" Argix Direct provides dedicated service representatives to support your business needs."/>
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="PageContent">
	<img class="imgFloatRight" src="<%= Url.Content("~/content/images/customerservice.jpg") %>" alt="Customer Service." width="240" />
	<div  class="textContainer"> 
        <h4>CUSTOMER SERVICE</h4>
        <h5>Argix Direct addresses our customer’s needs from every angle</h5>
        <ul>
            <li>System designed for the needs of our customers</li>
            <li>Dedicated Service Representatives</li>
            <li>Track packages using your internal numbers</li>
            <li>Easily access snapshots of truck loads, all the way down to specific packages</li>
            <li>Know and rely on specific shipment details</li>
            <li>Customized reporting</li>
            <li>Advanced CRM software</li>
            <li>Online Tracking</li>
        </ul>
	</div>	  
</asp:Content>
