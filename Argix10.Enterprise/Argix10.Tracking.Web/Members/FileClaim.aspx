<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileClaim.aspx.cs" Inherits="FileClaim" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=unicodeFFFE">
    <meta name="Generator" content="Microsoft Word 11 (filtered)">
    <title>Standard Form for Presentation of Loss and Damage Claims</title>
    <style>
        <!--
         /* Style Definitions */
        .pageTitle {
            font-size:15pt;
            font-family:"Times New Roman";
            font-weight: bold;
            color: black;
            text-align: center;
        }
        .sectionTitle {
            font-size:9pt;
            font-family:"Times New Roman";
            font-weight: bold;
            color:black;
        }
        .verbage {
            font-size:8pt;
            font-family:"Times New Roman";
            color:black;
        }
        .subText {
            font-size:7.5pt;
            font-family:"Times New Roman";
            color:black;
        }
        -->
    </style>
</head>
<body>
<div align="center">
    <p class="pageTitle">Standard Form for Presentation of Loss and Damage Claims</p>
    <table width="667px" border="0px" cellspacing="0" cellpadding="0" style="margin-left:0px; margin-right:0px; border-left:solid 1pt windowtext;border-right:solid 1pt windowtext">
        <tr style="font-size:1px"><td width="96px">&nbsp;</td><td width="48px">&nbsp;</td><td width="48px">&nbsp;</td><td width="48px">&nbsp;</td><td width="48px">&nbsp;</td><td width="48px">&nbsp;</td><td width="48px">&nbsp;</td><td width="48px">&nbsp;</td><td width="48px">&nbsp;</td><td width="120px">&nbsp;</td><td width="120px">&nbsp;</td></tr>
        <tr height="24px">
            <td colspan="9" style='border-top:solid 1pt windowtext; border-right:solid 1pt windowtext;'>&nbsp;<asp:Label ID="lblName" runat="server" Height="16px" Text="ATTN: Claims Department" Width="90%"></asp:Label></td>
            <td colspan="2" rowspan="3" class="subText" align="center" valign="top" style='border-top:solid 1pt windowtext;'>(Claimant's Number) §</td>
        </tr>
        <tr height="8px">
            <td colspan="9" class="subText" align="center" valign="top" style='border-top:solid 1pt windowtext; border-right:solid 1pt windowtext;'>(Name of person to whom claim is presented)</td>
        </tr>
        <tr height="16px">
            <td colspan="6" style=' border-right:solid 1pt windowtext;'>
                &nbsp;<asp:Label ID="lblCarrier" runat="server" Height="16px" Text="Argix Logistics, Inc." Width="90%"></asp:Label>
            </td>
            <td colspan="3" style=' border-right:solid 1pt windowtext;'>
                &nbsp;<asp:Label ID="lblDate" runat="server" Height="16px" Width="90%"></asp:Label>
            </td>
        </tr>
        <tr height="8px">
            <td colspan="6" class="subText" align="center" valign="top" style='border-top:solid 1pt windowtext;'>(Name of Carrier)</td>
            <td colspan="3" class="subText" align="center" valign="top" style='border-top:solid 1pt windowtext; border-right:solid 1pt windowtext;'>(Date)</td>
            <td colspan="2" rowspan="3" class="subText" align="center" valign="top" style='border-top:solid 1pt windowtext;border-bottom:double 2.0pt windowtext;'>(Carrier's Number)</td>
        </tr>
        <tr height="16px"><td colspan="9" style='border-right:solid 1pt windowtext;'>&nbsp;<asp:Label ID="lblAddress" runat="server"  Height="16px"
                Text="100 Middlesex Center Blvd, Jamesburg, NJ 08831" Width="90%"></asp:Label></td></tr>
        <tr height="8px">
            <td colspan="9" class="subText" align="center" valign="top" style='border-top:solid 1pt windowtext; border-bottom:double windowtext 2.0pt; border-right:solid 1pt windowtext;'>(Address)</td>
        </tr>
        <tr height="24px">
            <td colspan="11" class="verbage" align="left" valign="bottom" style='border-top:none;border-bottom:none;'>
                &nbsp;This claim for $________________ is made against the carrier named above by 
                <asp:Label ID="lblClaimant" runat="server"  Height="16px" Width="288px" Font-Underline="False"></asp:Label>
            </td>
        </tr>
        <tr height="8px">
            <td colspan="1" class="subText">&nbsp;</td>
            <td colspan="3" class="subText" align="center" valign="top">(Amount of claim)</td>
            <td colspan="5" class="subText">&nbsp;</td>
            <td colspan="2" class="subText" align="center" valign="top">(Name of claimant)</td>
        </tr>
        <tr height="16px">
            <td colspan="11" class="verbage" align="left" valign="bottom" style='border-top:none;border-bottom:none;'>
                &nbsp;for ________________________ in connection with the following described shipment(s):
            </td>
        </tr>
        <tr height="8px">
            <td colspan="11" class="subText" align="left" valign="top">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(loss or damage)</td>
        </tr>
        <tr height="16px">
            <td colspan="11" class="verbage" align="left" valign="bottom">
                &nbsp;Description of shipment 
                <asp:Label ID="lblDescription" runat="server"  Height="16px"
                    Width="384px"></asp:Label>
            </td>
        </tr>
        <tr height="16px">
            <td colspan="11" class="verbage" align="left" valign="bottom" style="height: 16px">
                &nbsp;Name and address of consignor (shipper)
                <asp:Label ID="lblConsignor" runat="server"  Height="16px" Width="384px"></asp:Label>&nbsp;
            </td>
        </tr>
        <tr height="16px">
            <td colspan="11" class="verbage" align="left" valign="bottom">
                &nbsp;Shipped from 
                <asp:Label ID="lblShipFrom" runat="server"  Height="16px" Width="336px"></asp:Label>,
                &nbsp;to ________________________________
            </td>
        </tr>
        <tr height="8px">
            <td colspan="7" class="subText" align="center" valign="top">&nbsp;(City, town or station)</td>
            <td colspan="4" class="subText" align="center" valign="top">&nbsp;(City, town or station)</td>
        </tr>
        <tr height="16px">
            <td colspan="11" class="verbage" align="left" valign="bottom">
                &nbsp;Final Destination
                <asp:Label ID="lblFinalDest" runat="server"  Height="16px" Width="336px"></asp:Label>
                &nbsp;&nbsp; Routed via
                <asp:Label ID="lblRoutedVia" runat="server"  Height="16px" Text="Argix Logistics, Inc."
                    Width="144px"></asp:Label>
            </td>
        </tr>
        <tr height="8px">
            <td colspan="7" class="subText" align="center" valign="top">&nbsp;(City, town or station)</td>
            <td colspan="4" class="subText" align="left" valign="top">&nbsp;</td>
        </tr>
        <tr height="16px">
            <td colspan="11" class="verbage" align="left" valign="bottom">
                &nbsp;Bill of Lading issued by
                <asp:Label ID="lblBOLBy" runat="server"  Height="16px" Width="288px"></asp:Label>&nbsp; Co; Date of Bill of Lading
                <asp:Label ID="lblBOLDate" runat="server"  Height="16px" Width="118px"></asp:Label>
            </td>
        </tr>
        <tr height="16px">
            <td colspan="11" class="verbage" align="left" valign="bottom">
                &nbsp;Paid Freight Bill (Pro) Number
                <asp:Label ID="lblPRONum" runat="server"  Height="16px" Width="148px"></asp:Label>&nbsp; Original Car Number and Initial _______________________________
            </td>
        </tr>
        <tr height="16px">
            <td colspan="11" class="verbage" align="left" valign="bottom">
                &nbsp;Truck or Trailer Number
                <asp:Label ID="lblTrailerNum" runat="server"  Height="16px"
                    Width="174px"></asp:Label>&nbsp; Connecting Line Reference __________________________________
            </td>
        </tr>
        <tr height="16px">
            <td colspan="11" class="verbage" align="left" valign="bottom">
                &nbsp;Name and address of consignee (shipped to)
                <asp:Label ID="lblConsignee" runat="server"  Height="16px" Width="384px"></asp:Label>
            </td>
        </tr>
        <tr height="16px">
            <td colspan="11" class="verbage" align="left" valign="bottom">
                &nbsp;If shipment reconsigned enroute, state particulars: ___________________________________________________________________
            </td>
        </tr>
        <tr height="24px">
            <td colspan="11" class="sectionTitle" align="center" valign="middle" style="border-top:double 2.0pt windowtext;">
                DETAILED STATEMENT SHOWING HOW AMOUNT CLAIMED IS DETERMINED
            </td>
        </tr>
        <tr height="8px">
            <td colspan="11" class="subText" align="center" valign="middle">
                (Number and description of articles, nature and extent of loss or damage, invoice price of articles, amount of claim, etc.)
            </td>
        </tr>
        <tr height="16px">
            <td colspan="10" style='border-top:double 2.0pt windowtext; border-right:solid 1pt windowtext;'>&nbsp;</td>
            <td colspan="1" style='border-top:double 2.0pt windowtext;'>&nbsp;</td>
        </tr>
        <tr height="16px">
            <td colspan="10" style='border-top:solid 1pt windowtext; border-right:solid 1pt windowtext;'>&nbsp;</td>
            <td colspan="1" style='border-top:solid windowtext 1pt;'>&nbsp;</td>
        </tr>
        <tr height="16px">
            <td colspan="10" style='border-top:solid 1pt windowtext; border-right:solid 1pt windowtext;'>&nbsp;</td>
            <td colspan="1" style='border-top:solid windowtext 1pt;'>&nbsp;</td>
        </tr>
        <tr height="16px">
            <td colspan="10" style='border-top:solid 1pt windowtext; border-right:solid 1pt windowtext;'>&nbsp;</td>
            <td colspan="1" style='border-top:solid windowtext 1pt;'>&nbsp;</td>
        </tr>
        <tr height="16px">
            <td colspan="10" style='border-top:solid 1pt windowtext; border-right:solid 1pt windowtext;'>&nbsp;</td>
            <td colspan="1" style='border-top:solid windowtext 1pt;'>&nbsp;</td>
        </tr>
        <tr height="16px">
            <td colspan="10" style='border-top:solid 1pt windowtext; border-right:solid 1pt windowtext;'>&nbsp;</td>
            <td colspan="1" style='border-top:solid windowtext 1pt;'>&nbsp;</td>
        </tr>
        <tr height="16px">
            <td colspan="10" style='border-top:solid 1pt windowtext; border-right:solid 1pt windowtext;'>&nbsp;</td>
            <td colspan="1" style='border-top:solid windowtext 1pt;'>&nbsp;</td>
        </tr>
        <tr height="16px">
            <td colspan="10" style='border-top:solid 1pt windowtext; border-right:solid 1pt windowtext;'>&nbsp;</td>
            <td colspan="1" style='border-top:solid windowtext 1pt;'>&nbsp;</td>
        </tr>
        <tr height="16px">
            <td colspan="10" style='border-top:solid 1pt windowtext; border-right:solid 1pt windowtext;'>&nbsp;</td>
            <td colspan="1" style='border-top:solid windowtext 1pt;'>&nbsp;</td>
        </tr>
        <tr height="12px">
            <td colspan="11" class="sectionTitle" align="center" valign="middle" style='border-top:double 2.0pt windowtext;'>
                IN ADDITION TO THE INFORMATION GIVEN ABOVE, THE FOLLOWING DOCUMENTS ARE SUBMITTED IN 
                <br />
                SUPPORT OF THIS CLAIM*
            </td>
        </tr>
        <tr height="12px">
            <td colspan="2" style='border-top:solid 1pt windowtext;'>&nbsp;</td>
            <td colspan="9" class="verbage" align="left" valign="middle" style='border-top:solid 1pt windowtext;'>&nbsp;(&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp;&nbsp;1.&nbsp;&nbsp;&nbsp;Original bill of lading, if not previously surrendered to carrier.</td>
        </tr>
        <tr height="12px">
            <td colspan="2" style='border-top:solid 1pt windowtext;'>&nbsp;</td>
            <td colspan="9" class="verbage" align="left" valign="middle" style='border-top:solid 1pt windowtext;'>&nbsp;(&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp;&nbsp;2.&nbsp;&nbsp;&nbsp;Original Invoice or certified copy, showing cost of items claimed.</td>
        </tr>
        <tr height="12px">
            <td colspan="2" style='border-top:solid 1pt windowtext;'>&nbsp;</td>
            <td colspan="9" class="verbage" align="left" valign="middle" style='border-top:solid 1pt windowtext;'>&nbsp;(&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;)&nbsp;&nbsp;&nbsp;3.&nbsp;&nbsp;&nbsp;Other particulars obtainable in proof of loss or damage claimed, such as POD with item
                noted.</td>
        </tr>
        <tr height="12px">
            <td colspan="2" style='border-top:solid 1pt windowtext;'>&nbsp;</td>
            <td colspan="9" class="verbage" align="left" valign="middle" style='border-top:solid 1pt windowtext;'>&nbsp;</td>
        </tr>
        <tr height="54px">
            <td colspan="2" class="verbage" align="right" valign="top" style='border-top:solid 1pt windowtext;'>Remarks&nbsp;</td>
            <td colspan="9" class="verbage" align="left" valign="top" style='border-top:solid 1pt windowtext;'>
                &nbsp;__________________________________________________________________________________________________<br />
                &nbsp;__________________________________________________________________________________________________<br />
                &nbsp;__________________________________________________________________________________________________
            </td>
        </tr>
        <tr height="12px">
            <td colspan="11" class="verbage" style='border-top:solid 1pt windowtext;'>
                &nbsp;The foregoing statement of facts is hereby certified to as correct.
            </td>
        </tr>
        <tr height="12px">
            <td colspan="8" style='border-top:none; border-bottom:none;border-right:none'>&nbsp;</td>
            <td colspan="3" class="subText" align="center" valign="baseline" style='border-bottom:solid 1pt windowtext;'>&nbsp;</td>
        </tr>
        <tr height="12px">
            <td colspan="8" class="subText">&nbsp;</td>
            <td colspan="3" class="subText" align="center" valign="top">(Signature of claimant)</td>
        </tr>
        <tr>
            <td colspan="11" class="subText"style='border-bottom:solid 1pt windowtext;'>
                <br />
                &nbsp;§Claimant should assign to each claim a number, inserting same in the space provided at the upper right hand corner of this form. Reference should be made 
                thereto in all correspondence pertaining to this claim.
                <br />
                &nbsp;• Claimant will please place check (x) before such of the documents mentioned as have been attached, and explain under &quot;Remarks&quot; the absence of any of 
                the documents called for in connection with this claim. When for any reason it is impossible for claimant to produce original bill of lading, or paid freight 
                bill,  claimant should indemnify carrier or carriers against duplicate claim supported by original documents.
            </td>
        </tr>
    </table>
</div>
</body>
</html>


