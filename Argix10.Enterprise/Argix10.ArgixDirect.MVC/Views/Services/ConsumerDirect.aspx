<%@ Page Language="C#" MasterPageFile="~/Views/Services/Services.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="MetaContent">
    <meta name="keywords" content="logistics, Consumer Direct, DDU, Destination Delivery Unit, package tracking, e-commerce, ecommerce " />
    <meta name="description" content="Argix Direct offers its Consumer Direct program as a solution for your catalog, e-commerce, seasonal and small parcel shipping and delivery needs to the consumer."/>
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="PageContent">
	<img class="imgFloatRight" src="<%= Url.Content("~/content/images/house.jpg") %>" alt="Consumer Direct." width="240" />
	<div  class="textContainer"> 
        <h4> CONSUMER DIRECT </h4>
        <p>The Argix Direct – Consumer Direct  program offers a solution for your catalog, e-commerce, seasonal and small  parcel shipping and delivery needs to the consumer.</p>
        <p>Working with the United  States Postal Service system at both the BMC (Bulk Mail Center) and DDU  (Destination Delivery Unit) level, Argix Direct can leverage its current  network to provide an efficient and cost effective program for you.</p>
        <p>The Argix Direct – Consumer  Direct program can provide you with a customized solution that provides the  same level of system support, package tracking, security and customer service  that our current customers enjoy with our other service offerings.</p>
      <p>&nbsp;</p>
    </div>	  
</asp:Content>
