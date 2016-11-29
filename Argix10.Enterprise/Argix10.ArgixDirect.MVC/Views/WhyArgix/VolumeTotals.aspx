<%@ Page Language="C#" MasterPageFile="~/Views/WhyArgix/WhyArgix.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="MetaContent">
    <meta name="keywords" content=" customer, customer service, customer service representative, Smartscan, tracking, reporting, shipment details" />
    <meta name="description" content=" Argix Direct provides dedicated service representatives to support your business needs."/>
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="PageContent">
  	<div  class="textContainer"> 
        <h4 class="execText">VOLUME TOTALS : 2010<br/> 
            <img class="imgBlock" src="<%= Url.Content("~/content/images/blackbar.jpg") %>" alt="" width="350" height="4"  />
        </h4>
        <br />
        <table width="355" border="0" cellpadding="0">
            <tr>
                <td width="200" class="execName">► Weight  </td>
                <td width="200" class="execTerm">540 million pounds</td>
            </tr>
            <tr>
                <td width="200" class="execName">► Cartons  </td>
                <td width="200" class="execTerm">32 million</td>
            </tr>
            <tr>
                <td width="200" class="execName">► Deliveries  </td>
                <td width="200" class="execTerm">816,000</td>
            </tr>
            <tr>
                <td width="200" class="execName">► Locations  </td>
                <td width="200" class="execTerm">11,900+</td>
            </tr>
        </table>      
    </div>	  
</asp:Content>
