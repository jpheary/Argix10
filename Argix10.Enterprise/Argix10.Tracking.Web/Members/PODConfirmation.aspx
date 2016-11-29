<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PODConfirmation.aspx.cs" Inherits="PODConfirmation" %>
<!DOCTYPE HTML>

<html>
<head  id="Head1" runat="server">
    <title>POD Request Confirmation</title>
</head>
<body>
<form id="form1" runat="server">
    <table width="576px" border="0px" cellpadding="3px" cellspacing="3px">
        <tr>
            <td class="ctbody" width="96px" align="center" valign="middle">
                <img src="../App_Themes/Argix/Images/info.bmp" height="72" />
            </td>
            <td class="ctbody" align="left" valign="top">
                Thank you. Your POD request has been submitted. You will receive an email confirming your request.<br /><br />
                If you do not receive a confirmation email then contact customer support at:<br />
		        &nbsp;&nbsp;&nbsp;&nbsp;Email: <a class="ctbody" href="mailto:extranet.support@argixlogistics.com">extranet.support@argixlogistics.com</a><br />
			    &nbsp;&nbsp;&nbsp;&nbsp;Phone: 800-274-4582
            </td>
        </tr>
        <tr><td colspan="2" class="ctbody">&nbsp;</td></tr>
        <tr><td colspan="2" class="ctbody" align="right">
            <asp:Button ID="btnOK" runat="server" Text="    OK    " /></td></tr>
    </table>
</form>
</body>
</html>

