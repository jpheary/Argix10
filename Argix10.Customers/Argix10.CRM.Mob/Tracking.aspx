<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Tracking.aspx.cs" Inherits="Tracking" StylesheetTheme="Argix" %>
<%@ MasterType VirtualPath="~/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cBody" runat="server" ContentPlaceHolderID="cpBody">
    <asp:MultiView ID="mvPage" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwSetup" runat="server">
            <div class="pageHeader">Tracking</div>
            <div class="pageBody">
                <table style="width:100%; height:275px">
                    <tr><td class="label">Client&nbsp;</td><td><asp:DropDownList ID="cboClient" runat="server" DataSourceID="odsClients" DatatextField="CompanyName" DataValueField="ClientID" ToolTip="" style="width:200px" /></td></tr>
                    <tr><td class="label">Store&nbsp;</td><td><asp:TextBox ID="txtStore" runat="server" style="width:100px" /></td></tr>
                    <tr><td class="label">From&nbsp;</td><td><input id="txtFrom" name="txtFrom" type="date" value="<%=DateTime.Today.AddDays(-7).ToString("MM-dd-yyyy") %>" /></td></tr>
                    <tr><td class="label">To&nbsp;</td><td><input id="txtTo" name="txtTo" type="date" value="<%=DateTime.Today.ToString("MM-dd-yyyy") %>" /></td></tr>
                    <tr><td class="label">By&nbsp;</td><td><asp:DropDownList ID="cboBy" runat="server" style="width:75px"><asp:ListItem Text="Delivery" Value="Delivery" Selected="True" /><asp:ListItem Text="Pickup" Value="Pickup" /></asp:DropDownList></td></tr>
                    <tr><td colspan="2" style="height:10px">&nbsp;</td></tr>
                    <tr><td class="label">&nbsp;</td><td><asp:Button ID="btnView" runat="server" Text="Track" CssClass="submit" OnCommand="OnOnCommand" CommandName="Track" /></td></tr>
                    <tr><td colspan="2">&nbsp;</td></tr>
                </table>
            </div>
            <asp:ObjectDataSource ID="odsClients" runat="server" SelectMethod="GetClients" TypeName="Argix.Enterprise.EnterpriseGateway" />
       </asp:View>
        <asp:View ID="vwStore" runat="server">
            <div class="pageHeader">
                <asp:Button ID="btnBack" runat="server" Text="<< Back" style="border-style:none" OnCommand="OnOnCommand" CommandName="Back" />
                <div class="trackingtitle"><%= this.cboClient.SelectedItem.Text + "#" + this.txtStore.Text %></div>
            </div>
            <div class="pageBody">
                <asp:ListView ID="lsvTracking" runat="server" DataSourceID="odsTracking">
                    <LayoutTemplate>
                        <div id="itemPlaceholder" runat="server" style="width:100%" ></div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <table border="0" cellpadding="3" cellspacing="3" style="width:100%; background-color:#ffffff">
                            <tr><td style="width:50px; text-align:right">TL:&nbsp;</td><td style="width:75px; font-weight:bold"><%# Eval("TL")%></td><td style="text-align:right;"><%# Eval("CartonCount")%>ctns,&nbsp;<%# Eval("Weight")%>lbs</td>
                                <td rowspan="4" style="width:24px"><asp:ImageButton ID="imgSelect" runat="server" ImageUrl="App_Themes/Argix/Images/select.gif" OnCommand="OnOnCommand" CommandName="ViewTL" CommandArgument='<%# Eval("TL") %>' /></td></tr>
                            <tr><td style="text-align:right">CBOL:&nbsp;</td><td colspan="2"><%# Eval("CBOL")%></td></tr>
                            <tr><td style="text-align:right"><%# DeliveryDateLabel(Eval("OFD1"),Eval("PodDate"))%></td><td colspan="2"><%# DeliveryDateFormat(Eval("OFD1"),Eval("PodDate"),"MM-dd-yyyy")%></td></tr>
                            <tr><td style="text-align:right">Agent:&nbsp;</td><td colspan="3"><%# Eval("AG")%>&nbsp;-&nbsp;<%# Eval("AgName")%></td></tr>
                            <tr><td colspan="4"><hr /></td></tr>
                        </table>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="3" cellspacing="0" style="width:100%; background-color:#ffffff">
                            <tr style="background-color:white; height:48px"><td>&nbsp;</td></tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
                <asp:ObjectDataSource ID="odsTracking" runat="server" TypeName="Argix.Enterprise.EnterpriseGateway" SelectMethod="TrackCartonsForStoreSummary">
                    <SelectParameters>
                        <asp:ControlParameter Name="clientID" ControlID="cboClient" PropertyName="SelectedValue" Type="String" />
                        <asp:ControlParameter Name="storeNumber" ControlID="txtStore" PropertyName="Text" Type="String" />
                        <asp:Parameter Name="from" DefaultValue="" Type="DateTime" />
                        <asp:Parameter Name="to" DefaultValue="" Type="DateTime" />
                        <asp:ControlParameter Name="by" ControlID="cboBy" PropertyName="SelectedValue" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>        
            </div>
        </asp:View>
        <asp:View ID="vwTL" runat="server">
            <div class="pageHeader">
                <asp:Button ID="btnBack1" runat="server" Text="<< Back" style="border-style:none" OnCommand="OnOnCommand" CommandName="BackStore" />
                <div class="trackingtitle"><%= this.cboClient.SelectedItem.Text + "#" + this.txtStore.Text%>,&nbsp;TL#&nbsp;<asp:Label ID="lblTL" runat="server" Text="" /></div>
            </div>
            <div class="pageBody">
                <asp:ListView ID="lsvCartons" runat="server" DataSourceID="odsCartons">
                    <LayoutTemplate>
                        <div id="itemPlaceholder" runat="server" style="width:100%" ></div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <table border="0" cellpadding="3" cellspacing="3" style="width:100%; background-color:#ffffff">
                            <tr><td style="width:50px; text-align:right">Ctn#:&nbsp;</td><td style="width:100px; font-weight:bold"><%# Eval("CartonNo")%></td><td style="text-align:right;"><%# Eval("Weight")%>lbs</td></tr>
                            <tr><td style="text-align:right">Pickup:&nbsp;</td><td colspan="2"><%# Format(Eval("Pudt"),"MM-dd-yyyy")%></td></tr>
                            <tr><td style="text-align:right">Shipper:</td><td colspan="2"><%# Eval("ShpName")%></td></tr>
                            <tr><td style="text-align:right">Status:&nbsp;</td><td colspan="2">Ctn=&nbsp;<%# Eval("CtnSts")%>;&nbsp;&nbsp;&nbsp;Scan=&nbsp;<%# Eval("ScnSts")%></td></tr>
                            <tr><td style="text-align:right">POD:&nbsp;</td><td colspan="2"><%# Format(Eval("PodDate"),"MM-dd-yyyy")%>&nbsp;<%# Format(Eval("PodTime"),"HH:mm tt")%></td></tr>
                            <tr><td colspan="3"><hr /></td></tr>
                        </table>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="3" cellspacing="0" style="width:100%; background-color:#ffffff">
                            <tr style="background-color:white; height:48px"><td>&nbsp;</td></tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
                <asp:ObjectDataSource ID="odsCartons" runat="server" TypeName="Argix.Enterprise.EnterpriseGateway" SelectMethod="TrackCartonsForStoreDetail">
                    <SelectParameters>
                        <asp:ControlParameter Name="clientID" ControlID="cboClient" PropertyName="SelectedValue" Type="String" />
                        <asp:ControlParameter Name="storeNumber" ControlID="txtStore" PropertyName="Text" Type="String" />
                        <asp:Parameter Name="from" DefaultValue="" Type="DateTime" />
                        <asp:Parameter Name="to" DefaultValue="" Type="DateTime" />
                        <asp:Parameter Name="tl" Type="String" />
                        <asp:ControlParameter Name="by" ControlID="cboBy" PropertyName="SelectedValue" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>        
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
