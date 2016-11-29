<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="PollockPaperInvoice.aspx.cs" Inherits="PollockPaperInvoice" %>
<%@ Register Src="~/DualDateTimePicker.ascx" TagName="DualDateTimePicker" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Setup" Runat="Server">
<table width="100%">
    <tr style="font-size:1px"><td width="100px">&nbsp;</td><td width="400px">&nbsp;</td><td>&nbsp;</td></tr>
    <tr>
        <td colspan="2"><uc1:DualDateTimePicker ID="ddpSetup" runat="server" Width="350px" LabelWidth="100px" DateDaysBack="365" DateDaysForward="0" DateDaysSpread="7" OnDateTimeChanged="OnFromToDateChanged" /></td>
        <td>&nbsp;</td>
    </tr>
    <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
</table>
</asp:Content>

