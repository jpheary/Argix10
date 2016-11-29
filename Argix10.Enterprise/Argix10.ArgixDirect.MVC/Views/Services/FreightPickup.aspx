<%@ Page Language="C#" MasterPageFile="~/Views/Services/Services.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="MetaContent">
    <meta name="keywords" content="logistics,  freight, pickup, services, carriers" />
    <meta name="description" content="Argix Direct provides clients with freight pickup services. We have an extensive network of carriers with nationwide reach."/>
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="PageContent">
	<img class="imgFloatRight" src="<%= Url.Content("~/content/images/freight.jpg") %>"  alt="Freight Pickup." width="240" />
    <div  class="textContainer"> 
    <h4> FREIGHT PICKUP</h4>
        <p>Argix Direct provides our clients with freight pickup support. We have long standing relationships with an extensive network of carriers with nationwide capabilities.</p>
        <p>Argix Direct provides single store deliveries from multiple shipments. This provides our clients with significant cost savings.</p>
	</div>	  
</asp:Content>
