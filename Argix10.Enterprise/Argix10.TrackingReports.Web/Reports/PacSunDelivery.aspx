<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="PacSunDelivery.aspx.cs" Inherits="_PacSunDelivery" %>
<%@ Register Src="../DualDateTimePicker.ascx" TagName="DualDateTimePicker" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Setup" Runat="Server">
<div class="form" style="width:800px">
    <table>
        <tr style="font-size:1px"><td style="width:96px">&nbsp;</td><td style="width:384px">&nbsp;</td></tr>
        <tr><td colspan="2">&nbsp;<uc1:DualDateTimePicker ID="ddpSetup" runat="server" Width="384px" LabelWidth="90px" DateDaysBack="365" DateDaysForward="0" DateDaysSpread="31" EnableViewState="true" OnDateTimeChanged="OnFromToDateChanged" /></td></tr>
        <tr style="font-size:1px; height:6px"><td colspan="2">&nbsp;</td></tr>
         <tr>
            <td align="right">Scope&nbsp;</td>
            <td>
                <asp:DropDownList ID="cboFilter" runat="server" Width="120px">
                    <asp:ListItem Text="All" Selected="True" Value="0" />
                    <asp:ListItem Text="OS&D Only" Value="1" />
                </asp:DropDownList>
            </td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="2">&nbsp;</td></tr>
   </table>
</div>
</asp:Content>

