<%@ Page Title="Argix :: Difference" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpContent" runat="server">
    <div class="header-difference"><span class="headline">A network of choices<br />designed for<br /></span><span class="headline-red">your success</span></div>
    <div class="content_sm">
        <p class="body-nav">Home > The Argix Difference &gt;<span class="sub-nav-highlight"> The Argix Network</span></p>
        <span class="big_caps_t">T</span><span class="titles">he Argix Network</span>
        <p class="body-copy-sm">The Argix Network is a dynamic interaction of terminals, systems, expertise on a global platform. Linked by integrated smart systems to international supply chains, the Argix Network provides precision delivery and distribution solutions for a wide range of B2B and B2C customers.</p>
        <p class="body-copy-sm">We're experts at creating effective strategies and forming strong partnerships with our clients. Our collaborative approach allows you to select the Network in its entirety or in its many individual components – you choose what you need and we deliver.</p>
    </div>
    <div class="side_bar_network">
        <p class="sub-nav-wide">
            <a href="<%: Url.Action("difference", "Home")%>"><span class="sub-nav-bigger">The Argix Difference</span></a><br />
            <a href="<%: Url.Action("network", "Home")%>"><span class="sub-nav-highlight">The Argix Network</span></a><br />
            <a href="<%: Url.Action("about", "Home")%>">About Argix</a>
        </p>
    </div>
</asp:Content>
