﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="Error" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Issue Mgt Error Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>Oops!</h2>
        <table>
			<tr><td style="width:48px">&nbsp</td><td>&nbsp;</td></tr>
			<tr>
				<td>&nbsp</td>
				<td>
					<p>An unexpected error occurred. Please go back to the <a href="Default.aspx">CRM home page</a> and try again.</p>
					<p>&nbsp;</p>
					<p><asp:Label ID="lblError" runat="server" Width="100%" Height="100%" Text=""></asp:Label></p>
				</td>
	        </tr>
	    </table>
    </div>
    </form>
</body>
</html>
