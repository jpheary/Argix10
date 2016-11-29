<%@ Page Language="C#" MasterPageFile="~/Views/Services/Services.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="MetaContent">
    <meta name="keywords" content="logistics,shipment, consolidation, storage facilities, storage, facilities, Distribution Center, Distribution" />
    <meta name="description" content="Argix Direct offers full service storage facilities for temporary or long term needs, to support new store openings, free up space in the Distribution Center, or provide shipment consolidation opportunities."/>
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="PageContent">
	<img class="imgFloatRight" src="<%= Url.Content("~/content/images/warehousing.jpg") %>"  alt="Warehousing" width="240" />
	<div  class="textContainer"> 
        <h4>WAREHOUSING</h4>
        <p>Argix Direct offers full service storage facilities for our clients. This can be for temporary or long term needs, to support new store openings, free up space in the DC, or provide shipment consolidation opportunities.</p>
        <p>Our storage facility is tied directly into our National Sort Center. This helps to provide inventory security with both inbound-outbound scanning. You can also receive customized reporting.</p>
        <p>Our storage option is a great service to support split shipments for stores with space constraints or to merge with additional outbound shipping.</p>
        <p>Argix Direct provides retailers with the ability to support seasonal buys, bulk buys or special merchandise release dates with both storage options and DC bypass.</p>
	</div>	  
</asp:Content>
