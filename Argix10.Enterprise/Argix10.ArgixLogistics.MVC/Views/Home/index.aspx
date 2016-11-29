<%@ Page Title="Argix Logistics" Language="C#" MasterPageFile="~/Views/Shared/Index.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpContent" runat="server">
    <div class="header-home"><span class="headline"></span><span class="headline-red"></span></div>
    <div align="center">
        <a href="<%: Url.Action("Transportation", "Home")%>"><img src="<%= Url.Content("~/Content/Images/index-buttons_01.jpg") %>" width="284" height="158" alt="Transportation" /></a>
        <a href="<%: Url.Action("Distribution", "Home")%>"><img src="<%= Url.Content("~/Content/Images/index-buttons_02.jpg") %>" width="295" height="158" alt="Distribution" /></a>
        <a href="<%: Url.Action("Supply_chain", "Home")%>"><img src="<%= Url.Content("~/Content/Images/index-buttons_03.jpg") %>" width="285" height="158" alt="Supply Chain Mgmt" /></a>
    </div>
    <div class="content-home">
        <span class="titles-home">Welcome to Argix Logistics</span>
        <p class="body-copy"><br />Get ready for a faster, smarter and more affordable way to meet your company's logistics needs. 
            At Argix Logistics, we stand apart in our ability to customize transportation, distribution and logistics management 
            services.<br /><br />With our unique combination of better flexibility, more options and a reliable national and regional 
            delivery network, you get streamlined logistic solutions that have everything you want, and nothing you don't. In fact, 
            it's this &quot;one-size-doesn't-fit-all-approach&quot; that's earned us the loyalty of many world-renowned brands.<br />
            <br />There's a better way to get it there. Let us show you how.
        </p>
        <p></p>
    </div>
</asp:Content>
