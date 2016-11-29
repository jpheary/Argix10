<%@ Page Title="Argix :: Distribution" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpContent" runat="server">
    <div class="header-distribution">
        <p><span class="headline">You can get there</span><span class="headline-red"></span><span class="headline"><br /></span><span class="headline-red">from here</span></p>
    </div>
    <div class="content">
        <p class="body-nav">Home ><span class="sub-nav-highlight"> Distribution</span></p>
        <span class="big_caps">D</span><span class="titles">istribution</span>
        <p class="body-copy">You know that distribution is much more than the process of getting an object<br />from one place 
        to another. Whether your needs are simple or complex, the<br />success of your supply chain depends on well-executed 
        distribution services.</p>
        <p class="body-copy">Argix Logistics' Distribution solutions are designed to bring simplicity and order<br />to even 
        the most challenging supply chain management scenarios. Our streamlined, efficient sorting, warehousing, fulfillment 
        and related services get your products to market faster, resulting in reduced costs, better productivity<br />
        and improved customer satisfaction.</p>
    </div>
    <div class="side_bar_distribution">
        <p class="sub-nav">
            <span class="sub-nav-bigger-red">Distribution</span><br />
            <a href="<%: Url.Action("sortingservies", "Home")%>">Sorting Services</a><br />
            <a href="<%: Url.Action("consolidation", "Home")%>">Consolidation</a><br />
            <a href="<%: Url.Action("fulfillmentservices", "Home")%>">Fulfillment Services</a><br />
            <a href="<%: Url.Action("warehousing", "Home")%>">Warehousing</a>
        </p>
    </div>
</asp:Content>
