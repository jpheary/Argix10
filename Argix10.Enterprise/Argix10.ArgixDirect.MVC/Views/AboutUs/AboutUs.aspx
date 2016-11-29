<%@ Page Title="Argix Direct - About Us" Language="C#" MasterPageFile="~/Views/AboutUs/AboutUs.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="MetaContent">
    <meta name="keywords" content="retailers, merchandise, retail, delivery, network" />
    <meta name="description" content="Argix Direct helps retailers move merchandise from source to store through a national retail delivery network."/>
</asp:Content>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="PageContent">
	<img class="imgFloatRight" src="<%= Url.Content("~/content/images/jamesburg.jpg") %>" alt="Headquarters." width="240" />
    <div class="textContainer"> 
        <h4> A SYSTEM DESIGNED FOR RETAILERS </h4>
        <p>For over 15 years Argix Direct has helped retailers move merchandise fast and reliably from source to store. Today, we are a fast   growing privately held company headquartered in Jamesburg, NJ, moving more than   40 million cartons per annum to over 7,000 stores nationwide.</p>
        <h4>A NATIONAL RETAIL DELIVERY NETWORK</h4>
        <p>Merchandise is received at our Retail Sort Center, which has the capacity to induct, label, sort and dispatch over 220,000 cartons per   day to 41 Store Delivery Terminals nationwide.</p>
        <p>Our processes and facilities incorporate proprietary technology to pull merchandise through the network at high speed to achieve 98% on-time   deliveries. The same processes and the latest scanning technology minimize   misdeliveries and discrepancies.</p>
    </div>
</asp:Content>
