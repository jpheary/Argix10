<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Mobile/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="cMain" ContentPlaceHolderID="MainContent" runat="server">
    <div style=" text-align:center">
        <img src="<%= Url.Content("~/Content/Images/services.gif") %>" width="270px" alt="Argix Services" />
    </div>
    <div id="address">100 Middlesex Center Blvd. Jamesburg, NJ 08831<br />732-656-2550</div>
</asp:Content>
