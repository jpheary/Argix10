<%@ Page Title="Argix Logistics Carton Tracking" Language="C#" MasterPageFile="~/Views/Shared/TrackingSite.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cpBody" runat="server">
    <div id="tracking">
         <a href="<%: Url.Action("Tracking", "Tracking")%>" title="Tracking">
            <img id="imgTracking" src="<%= Url.Content("~/Content/Images/tracking.gif") %>" alt="Tracking" style="border: 0;" />
        </a>   
    </div>
    <div id="reports">
        <a href="<%: Url.Action("Reports", "Tracking")%>" title="Reports">
            <img id="imgReports" src="<%= Url.Content("~/Content/Images/reports.jpg") %>" alt="Reports" style="border: 0;" />
        </a>   
    </div>
    <div id="profile">
        <a href="<%: Url.Action("Profile", "Tracking")%>" title="Profile">
            <img id="imgProfile" src="<%= Url.Content("~/Content/Images/profile.jpg") %>" alt="Profile" style="border: 0;" />
        </a>   
    </div>
</asp:Content>


