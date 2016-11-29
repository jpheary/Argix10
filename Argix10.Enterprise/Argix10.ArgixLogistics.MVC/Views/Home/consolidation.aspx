<%@ Page Title="Argix :: Supply Chain Management" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpContent" runat="server">
    <div class="header-distribution"><p><span class="headline">Engineering the</span><span class="headline"><br />
        </span><span class="headline-red">inbound flow</span></p>
    </div>
    <div class="content">
        <p class="body-nav">Home > Distribution &gt;<span class="sub-nav-highlight"> Consolidation</span></p>
        <span class="big_caps">C</span><span class="titles">onsolidation</span>
        <p class="body-copy">Consolidation is the intersection that links supply and demand. When designed correctly it 
        can assist the warehouse storage, picking, processing and outbound shipping functions. In addition, a strong 
        Consolidation program is a key element of DC Bypass. It's all about improving the flow.<br /><br />Argix Logistics 
        has the Network transportation and IT resources to connect your supply to demand giving you control over the vendor 
        process and the visibility to better schedule and plan vital 4-wall activities. From P.O. validation to line item 
        sort and load, Argix has the experience to manage vendor activity so that you can concentrate on improved utilization 
        of labor and facilities.<br />
        <br />Like all of our services Argix gives you the option of tying this service into our national network offering 
        efficiency and savings in vendor pickup, sorting, line haul and delivery at final destination.<br /></p>
    </div>
    <div class="side_bar_consolidation">
        <p class="sub-nav">
            <a href="<%: Url.Action("distribution", "Home")%>" class="sub-nav-bigger">Distribution</a><br />
            <a href="<%: Url.Action("sortingservies", "Home")%>">Sorting Services</a><br />
            <span class="sub-nav-highlight">Consolidation</span><br />
            <a href="<%: Url.Action("fulfillmentservices", "Home")%>">Fulfillment Services</a><br />
            <a href="<%: Url.Action("warehousing", "Home")%>">Warehousing</a>
        </p>
    </div>
</asp:Content>
