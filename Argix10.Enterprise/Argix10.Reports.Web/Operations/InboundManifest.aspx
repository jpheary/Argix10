<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="InboundManifest.aspx.cs" Inherits="InboundManifest" %>
<%@ Register Src="~/DualDateTimePicker.ascx" TagName="DualDateTimePicker" TagPrefix="uc1" %>
<%@ Register Src="~/ClientVendorLogGrids.ascx" TagName="ClientVendorLogGrids" TagPrefix="uc5" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="Content1" runat="Server" ContentPlaceHolderID="Setup">
<asp:Panel ID="pnlSetup" runat="server" Width="100%" GroupingText="Setup">
    <table style="width:100%">
        <tr style="font-size:1px"><td style="width:96px">&nbsp;</td><td style="width:384px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td colspan="2">&nbsp;<uc1:DualDateTimePicker ID="ddpPickups" runat="server" Width="336px" LabelWidth="96px" FromLabel="Pickup Start" ToLabel="Pickup End" DateDaysBack="90" DateDaysForward="0" DateDaysSpread="7" OnDateTimeChanged="OnFromToDateChanged" /></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr><td colspan="3">&nbsp;<uc5:ClientVendorLogGrids ID="dgdClientVendorLog" runat="server" Height="300px" OnAfterClientSelected="OnClientSelected" OnAfterVendorLogEntrySelected="OnVendorLogEntrySelected" /></td></tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
   </table>
</asp:Panel>
</asp:Content>

