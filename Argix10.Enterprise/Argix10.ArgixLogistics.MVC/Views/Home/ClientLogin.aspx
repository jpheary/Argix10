<%@ Page Title="Argix" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="MetaContent">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="MainContent" runat="server">
<div id="wrap">
    <div id="header"><a href="<%: Url.Action("ClientLogin", "Home")%>"><img src="<%= Url.Content("~/Content/Images/img_01.gif") %>" alt="carton tracking click here" width="240" height="64" style="border: 0;" /></a><a href="<%: Url.Action("ContactUs", "Home")%>"><img src="<%= Url.Content("~/Content/Images/img_02.gif") %>" alt="request information click here" width="240" height="64" style="border: 0;" /></a></div>
    <div id="contact"><span class="contact-top">For more information, contact us at 732.656.2550</span><br /></div>
    <div id="billboard">
        <div id="loginContent">
   	        <p>To access our LOGIN page you must allow POPUPS in your browser. </p>
	        <p>If you are not redirected to LOGIN when you click, please check your browser settings.  </p>
   	        <p>&nbsp;</p>
   	        <p>Thank you. </p>
        </div>
        <div id="loginNav"><a href="/tracking/members/default.aspx" target="_blank">Click For Login Page</a></div>
    </div>
    <div id="comingsoon">
        <p class="headline">New Website Coming Soon</p>
        <hr size="11" noshade="noshade" color="#ee2a24" /><br />
    </div>
    <div id="bottom_copy">
        <p class="body-copy">We are excited to inform you that we have changed our name from Argix Direct to Argix Logistics to <br />
        better communicate our new portfolio of services.
        </p>
        <div id="subnav"> 
            <p class="main-nav">&gt;  An &quot;a la carte&quot; approach to business
            </p>
            <p class="main-nav-sm">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Get ready to experience the Argix difference.</p>
            <p class="mich-sub">Transportation <br />
            Distribution <br />
            Logistics Management</p>
        </div>
        <div id="logo_space"><img src="<%= Url.Content("~/Content/Images/argix-logo.gif") %>" width="322" height="109" alt="Argix" /></div>
    </div>
</div>
</asp:Content>
