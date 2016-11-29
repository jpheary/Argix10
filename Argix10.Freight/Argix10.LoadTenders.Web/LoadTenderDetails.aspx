<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LoadTenderDetails.aspx.cs" Inherits="LoadTenderDetails" StylesheetTheme="Argix" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Load Tender Details</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="smPage" runat="server" EnablePartialRendering="false" ScriptMode="Auto"></asp:ScriptManager>
    <table width="100%" border="0" cellpadding="0px" cellspacing="0px">
        <tr><td style="font-size:1px; width:24px">&nbsp;</td><td style="font-size:1px">&nbsp;</td></tr>
        <tr style="height:32px">
            <td>&nbsp;</td>
            <td style="font-size:1.0em; vertical-align:middle; padding-left:6px; background-image:url(App_Themes/Argix/Images/pagetitle.gif); background-repeat:repeat-x;">
                <table width="100%" border="0" cellpadding="0px" cellspacing="0px"> 
                    <tr>
                        <td valign="top" style="font-size:1.5em">
                            <img src="App_Themes/Argix/Images/app.gif" alt="Argix Logistics Load Tenders" />&nbsp;Argix Logistics Load Tenders
                        </td>
                        <td align="right" valign="bottom">&nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr><td colspan="2" style="font-size:1px; height:24px">&nbsp;</td></tr>
        <tr>
            <td colspan="2">
                <div style="float:left; width:400px; margin:0 0 0 50px">
                <asp:FormView ID="FormView1" runat="server" DataSourceID="odsLoad">
                    <ItemTemplate>
                    <table>
                    <tr>
                      <td align="right"><b>Load#:</b></td>
                      <td><asp:Label id="lblLoad" runat="server" Text='<%# Eval("Load") %>' /></td>
                    </tr>
                    <tr>
                      <td align="right"><b>Reference#:</b></td>
                      <td><asp:Label id="lblRefNum" runat="server" Text='<%# Eval("ReferenceNumber") %>' /></td>
                    </tr>
                    <tr>
                      <td align="right"><b>Location#:</b></td>
                      <td><asp:Label id="lblLocNum" runat="server" Text='<%# Eval("LocationNumber") %>' /></td>
                    </tr>
                    <tr>
                      <td align="right"><b>Location ID:</b></td>
                      <td><asp:Label id="lblLocID" runat="server" Text='<%# Eval("LocationID") %>' /></td>
                    </tr>
                    <tr>
                      <td align="right"><b>PO#:</b></td>
                      <td><asp:Label id="lblPO" runat="server" Text='<%# Eval("PONumber") %>' /></td>
                    </tr>
                 </table>                    
                  </ItemTemplate>
                </asp:FormView>
                </div>
                <div style="float:left; width:400px; margin:0 0 0 50px">
                <asp:FormView ID="FormView2" runat="server" DataSourceID="odsLoad">
                    <ItemTemplate>
                    <table>
                     <tr>
                      <td align="right"><b>Client:</b></td>
                      <td><asp:Label id="lblZip" runat="server" Text='<%# Eval("Client") %>' />&nbsp;-&nbsp;<asp:Label id="Label3" runat="server" Text='<%# Eval("ClientName") %>' />, Store#&nbsp;<asp:Label id="Label4" runat="server" Text='<%# Eval("StoreNumber") %>' /></td>
                    </tr>
                    <tr>
                      <td align="right"><b>Location:</b></td>
                      <td><asp:Label id="lblLocation" runat="server" Text='<%# Eval("Location") %>' /></td>
                    </tr>
                    <tr>
                      <td align="right"><b>Address:</b></td>
                      <td><asp:Label id="lblAddLine1" runat="server" Text='<%# Eval("AddressLine1") %>' /></td>
                    </tr>
                    <tr>
                      <td align="right">&nbsp;</td>
                      <td><asp:Label id="lblAddLine2" runat="server" Text='<%# Eval("AddressLine2") %>' /></td>
                    </tr>
                     <tr>
                      <td align="right">&nbsp;</td>
                      <td><asp:Label id="lblCity" runat="server" Text='<%# Eval("City") %>' />,&nbsp;<asp:Label id="Label1" runat="server" Text='<%# Eval("StateOrProvince") %>' />&nbsp;<asp:Label id="Label2" runat="server" Text='<%# Eval("PostalCode") %>' /></td>
                    </tr>
                 </table>                    
                  </ItemTemplate>
                </asp:FormView>
                </div>
            </td>
        </tr>
        <tr><td colspan="2" style="font-size:1px; height:24px">&nbsp;</td></tr>
        <tr>
            <td>&nbsp;</td>
            <td>
                <table width="900px" border="0px" cellpadding="0px" cellspacing="0px">
                    <tr style="height:18px"><td style="font-size:1.0em; vertical-align:middle; padding-left:6px; background-image:url(App_Themes/Argix/Images/gridtitle.gif); background-repeat:repeat-x;">&nbsp;Load Tender Details</td></tr>
                    <tr>
                        <td valign="top">
                            <asp:Panel id="pnlLoad" runat="server" Width="900px" Height="300px" BorderStyle="Inset" BorderWidth="1px" ScrollBars="Auto">
                                <asp:UpdatePanel ID="upnlLoad" runat="server" UpdateMode="Conditional" >
                                <ContentTemplate>
                                    <asp:GridView ID="grdLoad" runat="server" width="100%" AutoGenerateColumns="False" DataSourceID="odsLoad" DataKeyNames="Load" AllowSorting="True">
                                        <Columns>
                                            <asp:BoundField DataField="ItemNumber" HeaderText="Item#" HeaderStyle-Width="75px" />
                                            <asp:BoundField DataField="ItemDescription" HeaderText="Item Desc" HeaderStyle-Width="200px" />
                                            <asp:BoundField DataField="QuantityOrdered" HeaderText="Qty" HeaderStyle-Width="75px" />
                                            <asp:BoundField DataField="UOM" HeaderText="Units" HeaderStyle-Width="100px" />
                                            <asp:BoundField DataField="SortedItemNumber" HeaderText="Sorted Item#" HeaderStyle-Width="150px" />
                                            <asp:BoundField DataField="SortedPONumber" HeaderText="Sorted PO#" HeaderStyle-Width="150px" />
                                            <asp:BoundField DataField="Sorted" HeaderText="Sorted?" HeaderStyle-Width="75px" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:ObjectDataSource ID="odsLoad" runat="server" TypeName="Argix.TsortGateway" SelectMethod="GetLoadTenderDetails" EnableCaching="false" CacheExpirationPolicy="Sliding" CacheDuration="300" >
                                        <SelectParameters>
                                            <asp:QueryStringParameter Name="loadNumber" QueryStringField="load" Type="String" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </ContentTemplate>
                                </asp:UpdatePanel>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr><td colspan="2" style="font-size:1px; height:12px">&nbsp;</td></tr>
    </table>
    </form>
</body>
</html>
