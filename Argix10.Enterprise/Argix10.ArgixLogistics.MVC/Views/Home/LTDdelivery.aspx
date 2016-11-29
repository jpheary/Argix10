<%@ Page Title="Argix :: Transportation" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpContent" runat="server">
    <div class="header-transportation">
        <p><span class="headline-red">Optim ization</span><span class="headline"><br /> is the LTL advantage</span></p>
    </div>
    <div class="content">
        <p class="body-nav">Home > Transportation &gt;<span class="sub-nav-highlight"> LTL Delivery</span></p> 
        <span class="big_caps">L</span><span class="titles">TL Delivery (Pallet Shipment Program )</span>
        <p class="body-copy">Argix Logistics' pallet shipment program has redefined the traditional LTL (less-than-truckload) industry. Argix optimizes its network capabilities and offers attractive pricing with superior services for pallet LTL shipments. We customize pricing based on your requirements. We maintain entry-level pricing with better-than-standard transits that rival the industry's service without the inflated cost.</p>
    </div>
    <div class="side_bar_ltl">
        <p class="sub-nav">
            <span class="sub-nav-bigger"><a href="<%: Url.Action("transportation", "Home")%>">Transportation</a></span><br />
            <a href="<%: Url.Action("nationwideB2Bdelivery", "Home")%>">Nationwide B2B Delivery</a><br />
            <a href="<%: Url.Action("nationwideB2Cdelivery", "Home")%>">Nationwide B2C Delivery</a><br />
            <a href="<%: Url.Action("DCbypass", "Home")%>">DC Bypass</a><br />
            <span class="sub-nav-highlight">LTL Delivery</span><br />
            <a href="<%: Url.Action("regionaldelivery", "Home")%>">Regional Delivery</a>
        </p>
    </div>
</asp:Content>
