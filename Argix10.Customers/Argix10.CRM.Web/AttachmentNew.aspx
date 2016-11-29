<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="AttachmentNew.aspx.cs" Inherits="AttachmentNew" %>
<%@ MasterType VirtualPath="~/Default.master" %>

<asp:Content ID="cHead" runat="server" ContentPlaceHolderID="cpHead">
</asp:Content>
<asp:Content ID="cBody" runat="server" ContentPlaceHolderID="cpBody">
<div>
    <div class="subtitle"><asp:Label ID="lblTitle" runat="server" Text="New Attachment" /></div>
    <table ID="tblPage" style="width:500px">
        <tr style="font-size:1px"><td style="width:96px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td style="text-align:right; vertical-align:top">Attachment&nbsp;</td>
            <td><asp:FileUpload ID="fuAttachment" runat="server" Width="400px" ToolTip="Select a file for attachment..." /></td>
        </tr>
        <tr style="font-size:1px; height:24px"><td colspan="2">&nbsp;</td></tr>
        <tr>
            <td>&nbsp;</td>
            <td style="text-align:right">
                <asp:Button ID="btnOk" runat="server" Text="   OK   " ToolTip="Create new action" Height="20px" Width="96px" UseSubmitBehavior="False" CommandName="OK" OnCommand="OnCommandClick" />
                &nbsp;
                <asp:Button ID="btnCancel" runat="server" Text=" Cancel " ToolTip="Cancel new action" Height="20px" Width="96px" UseSubmitBehavior="False" CommandName="Cancel" OnCommand="OnCommandClick" />
            </td>
        </tr>
    </table>
</div>
</asp:Content>
