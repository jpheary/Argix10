﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Areas/Mobile/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="cMain" ContentPlaceHolderID="MainContent" runat="server">
    <h2>About Us</h2>
    <h4>A SYSTEM DESIGNED FOR RETAILERS</h4>
    <img class="imgFloatRight" src="<%= Url.Content("~/content/images/jamesburg.jpg") %>" alt="Headquarters." width="120" />
    <div class="textContainer"> 
        <p>For over 15 years Argix Direct has helped retailers move merchandise fast and reliably from source to store. Today, we are a fast growing privately held company headquartered in Jamesburg, NJ, moving more than 40 million cartons per annum to over 7,000 stores nationwide.</p>
    </div>
    <h4>A NATIONAL RETAIL DELIVERY NETWORK</h4>
    <div class="textContainer"> 
        <p>Merchandise is received at our Retail Sort Center, which has the capacity to induct, label, sort and dispatch over 220,000 cartons per day to 41 Store Delivery Terminals nationwide.</p>
        <p>Our processes and facilities incorporate proprietary technology to pull merchandise through the network at high speed to achieve 98% on-time deliveries. The same processes and the latest scanning technology minimize misdeliveries and discrepancies.</p>
    </div>
    <h4>LOCATION</h4>
    <div class="textContainer">
        <h5>43 Distribution Terminals Nationwide</h5>
        <p>
            <img src="<%= Url.Content("~/content/images/zonemap.gif") %>" alt="Terminal map" width="100%" />
        </p>
    </div>
</asp:Content>
