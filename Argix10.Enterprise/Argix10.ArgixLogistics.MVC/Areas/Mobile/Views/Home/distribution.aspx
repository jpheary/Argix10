<%@ Page Title="Argix :: Distribution" Language="C#" MasterPageFile="~/Areas/Mobile/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpContent" runat="server">
    <div class="header-distribution">
        <span class="headline">You can get there<br /></span><span class="headline-red">from here</span>
    </div>
    <div class="content">
        <span class="big_caps">D</span><span class="titles">istribution</span>
        <p class="body-copy">You know that distribution is much more than the process of getting an object from one place 
        to another. Whether your needs are simple or complex, the success of your supply chain depends on well-executed 
        distribution services.</p>
        <p class="body-copy">Argix Logistics' Distribution solutions are designed to bring simplicity and order to even 
        the most challenging supply chain management scenarios. Our streamlined, efficient sorting, warehousing, fulfillment 
        and related services get your products to market faster, resulting in reduced costs, better productivity 
        and improved customer satisfaction.</p>
    </div>
    <a class="pageMenu" href="javascript: unhide('sorting');">Sorting Services</a>
    <div id="sorting" class="hidden">
        <div class="content">
            <p class="body-copy">When capacity in your facility becomes an issue Argix Logistics can help. By 
            using our Sort Services you can reclaim valuable real estate in your DC, avoid large capital investment, 
            and increase carton sort accuracy.</p>
            <p class="body-copy">
            The Argix Logistics 325,000 sq ft national sort center in Jamesburg, NJ is equipped with 2 High-Speed Carton 
            Sorters and a 3rd Carton sorting system for smaller parcels. Our IT system is capable of receiving multiple 
            file and EDI types and has the ability to scan a variety of (label) barcodes.</p>
            <p class="body-copy">
            The Argix Sorting Services can be used to support a client's own delivery system or cartons can induct directly 
            into the Argix national Network to their final destination.</p>
        </div>
    </div>
    <a class="pageMenu" href="javascript: unhide('consolidation');">Consolidation</a>
    <div id="consolidation" class="hidden">
        <div class="content">
            <p class="body-copy">Consolidation is the intersection that links supply and demand. When designed correctly it 
            can assist the warehouse storage, picking, processing and outbound shipping functions. In addition, a strong 
            Consolidation program is a key element of DC Bypass. It's all about improving the flow.<br />
            <br />
            Argix Logistics has the Network transportation and IT resources to connect your supply to demand giving you control 
            over the vendor process and the visibility to better schedule and plan vital 4-wall activities. From P.O. validation 
            to line item sort and load, Argix has the experience to manage vendor activity so that you can concentrate on improved 
            utilization of labor and facilities.<br />
            <br />
            Like all of our services Argix gives you the option of tying this service into our national network offering 
            efficiency and savings in vendor pickup, sorting, line haul and delivery at final destination.<br /></p>
        </div>
    </div>
    <a class="pageMenu" href="javascript: unhide('fulfillment');">Fulfillment Services</a>
    <div id="fulfillment" class="hidden">
        <div class="content">
            <p class="body-copy">However simple or complex your company's fulfillment needs, Argix Logistics has the capability 
            within our Network to deliver a streamlined solution with speed, reliability and accuracy. Our flexibility offers 
            you a cost effective solution that fits your company's unique fulfillment needs.<br />
            <br />
            With Argix, you get the flexibility of choosing a footprint and WMS solution that's right for your brand, while enjoying 
            our fully secured facility in Jamesburg, New Jersey. Argix also provides complete yard management for your inbound and 
            outbound trailers.</p>
        </div>
    </div>
    <a class="pageMenu" href="javascript: unhide('warehousing');">Warehousing</a>
    <div id="warehousing" class="hidden">
        <div class="content">
            <p class="body-copy">Expand your warehouse footprint where and when necessary by utilizing the Argix Network.<br />
            <br />
            Whatever your warehousing needs, big or small, short term or long term, you can rely on Argix Logistics. Whether 
            its new store openings, bulk purchases, seasonal increases or timed released product we have the ability to support 
            your needs.<br />
            <br />
            Like all of our services Argix gives you the option of tying this service into our national network offering efficiency 
            and savings in vendor pickup, sorting, line haul and delivery at final destination.</p>
        </div>
    </div>
</asp:Content>
