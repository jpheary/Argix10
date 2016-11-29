<%@ Page Language="C#" MasterPageFile="~/Views/Services/Services.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="MetaContent">
    <meta name="keywords" content="logistics, mall, strip mall, outlet center,  expedited freight, non-expedited rates, SmartScan, EDI, drop trailers, zone skip " />
    <meta name="description" content="Argix Direct delivers in virtually every mall, strip, and outlet center throughout the country every day. This gives our retail clients expedited freight at non-expedited rates. Argix Direct also offers EDI capabilities, drop trailers, and zone skip.  We have 42 delivery terminals nationwide that support your time sensitive shipments. As always, you can fully track your shipments with our proprietary SmartScan technology."/>
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="PageContent">
	<img class="imgFloatRight" src="<%= Url.Content("~/content/images/deliveries.jpg") %>"  alt="Store Deliveries" width="240" />
	<div  class="textContainer"> 
        <h4> STORE DELIVERY</h4>
        <p>Our store delivery program is customized for the retailer, on either a regional or national basis. Argix Direct offers both DC pickup or bypass to consolidate all components into a single store delivery.</p>
        <p>Argix Direct delivers in virtually every mall, strip, and outlet center throughout the country every day. This gives our retail clients expedited freight at non-expedited rates. All Argix Direct deliveries are inside with consistent transit times– absolutely critical for retail.</p>
        <p>Argix Direct also offers EDI capabilities, drop trailers, and zone skip if necessary.</p>
        <p>We have 42 delivery terminals nationwide that support your time sensitive shipments. As always, you can fully track your shipments with our proprietary <a href="<%: Url.Action("SmartScan", "Services")%>"><span class="redBoldLink">Smartscan</span> </a> technology. Our goal is to support our clients with less internal handling and lower overall costs.</p>
	</div>
</asp:Content>
