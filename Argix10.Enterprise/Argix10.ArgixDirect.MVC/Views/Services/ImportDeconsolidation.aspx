<%@ Page Language="C#" MasterPageFile="~/Views/Services/Services.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="MetaContent">
    <meta name="keywords" content="logistics, Import Deconsolidation, Import, Deconsolidation" />
    <meta name="description" content="Argix Direct is strategically located near New Jersey Port to offer exceptional support for our client's Import Deconsolidation needs. Our sorting facility is fully bonded and gives you the capability to consolidate multiple shipments."/>
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="PageContent">
	<img class="imgFloatRight" src="<%= Url.Content("~/content/images/deconsolidation.jpg") %>" alt="Import Deconsolidation" width="240" />
	<div  class="textContainer"> 
        <h4> IMPORT DECONSOLIDATION</h4>
        <p>With Argix Direct’s strategic location near the New Jersey Port, we can offer exceptional support for your Import Deconsolidation needs. Our sorting facility  gives you the capability to consolidate multiple shipments.</p>
        <p>As always, you can fully track your shipments with our proprietary <a href="<%: Url.Action("SmartScan", "Services")%>"><span class="redBoldLink">Smartscan</span> </a> technology. Our goal is to support our clients with less internal handling and reduce overall costs.</p>
	</div>	  
</asp:Content>
