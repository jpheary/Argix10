<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="PacSunSummary.aspx.cs" Inherits="_PacSunSummary" %>
<%@ Register Src="../DualDateTimePicker.ascx" TagName="DualDateTimePicker" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Setup" Runat="Server">
<div class="form" style="width:800px">
    <table>
        <tr style="font-size:1px; height:1px"><td style="width:100px">&nbsp;</td><td style="width:400px">&nbsp;</td></tr>
        <tr><td colspan="2">&nbsp;<uc1:DualDateTimePicker ID="ddpSetup" runat="server" Width="350px" LabelWidth="100px" DateDaysBack="365" DateDaysForward="0" DateDaysSpread="31" EnableViewState="true" OnDateTimeChanged="OnFromToDateChanged" /></td></tr>
        <tr style="font-size:1px; height:6px"><td colspan="2">&nbsp;</td></tr>
   </table>
</div>
</asp:Content>

