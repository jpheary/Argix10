<%@ Page Title="Argix :: Supply Chain Management" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpContent" runat="server">
    <div class="header-distribution"><p><span class="headline">Program s designed<br />for your company's<br />
        </span><span class="headline-red">unique fulfillment needs</span></p>
    </div>
    <div class="content">
        <p class="body-nav">Home > Distribution &gt; <span class="sub-nav-highlight"> Fulfillment Services</span></p>
        <span class="big_caps">F</span><span class="titles">ulfillment Services</span>
        <p class="body-copy">However simple or complex your company's fulfillment needs, Argix Logistics has the capability 
        within our Network to deliver a streamlined solution with speed, reliability and accuracy. Our flexibility offers 
        you a cost effective solution that fits your company's unique fulfillment needs.<br /><br />With Argix, you get the 
        flexibility of choosing a footprint and WMS solution that's right for your brand, while enjoying our fully secured 
        facility in Jamesburg, New Jersey. Argix also provides complete yard management for your inbound and outbound trailers.</p>
    </div>
    <div class="side_bar_fulfillment">
        <p class="sub-nav">
            <a href="<%: Url.Action("distribution", "Home")%>" class="sub-nav-bigger">Distribution</a><br />
            <a href="<%: Url.Action("sortingservies", "Home")%>">Sorting Services</a><br />
            <a href="<%: Url.Action("consolidation", "Home")%>">Consolidation</a><br />
            <span class="sub-nav-highlight">Fulfillment Services</span><br />
            <a href="<%: Url.Action("warehousing", "Home")%>">Warehousing</a>
        </p>
    </div>
</asp:Content>
