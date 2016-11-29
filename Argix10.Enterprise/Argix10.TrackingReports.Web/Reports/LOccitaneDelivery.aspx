<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="LOccitaneDelivery.aspx.cs" Inherits="_LOccitaneDelivery" %>
<%@ Register Src="../DualDateTimePicker.ascx" TagName="DualDateTimePicker" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Setup" Runat="Server">
<div class="form" style="width:800px">
    <table>
        <tr style="font-size:1px"><td style="width:96px">&nbsp;</td><td style="width:384px">&nbsp;</td></tr>
        <tr><td colspan="2">&nbsp;<uc1:DualDateTimePicker ID="ddpSetup" runat="server" Width="336px" LabelWidth="90px" DateDaysBack="365" DateDaysForward="0" DateDaysSpread="31" FromVisible="true" ToLabel="To" ToVisible="true" FromLabel="From" EnableViewState="true" OnDateTimeChanged="OnFromToDateChanged" /></td></tr>
        <tr style="font-size:1px; height:12px"><td colspan="2">&nbsp;</td></tr>
        <tr>
            <td align="right">Scope&nbsp;</td>
            <td>
                <asp:DropDownList ID="cboFilter" runat="server" Width="192px">
                    <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                    <asp:ListItem Value="1">OS&amp;D Only</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="2">&nbsp;</td></tr>
    </table>
</div>
</asp:Content>
