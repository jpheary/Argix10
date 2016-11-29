<%@ Page Language="C#" MasterPageFile="~/Views/WhyArgix/WhyArgix.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="MetaContent">
    <meta name="keywords" content="transit, transit time, sort, delivery" />
    <meta name="description" content=" Argix Direct provides time sensitive solutions for expeditious delivery of your freight."/>
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="PageContent">
	<div class="textContainer">
        <h4>TRANSIT TIMES : Time Sensitive Solutions</h4>
        <br />
        <img src="<%= Url.Content("~/content/images/transittimes.jpg") %>" alt="Transit Times" width="550" />
    <p class="footnote">*Transit times from National Sort Center in New Jersey. </p>
    </div>  
</asp:Content>
