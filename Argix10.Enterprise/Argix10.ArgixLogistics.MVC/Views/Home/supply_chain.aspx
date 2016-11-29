<%@ Page Title="Argix :: Supply Chain Management" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpContent" runat="server">
    <div class="header-supply-chain">
        <p><span class="headline-red">Improving performance</span><span class="headline"><br /></span><span class="headline">at each step</span></p>
    </div>
    <div class="content">
        <p class="body-nav">Home ><span class="sub-nav-highlight"> Supply Chain Management</span></p>
          <span class="big_caps">S</span><span class="titles">upply Chain Management</span>
              <p class="body-copy">Through strategic partnerships, Argix Logistics offers full connectivity to the
                global supply chain. Sharp negotiation and strong relationships provide our
                clients with flexible choices and up-to-the-moment industry updates to help
                guide the decision making process in their favor. Whether looking for expertise
                that does not reside in-house or looking to gain additional opportunity with
                those resources, our Supply Chain Management Program can play a vital role in
                developing or improving supply chain performance.<br />
                <br />
                At Argix Logistics, we leverage our alliances with strategic partners to provide<br />
              organizations with results that meet or exceed expectations.</p>
    </div>
    <div class="side_bar_supply">
        <p class="sub-nav">
            <span class="sub-nav-bigger-red">Supply Chain <br />Management</span><br />
            <a href="<%: Url.Action("international", "Home")%>">International Consolidation</a><br />
            <a href="<%: Url.Action("ocean-freight", "Home")%>">Ocean Freight</a><br />
            <a href="<%: Url.Action("air-freight", "Home")%>">Air Freight</a><br />
            <a href="<%: Url.Action("customs-brokerage", "Home")%>">Customs Brokerage</a><br />
            <a href="<%: Url.Action("drayage", "Home")%>">Drayage</a><br />
            <a href="<%: Url.Action("domestic-deconsolidation", "Home")%>">Domestic Deconsolidation</a>
        </p>
    </div>
</asp:Content>
