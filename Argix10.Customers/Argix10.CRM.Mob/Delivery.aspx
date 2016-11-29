<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Delivery.aspx.cs" Inherits="Delivery" StylesheetTheme="Argix" %>
<%@ MasterType VirtualPath="~/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cBody" runat="server" ContentPlaceHolderID="cpBody">
    <asp:MultiView ID="mvPage" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwSetup" runat="server">
            <div class="pageHeader">Deliveries</div>
            <div class="pageBody">
                <table style="width:100%; height:275px">
                    <tr><td class="label">Client&nbsp;</td><td><asp:DropDownList ID="cboCompany" runat="server" DataSourceID="odsCompanies" DatatextField="CompanyName" DataValueField="CompanyID" ToolTip="" style="width:200px" /></td></tr>
                    <tr><td class="label">Store&nbsp;</td><td><asp:TextBox ID="txtStore" runat="server" style="width:100px" /></td></tr>
                    <tr><td class="label">From&nbsp;</td><td><input id="txtFrom" name="txtFrom" type="date" value="<%=DateTime.Today.AddDays(-7).ToString("MM-dd-yyyy") %>" /></td></tr>
                    <tr><td class="label">To&nbsp;</td><td><input id="txtTo" name="txtTo" type="date" value="<%=DateTime.Today.ToString("MM-dd-yyyy") %>" /></td></tr>
                    <tr><td colspan="2" style="height:10px">&nbsp;</td></tr>
                    <tr><td class="label">&nbsp;</td><td><asp:Button ID="btnView" runat="server" Text="View" CssClass="submit" OnCommand="OnOnCommand" CommandName="View" /></td></tr>
                    <tr><td colspan="2">&nbsp;</td></tr>
                </table>
            </div>
            <asp:ObjectDataSource ID="odsCompanies" runat="server" SelectMethod="GetCompanies" TypeName="Argix.Customers.CustomersGateway" />
       </asp:View>
        <asp:View ID="vwDeliveries" runat="server">
            <div class="pageHeader">
                <asp:Button ID="btnBack" runat="server" Text="<< Back" style="border-style:none" OnCommand="OnOnCommand" CommandName="Back" />
            </div>
            <div class="pageBody">
                <asp:ListView ID="lsvDeliveries" runat="server" DataSourceID="odsDeliveries">
                    <LayoutTemplate>
                        <div id="itemPlaceholder" runat="server" style="width:100%" ></div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <table border="0" cellpadding="3" cellspacing="3" style="width:100%; background-color:#ffffff">
                            <tr><td style="width:50px; text-align:right">CBOL:&nbsp;</td><td style="width:75px; font-weight:bold"><%# Eval("CBOL")%></td><td style="text-align:right;"><%# Eval("CartonsSorted")%>ctns,&nbsp;<%# Eval("Weight")%>lbs</td></tr>
                            <tr><td style="text-align:right">POD:&nbsp;</td><td colspan="2"><%# Format(Eval("PodDate"),"MM-dd-yyyy")%>&nbsp;<%# Format(Eval("PodTime"),"hh:mm tt")%></td></tr>
                            <tr><td style="text-align:right">Est Del:&nbsp;</td><td colspan="2"><%# Format(Eval("ShouldBeDeliveredOn"),"MM-dd-yyyy")%>&nbsp;(<%# Format(Eval("WindowStartTime"),"hh:mm")%>&nbsp;-&nbsp;<%# Format(Eval("WindowEndTime"),"hh:mm")%>)</td></tr>
                            <tr><td style="text-align:right">TL's:&nbsp;</td><td colspan="2"><%# Eval("TLS")%></td></tr>
                            <tr><td colspan="3"><hr /></td></tr>
                        </table>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table border="0" cellpadding="3" cellspacing="0" style="width:100%; background-color:#ffffff">
                            <tr style="background-color:white; height:48px"><td>&nbsp;</td></tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
                <asp:ObjectDataSource ID="odsDeliveries" runat="server" TypeName="Argix.Customers.CustomersGateway" SelectMethod="GetDeliveries">
                    <SelectParameters>
                        <asp:ControlParameter Name="companyID" ControlID="cboCompany" PropertyName="SelectedValue" Type="Int32" />
                        <asp:ControlParameter Name="storeNumber" ControlID="txtStore" PropertyName="Text" Type="Int32" />
                        <asp:Parameter Name="from" DefaultValue="" Type="DateTime" />
                        <asp:Parameter Name="to" DefaultValue="" Type="DateTime" />
                    </SelectParameters>
                </asp:ObjectDataSource>        
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
