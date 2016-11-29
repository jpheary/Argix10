<%@ Page Language="C#" MasterPageFile="~/Views/WhyArgix/WhyArgix.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="MetaContent">
    <meta name="keywords" content="logistics, retailing, multiple deliveries, synthesized shipments, Smartscan, DC " />
    <meta name="description" content="Argix Direct provides customized logistics solutions for your specific retailing needs. We offer a lower cost solution by synthesizing shipments, eliminating multiple deliveries, and freeing up capacity in your DC."/>
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="PageContent">
	<img class="imgFloatRight" src="<%= Url.Content("~/content/images/whyargix.jpg") %>" alt="Why Argix?" width="240" />
	<div  class="textContainer"> 
        <p>Argix Direct will provide you with a fully customized logistics solution for your specific retailing needs. </p>
        <p>We provide a lower cost solution by synthesizing shipments, eliminating multiple deliveries, and freeing up capacity in your DC.</p>
        <p>Argix Direct has a proprietary Smartscan system that provides immediate and detailed shipment reportings. Our professional customer service representatives are always ready to support your logistical needs. </p>
	</div>	  
</asp:Content>
