<%@ Page Language="C#" MasterPageFile="~/Views/Services/Services.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="MetaContent">
    <meta name="keywords" content="logistics, consolidation, shipments, SmartScan" />
    <meta name="description" content="Argix Direct specializes in single store deliveries from multiple shipments. Consolidation can save money, improve efficiency and reduce paperwork.  We give you the ability to track shipments through our proprietary SmartScan technology."/>
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="PageContent">
	<img class="imgFloatRight" src="<%= Url.Content("~/content/images/consolidation.jpg") %>" alt="Consolidation" width="240" />
	<div  class="textContainer"> 
        <h4> CONSOLIDATION </h4>
        <p>Argix Direct specializes in single store deliveries from multiple shipments. We have extensive experience in managing multiple points (DC, vendors, imports, storage) into single deliveries. This helps to lower costs for shipments.</p>
        <p>Consolidation also provides in-store cost savings by improved scheduling, reduced paperwork and shrink potential.</p>
        <p>Additionally, Argix Direct’s expertise in consolidation helps our clients free up space in their DC’s.</p>
        <p>You always have the ability to track shipments through our proprietary <a href="<%: Url.Action("SmartScan", "Services")%>"><span class="redBoldLink">Smartscan</span> </a> technology.<br />
        </p>
	</div>	  
</asp:Content>
