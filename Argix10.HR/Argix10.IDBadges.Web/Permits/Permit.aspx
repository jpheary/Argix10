<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Permit.aspx.cs" Inherits="_Permit" %>
<%@ Register Assembly="System.Web.Extensions, Version=3.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
<!DOCTYPE HTML>

<html>
<head id="Head1" runat="server">
    <title>Argix Logistics HR Security</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="Pragma" content="no-cache" />
	<meta http-equiv="Cache-Control" content="no-cache='set-cookie', no-store" />
	<meta http-equiv="Expires" content="-1" />
    <link href='https://fonts.googleapis.com/css?family=Michroma' rel='stylesheet' type='text/css' />
    <link href='https://fonts.googleapis.com/css?family=Open+Sans+Condensed:300,700,300italic' rel='stylesheet' type='text/css' />
    <style>
        .gridbox {
            border: 0px solid grey;
        }
        .gridcontainer {
            position: relative;
	        width: 98%;
	        margin: auto;
	        padding: 0% 1%;
	        background-color: #ffffff;
        }
        .gridheader {
            position: relative;
	        width: 100%;
            height: 75px;
	        margin: 0px;
	        padding: 0px;
            border-bottom: 3px solid #c5c7c9;
        }
        .gridtitle {
            position: relative;
	        width: 100%;
            height: 50px;
        }
        .gridbody {
            position: relative;
	        width: 100%;
	        margin: 0px;
	        padding: 0px;
        }
        .logo {
            float: left;
            width: auto;
            margin: 9px 6px 6px 6px;
        }
        .title {
            float: left;
            margin: 5px 0px;
            padding: 5px 0px 0px 5px;
	        color: #ee2a24;
	        font-family: Michroma;
            font-size: 20px;
            font-weight: 400;
            text-align: left;
        }
        .subtitle {
            margin: 0px 0px 10px 0px;
            padding: 0px 0px 0px 5px;
	        color: #c5c7c9;
            font-size: 1.3em;
            font-weight: 600;
            text-align: left;
        }

        .permit {
            width: 450px;
            margin: 0px auto;
        }
        .permit p {
            margin: 10px 30px;
        }
        .permit fieldset {
            margin: 10px auto;
            padding: 15px 5px 5px 0px;
        }
        .permit fieldset legend {
            margin: 0px 0px 10px 0px;
            padding: 0px 5px;
            color: #c5c7c9;
        }
        .permit label {
            display: inline-block;
            width: 100px;
            padding: 3px 5px 0px 0px;
            text-align: right;
            vertical-align: top;
            font-weight: bold;
        }
        .permit fieldset span {
            margin: 2px 0px;
            padding: 0px 0px 0px 5px;
            border-bottom: 1px solid #c5c7c9;
        }
        .clear { clear: both;  }
    </style>
</head>
<body>
<form id="idForm" runat="server">
    <div class="gridcontainer">
        <div class="gridbox gridheader">
            <div class="logo">
                <a href="https://www.argixlogistics.com" target="_self"><img id="Img1" runat="server" src="~/App_Themes/Argix/Images/argix-logo.gif" alt="Argix logo" style="border: 0;" /></a>
            </div>
        </div>
        <div class="gridbox gridtitle">
            <div class="title">HR Security</div>
            <div class="clear"></div>
        </div>
        <div class="gridbox gridbody">
            <div class="permit">
                <div class="subtitle">Parking Permit#&nbsp;<asp:Label ID="lblPermitNumber" runat="server" Width="100px" Text="" /></div>
                <div>
                    <fieldset>
                        <legend>Vehicle</legend>
                        <label for="lblPlate">Plate#</label><asp:Label ID="lblPlate" runat="server" Width="200px" Text="" /><br />
                        <label for="lblMake">Make</label><asp:Label ID="lblMake" runat="server" Width="300px" Text="" /><br />
                        <br />
                    </fieldset>
                    <fieldset>
                        <legend>Contact</legend>
                        <label for="lblName">Name</label><asp:Label ID="lblName" runat="server" Width="300px" Text="" /><br />
                        <label for="lblPhone">Phone</label><asp:Label ID="lblPhone" runat="server" Width="150px" Text="" /><br />
                        <label for="lblBadge">Badge#</label><asp:Label ID="lblBadge" runat="server" Width="100px" Text="" /><br />
                       <br />
                    </fieldset>
                </div>
            </div>
        </div>
    </div>
</form>
</body>
</html>
