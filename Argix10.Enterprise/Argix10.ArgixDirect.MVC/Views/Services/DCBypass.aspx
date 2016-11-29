<%@ Page Language="C#" MasterPageFile="~/Views/Services/Services.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="MetaContent">
    <meta name="keywords" content="logistics, Distribution Center, DC bypass, DC, bypass " />
    <meta name="description" content="Argix Direct supports your logistics needs through bypassing the Distribution Center.  We have the ability to receive your freight directly into any of our terminals nationwide. We also have the expertise to manage receiving from multiple sources."/>
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="PageContent">
	<img class="imgFloatRight" src="<%= Url.Content("~/content/images/dcbypass.jpg") %>"  alt="DC Bypass" width="240" />
	<div class="textContainer"> 
        <h4> DC BYPASS </h4>
        <p>Argix Direct has the expertise and the ability to support your logistics needs through bypassing the DC. This is often necessary during peak seasons or for specialized retail programs.</p>
        <p>We have the ability to receive your freight directly into any of our terminals nationwide. We also have the expertise to manage receiving from multiple points: Distribution Center, Imports, Storage Facilities, or Vendors. Argix Direct will consolidate these shipments in our National Sort Center and create a single store delivery.</p>
        <p>You always have the ability to track shipments through our proprietary <a href="<%: Url.Action("SmartScan", "Services")%>"><span class="redBoldLink">Smartscan</span> </a> technology.</p>
        <p>With our DC bypass service, you can free up space in your DC, minimize handling and lower your overall costs.<br />
        </p>
	</div>	  
</asp:Content>
