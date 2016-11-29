<%@ Page Title="Argix Direct - Location" Language="C#" MasterPageFile="~/Views/AboutUs/AboutUs.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="MetaContent">
    <meta name="keywords" content="Jamesburg, New Jersey, Distribution, Terminals" />
    <meta name="description" content="Argix Direct has over 15 years of specialized retail store deliveries and over 120 years of retail expertise. We operate 41 distribution terminals nationwide and one import deconsolidation center in North Bergen, NJ."/>
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="PageContent">
    <div class="textContainer">
        <h4>43 Distribution Terminals <strong>Nationwide</strong></h4>
        <br /><br />
        <a href="<%: Url.Action("LocationLargeMap","AboutUs")%>" target="_blank">
            <img src="<%= Url.Content("~/content/images/zonemap.gif") %>" alt="Terminal map" width="550" style=" border:2px solid #000000" />
        </a>
        <p>Click on map for larger view</p>
    </div>
</asp:Content>
