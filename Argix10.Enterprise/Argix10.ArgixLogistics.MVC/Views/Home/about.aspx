<%@ Page Title="Argix :: Difference" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpContent" runat="server">
    <div class="header-difference"><span class="headline">A long history of<br /></span><span class="headline-red">smart solutions</span></div>
    <div class="content"><p class="body-nav">Home > The Argix Network ><span class="sub-nav-highlight"> About Argix</span></p>
    <span class="big_caps">A</span><span class="titles">bout Argix</span>
        <p class="body-copy">With over 30 years of building our Network and servicing our long-term<br />
        customers, Argix Logistics has established itself as an expert logistics solution<br />
        provider.<br />
        <br />
        We have an outstanding reputation for developing successful strategic<br />
        partnerships to bolster our in-house capabilities and other outside<br />
        opportunities. Our flexible approach to business means we are able to<br />
        accommodate your specific needs and help you keep costs under control<br />
        without compromising the hard-earned reputation of your brand.<br />
        <br />
        With Argix, you can count on excellence every step of the way. From optimum<br />
        communication and team involvement to DC bypass and fewer &quot;touches&quot;<br />
        throughout the process. All supported by our tracking and reporting<br />
        technology which eliminates worries on your end.</p>
    </div>
    <div class="side_bar_about">
        <p class="sub-nav">
        <a href="<%: Url.Action("difference", "Home")%>"><span class="sub-nav-bigger">The Argix Difference</span></a><br />
        <a href="<%: Url.Action("network", "Home")%>">The Argix Network</a><br />
        <a href="<%: Url.Action("about", "Home")%>"><span class="sub-nav-highlight">About Argix</span></a></p>
    </div>
</asp:Content>
