<%@ Page Language="C#" MasterPageFile="~/Views/Services/Services.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="MetaContent">
    <meta name="keywords" content="logistics, services, solution, retailing" />
    <meta name="description" content="Argix Direct can develop a TOTAL Logistics solution that is right for your retailing needs."/>
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="PageContent">
    <img class="imgFloatRight" src="<%= Url.Content("~/content/images/services.gif") %>" alt="Services" width="300" height="300"/>
	<div  class="textContainer"> 
        <p> Argix Direct will be your partner in developing and supporting a TOTAL Logistics solution that is right for you. We develop a customized solution that fits your retailing needs. At Argix Direct, we have extensive experience in both retail and logistics. We will provide you with support from Line Haul to DC bypass; from Import Deconsolidation to Storage - tailored to your needs.</p>
        <p>Contact us to see how Argix Direct can help develop your TOTAL Logistics Solution. </p>
    </div>	  
</asp:Content>
