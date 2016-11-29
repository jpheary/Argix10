<%@ Page Title="Argix Direct - Smartscan" Language="C#" MasterPageFile="~/Areas/Mobile/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMain" ContentPlaceHolderID="MainContent" runat="server">
	<div id="back"><a href="javascript:history.go(-1)"><< Back</a></div>
    <div class="textContainer"> 
	    <h1>Smartscan</h1>
        <br />
        <img src="<%= Url.Content("~/content/images/scanlabel.jpg") %>" alt="Smart Scan" width="280" />
    </div>
</asp:Content>

