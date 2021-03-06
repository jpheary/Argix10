<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" StylesheetTheme="ArgixDirect" Theme="ArgixDirect" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Argix Logistics ~ Tracking</title>
    <link href="App_Themes/ArgixDirect/ArgixDirect.css" rel="stylesheet" type="text/css" />
</head>
<body>
<div class="container">
    <div id="topBannerDiv"><img src="App_Themes/ArgixDirect/Images/Argix-Client_banner.jpg" alt="" /><br /></div>
    <div id="topLine"></div>
    <div class="content">
    <form id="form1" runat="server" defaultfocus="txtNumbers" name="frmTrack" action="Default.aspx" method="post">
        <table class="default" style="width:480px" border="0" cellpadding="0" cellspacing="0" >
	        <tr>
	            <td style="height: 243px; width: 100%;">
		            <table style="width:100%" cellpadding="0" cellspacing="0">
				        <tr><td class="label" style="height:25px"><span class="bold">Enter Tracking Numbers:</span></td></tr>
				        <tr><td style="height:15px; vertical-align:middle"> Enter up to <span class="bold">10 Argix Logistics or USPS tracking numbers</span>, one per line.</td></tr>
				        <tr>
					        <td style="height:154px">
						        <table style="width:480px; height:100%" border="0" cellpadding="0" cellspacing="1">
							        <tr>
								        <td><asp:TextBox ID="txtNumbers" runat="server" Width="100%" Rows="10" TextMode="MultiLine"></asp:TextBox></td>
                                        <td style="width:48px">
                                            <asp:RequiredFieldValidator id="rfvTrack" runat="server" ErrorMessage="Please enter at least one tracking number." ControlToValidate="txtNumbers">*</asp:RequiredFieldValidator>
                                            <asp:CustomValidator ID="cvTrack" runat="server" ClientValidationFunction="ValidateNumbers" ControlToValidate="txtNumbers" ErrorMessage="Each tracking number must be 22 numeric characters." ValidateEmptyText="True">*</asp:CustomValidator>
                                        </td>
							        </tr>
							        <tr>
								        <td><asp:ValidationSummary ID="vsTrack" runat="server" Width="100%" DisplayMode="SingleParagraph" /></td>
                                        <td style="width:48px">&nbsp;</td>
                                    </tr>
						        </table>
					        </td>
				        </tr>
				        <tr>
				            <td style="height:19px; text-align:right">
                                <asp:LinkButton ID="btnTrack2" SkinID="linkBtn" OnClick="OnTrack" runat="server" >Track</asp:LinkButton>
                                <asp:ImageButton ID="btnTrack" runat="server" OnClick="OnTrackImg"  ImageUrl="~/App_Themes/ArgixDirect/Images/btn_arrow_red_right.gif" ImageAlign="AbsBottom" /> &nbsp;&nbsp;
					        </td>
				        </tr>
		            </table>
                </td>
	        </tr>
        </table>
        <br /><br /><br /><br />
    </form>
    </div>
</div>
</body>
</html>
<script type="text/jscript" language="jscript">
    <!--
    function ValidateNumbers(sender, e) {
        var s = new String(e.Value);
        var ss = s.split("\n");
        var valid = true;
        for (i = 0; i < ss.length; i++) {
            var _s = trim(ss[i].replace("\r", ""));
            valid = (_s.length == 22 || _s.length == 30);

            var re1 = /\w{22}/g;
            var re2 = /\w{30}/g;
            valid = re1.test(_s) || re2.test(_s);
            if (!valid) break;
        }
        e.IsValid = valid;
    }
    function trim(str) {
        str = str.toString();
        while(1) {
            if(str.substring(0, 1) != " ") { break; }
            str = str.substring(1, str.length);
        }
        while(1) {
            if(str.substring(str.length - 1,str.length) != " ") { break; }
            str = str.substring(0, str.length - 1);
        }
        return str;
    }
    -->
</script>
