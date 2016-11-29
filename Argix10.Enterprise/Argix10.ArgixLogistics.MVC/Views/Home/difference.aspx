<%@ Page Title="Argix :: Difference" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpContent" runat="server">
    <div class="header-difference"><span class="headline">An </span><span class="headline-red">a la carte</span><span class="headline"> approach<br />to business</span></div>
    <div class="content_sm">
        <p class="body-nav">Home ><span class="sub-nav-highlight"> The Argix Difference</span></p><span class="big_caps_t">T</span><span class="titles">he Argix Difference</span>
        <p class="body-copy-sm">Our comprehensive selection of transportation, distribution and supply chain management services allows us to mix and match options so we can develop a program that has everything you want and nothing you don't.</p>
        <p class="body-copy-sm">You'll enjoy a solution designed for your unique business model – with exceptional reliability and performance to help you reduce costs, increase efficiency and eliminate risks. That means you no longer have to worry about hidden costs that can wreak havoc on your bottom line. Argix positions your business to broaden your supply chain options without investment, strengthening your company and allowing you to focus on growth.</p>
    </div>
    <div class="side_bar_difference">
        <p class="sub-nav-wide">
            <a href="<%: Url.Action("difference", "Home")%>"><span class="sub-nav-bigger-red">The Argix Difference</span></a><br />
            <a href="<%: Url.Action("network", "Home")%>">The Argix Network</a><br />
            <a href="<%: Url.Action("about", "Home")%>">About Argix</a>
        </p>
    </div>
</asp:Content>
