<%@ Page Title="Argix :: Distribution" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpContent" runat="server">
    <div class="header-distribution">
        <p><span class="headline">Our space is<br /></span><span class="headline-red">your space</span></p>
    </div>
    <div class="content">
        <p class="body-nav">Home ><span class="sub-nav-highlight"> Warehousing</span></p>
        <span class="big_caps">W</span><span class="titles">arehousing</span>
        <p class="body-copy">Expand your warehouse footprint where and when necessary by utilizing the<br />Argix Network.<br />
        <br />Whatever your warehousing needs, big or small, short term or long term, you can rely on Argix Logistics. Whether 
        its new store openings, bulk purchases, seasonal increases or timed released product we have the ability to support 
        your needs.<br /><br />Like all of our services Argix gives you the option of tying this service into our national 
        network offering efficiency and savings in vendor pickup, sorting, line haul and delivery at final destination.</p>
  </div>
    <div class="side_bar_warehousing">
        <p class="sub-nav">
            <a href="<%: Url.Action("distribution", "Home")%>" class="sub-nav-bigger">Distribution</a><br />
            <a href="<%: Url.Action("sortingservies", "Home")%>">Sorting Services</a><br />
            <a href="<%: Url.Action("consolidation", "Home")%>">Consolidation</a><br />
            <a href="<%: Url.Action("fulfillmentservices", "Home")%>">Fulfillment Services</a><br />
            <span class="sub-nav-highlight">Warehousing</span>
        </p>
    </div>
</asp:Content>
