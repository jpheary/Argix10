<%@ Page Title="Argix :: Transportation" Language="C#" MasterPageFile="~/Areas/Mobile/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cContent" ContentPlaceHolderID="cpContent" runat="server">
    <div class="header-transportation">
        <span class="headline">If there's a more direct<br />route to your destination,<br /></span><span class="headline-red">why not take it?</span>
    </div>
    <div class="content"> <span class="big_caps_t">T</span><span class="titles">here's a better way.</span>
        <p class="body-copy">Your delivery needs are unique. At Argix Logistics, we treat them that way. Our national and 
        regional transportation services are intended for flexibility. With Argix, you get a customized delivery solution 
        for reduced costs, fewer touches and less risk. </p>
        <p class="body-copy"> It just makes sense.  If there's a more direct route to your destination, why not take it?  
        We make your planning and execution easier with precision time and day definite capability. We save time and money 
        with DC Bypass, zone skip and other smart shipping, tracking and customer reporting options.</p>
        <p class="body-copy">Whether your needs are large or small, seasonal or year-around, simple or complex, count on 
        Argix Logistics for reliable transportation solutions customized to maximize efficiency and control costs.</p>
    </div>
    <a class="pageMenu" href="javascript: unhide('b2b');">Nationwide B2B Delivery</a>
    <div id="b2b" class="hidden">
        <div class="content">
            <p class="body-copy">Argix Logistics provides nationwide B2B delivery solutions customized to improve efficiency and 
            eliminate risks. Our national footprint of 43 terminals gives you precision delivery with reduced handling and cost.<br /></p>
            <p class="body-copy">Argix Logistics can move your freight from single or multiple pick up/induct points, bypassing 
            some or all DC operations and combining everything into a single delivery and billing. The Argix Network enables day 
            and time definite delivery affording you with predictable planning and reliable execution. Flexible options such as EDI, 
            zone skipping, drop trailer/yard management and a customer-tailored tracking and reporting package all add up to increased 
            value.</p>
        </div>
    </div>
    <a class="pageMenu" href="javascript: unhide('b2c');">Nationwide B2C Delivery</a>
    <div id="b2c" class="hidden">
        <div class="content">
            <p class="body-copy">Tired of the same old small package delivery choices? There is another way!<br /></p>
            <p class="body-copy">Argix Logistics offers customized direct-to-consumer delivery solutions that save time, limit 
            handling and control costs. Whether it's catalog, ecommerce, seasonal or small parcel shipping and delivery, you'll 
            get the reliability, security and system support that's synonymous with the Argix name.</p>
            <p class="body-copy">
            We combine our work-share partnership and certifications with the U.S. Postal Service with our other specialized delivery 
            services to leverage better efficiency, flexibility and savings for your direct-to-consumer delivery needs. Your customers 
            will enjoy full parcel tracking, a convenience expected by today's sophisticated consumer.</p>
        </div>
    </div>
    <a class="pageMenu" href="javascript: unhide('bypass');">DC Bypass</a>
    <div id="bypass" class="hidden">
        <div class="content">
            <p class="body-copy">Argix Logistics' DC Bypass services are all about moving your products to market faster. When 
            you bypass the distribution center, your products remain in motion, allowing them to reach their destination point 
            quicker and with fewer hassles.This means reduced labor; which minimizes handling that can relate to loss or damage 
            and overall transportation costs.<br /></p>
            <p class="body-copy">Our experts are ready to develop a unique solution for you based on your allocation windows, 
            merchandising expectations, financial concerns regarding ownership and more effective inventory turn.<br /></p>
            <p class="body-copy">In the end, it's all about higher margins based on turn.</p>
        </div>
    </div>
    <a class="pageMenu" href="javascript: unhide('ltl');">LTL Delivery (Pallet Shipment Program )</a>
    <div id="ltl" class="hidden">
        <div class="content">
            <p class="body-copy">Argix Logistics' pallet shipment program has redefined the traditional LTL (less-than-truckload) 
            industry. Argix optimizes its network capabilities and offers attractive pricing with superior services for pallet LTL 
            shipments. We customize pricing based on your requirements. We maintain entry-level pricing with better-than-standard 
            transits that rival the industry's service without the inflated cost.</p>
        </div>
    </div>
    <a class="pageMenu" href="javascript: unhide('regional');">Regional Delivery</a>
    <div id="regional" class="hidden">
        <div class="content">
            <p class="body-copy">Get all of the benefits of our customized national delivery solutions for your regional delivery 
            needs with Argix Logistics' Regional Delivery Program. We maximize efficiency and reduce costs by leveraging the same 
            processes used in our national model and receiving shipments directly to a localized region.<br /></p>
            <p class="body-copy">Whether B2B or B2C, Argix Logistics can flow your freight from single or multiple pick up/induct 
            points, bypassing some or all DC operations and combining the aggregate into a single delivery and billing. The Argix 
            Network enables day and time definite delivery precision affording you with predictable planning and reliable execution.<br />
            <br />
            Flexible options such as EDI, zone skipping, drop trailer/yard management and a customer-tailored tracking and 
            reporting package all add up to increased value.</p>
        </div>
    </div>
</asp:Content>
