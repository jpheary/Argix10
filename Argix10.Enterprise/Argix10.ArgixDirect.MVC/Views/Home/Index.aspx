<%@ Page Title="Argix Direct - Home" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="MetaContent">
    <meta name="keywords" content="logistics, services, solution, retailing" />
    <meta name="description" content="Argix Direct can develop a TOTAL Logistics solution that is right for your retailing needs."/>
</asp:Content>
<asp:Content ID="cLeft" runat="server" ContentPlaceHolderID="LeftContent">
    <div id="title">Home</div>
    <div id="submenu">
		<ul>
			<li><%: Html.ActionLink("Testimonials","Testimonials","Home")%></li>
		</ul>
	</div>
</asp:Content>
<asp:Content ID="cRight" runat="server" ContentPlaceHolderID="RightContent">
    <img src="<%= Url.Content("~/Content/Images/services.gif") %>" alt="Argix Services" />
</asp:Content>
