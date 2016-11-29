<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Error" %>
<%@ MasterType VirtualPath="~/Default.master" %>

<asp:Content ID="cBody" ContentPlaceHolderID="cpBody" Runat="Server">
<div id="detailHeadContainer" style="height:375px" > 
    <table id="tblError" style="width:750px">
        <tr><td>&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
	        <td>&nbsp;</td>
	        <td style="border:1px solid #e7000d;">&nbsp;<asp:Label ID="lblTitle" runat="server" Text="System Error Encountered" style="font-size: 16px" /></td>
        </tr>
        <tr><td>&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
	        <td>&nbsp;</td>
	        <td>
		        <p>An error was encountered while you were using this site. Please go back to the 
		        Fast Tracking Home Page and start over. If the problem persists, 
		        restart your browser and try again.</p>
	        </td>
        </tr>
        <tr><td>&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
	        <td>&nbsp;</td>
            <td class="body">&nbsp;<asp:Label ID="lblError" runat="server" Text="" /></td>
        </tr>
    </table>
</div>
</asp:Content>

