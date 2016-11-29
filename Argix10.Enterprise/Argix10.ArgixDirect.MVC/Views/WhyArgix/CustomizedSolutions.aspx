<%@ Page Language="C#" MasterPageFile="~/Views/WhyArgix/WhyArgix.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="MetaContent">
    <meta name="keywords" content="specialty retail store delivery, specialty retail store, delivery, Handling, Flexibility" />
    <meta name="description" content="Argix Direct provides a customized technology-driven design for specialty retail store deliveries."/>
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="PageContent">
	<img class="imgFloatRight" src="<%= Url.Content("~/content/images/solutions.jpg") %>" alt="Customized Solutions." width="240" />
	<div  class="textContainer"> 
        <h4>CUSTOMIZED SOLUTIONS</h4>
        <h4>Shrink Your Supply Chain<br /><span class="redText">Expand Your Competitive Edge</span></h4>
        <img src="<%= Url.Content("~/content/images/blackbar.jpg") %>" alt="" width="225" height="3" />
        <p class="style1">Exclusive technology-driven design for specialty retail store deliveries:</p>
        <ul>
            <li>Custom Store Delivery Program</li>
            <li>Increased Accuracy</li>
            <li>Expedited Transit Times</li>
            <li>Efficient Handling</li>
            <li>Greater Flexibility</li>
            <li>Lower Cost</li>
            <li><a href="<%: Url.Action("SynthesizedShipments", "WhyArgix")%>"><span class="redBoldLink">Synthesized Shipments</span></a></li>
        </ul>
	</div>	  
</asp:Content>
