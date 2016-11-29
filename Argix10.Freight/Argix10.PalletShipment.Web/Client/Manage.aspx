<%@ Page Title="Manage" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="Manage.aspx.cs" Inherits="Manage" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpBody">
<script type="text/jscript">
    $(document).ready(function () {
        $("#<%=txtContactPhone.ClientID %>").inputmask({ "mask": "999-999-9999" });
    });
</script>
<div class="tabPage">
	<ul>
		<li id="liClient" runat="server"><asp:ImageButton ID="tabClient" runat="server" ImageUrl="~/App_Themes/Argix/Images/account.png" OnCommand="OnChangeView" CommandName="Client" /></li>
		<li id="liShippers" runat="server"><asp:ImageButton ID="tabShippers" runat="server" ImageUrl="~/App_Themes/Argix/Images/shippers.png" OnCommand="OnChangeView" CommandName="Shippers" /></li>
		<li id="liConsignees" runat="server"><asp:ImageButton ID="tabConsignees" runat="server" ImageUrl="~/App_Themes/Argix/Images/consignees.png" OnCommand="OnChangeView" CommandName="Consignees" /></li>
		<li id="liBlank1" runat="server">&nbsp;</li>
		<li id="liBlank2" runat="server">&nbsp;</li>
	</ul>
</div>
<div style="border:1px solid #000000; border-top-style:none; padding:10px 10px 10px 10px; margin-top:25px">
<asp:MultiView runat="server" ID="mvwPage" ActiveViewIndex="0">
<asp:View ID="vwClient" runat="server">
    <div class="subtitle">Client Account #<asp:Label ID="lblID" runat="server" /></div>
    <div style="float:right"><asp:HyperLink ID="lnkChangePassword" runat="server" NavigateUrl="~/Account/ChangePassword.aspx">Click here to change your password</asp:HyperLink>.</div>
    <asp:ValidationSummary ID="vsClient" runat="server" ValidationGroup="vgClient" />
    <asp:CustomValidator id="cvStatus" runat="server" ValidationGroup="vgClient" EnableClientScript="False" />
    <table>
        <tr><td class="labelx">Company Name</td></tr>
        <tr><td><asp:TextBox ID="txtCompanyName" runat="server" Width="275px" ReadOnly="true" /></td></tr>
        <tr><td class="labelx">Company Address</td></tr>
        <tr><td><asp:TextBox ID="txtCompanyStreet" runat="server" Width="275px" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCompanyCity" runat="server" Width="175px" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCompanyState" MaxLength="2" runat="server" Width="50px" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCompanyZip" MaxLength="5" runat="server" Width="75px" />
        </td></tr>
        <tr><td><div class="sublabel" style="float:left; width:275px; margin-right:18px">Street</div><div class="sublabel" style="float:left; width:175px; margin-right:18px">City</div><div class="sublabel" style="float:left; width:50px; margin-right:18px">State</div><div class="sublabel" style="float:left; width:75px">Zipcode</div></td></tr>
        <tr><td class="labelx">Contact Info</td></tr>
        <tr><td><asp:TextBox ID="txtContactName" runat="server" Width="275px" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtContactPhone" runat="server" Width="100px" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtContactEmail" runat="server" Width="175px" ReadOnly="true" />
        </td></tr>
        <tr><td><div class="sublabel" style="float:left; width:275px; margin-right:18px">Name</div><div class="sublabel" style="float:left; width:100px; margin-right:18px">Phone Number</div><div class="sublabel" style="float:left; width:175px">Email Address</div></td></tr>
    </table>
    <br />
    <div class="redline"></div>
    <br />
    <table>
        <tr><td class="labelx">Corporate Name</td></tr>
        <tr><td><asp:TextBox ID="txtCorporateName" runat="server" Width="275px" /></td></tr>
        <tr><td class="labelx">Corporate Address</td></tr>
        <tr><td><asp:TextBox ID="txtCorporateStreet" runat="server" Width="275px" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCorporateCity" runat="server" Width="175px" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCorporateState" MaxLength="2" runat="server" Width="50px" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCorporateZip" MaxLength="5" runat="server" Width="75px" />
        </td></tr>
        <tr><td><div class="sublabel" style="float:left; width:275px; margin-right:18px">Street</div><div class="sublabel" style="float:left; width:175px; margin-right:18px">City</div><div class="sublabel" style="float:left; width:50px; margin-right:18px">State</div><div class="sublabel" style="float:left; width:75px">Zipcode</div></td></tr>
        <tr><td class="labelx">&nbsp;</td></tr>
        <tr><td><asp:TextBox ID="txtTaxIDNumber" runat="server" Width="100px" Visible="false" /></td></tr>
    </table>
    <br />
    <div class="redline"></div>
    <br />
    <table>
        <tr><td class="labelx">Billing Address</td></tr>
        <tr><td><asp:TextBox ID="txtBillingStreet" runat="server" Width="275px" ReadOnly="true" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBillingCity" runat="server" Width="175px" ReadOnly="true" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBillingState" MaxLength="2" runat="server" Width="50px" ReadOnly="true" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBillingZip" MaxLength="5" runat="server" Width="75px" ReadOnly="true" />
        </td></tr>
        <tr><td><div class="sublabel" style="float:left; width:275px; margin-right:18px">Street</div><div class="sublabel" style="float:left; width:175px; margin-right:18px">City</div><div class="sublabel" style="float:left; width:50px; margin-right:18px">State</div><div class="sublabel" style="float:left; width:75px">Zipcode</div></td></tr>
    </table>
    <br />
    <div>
        <asp:Button ID="btnAccountUpdate" runat="server" Text="Update" CssClass="submit" CommandName="AccountUpdate" OnCommand="OnManageCommand" />
    </div>
    <asp:RequiredFieldValidator ID="rfvCompanyName" runat="server" ControlToValidate="txtCompanyName" ErrorMessage="Company name is required." ValidationGroup="vgClient" />
    <asp:RequiredFieldValidator ID="rfvCompanyStreet" runat="server" ControlToValidate="txtCompanyStreet" ErrorMessage="Company street is required." ValidationGroup="vgClient" />
    <asp:RequiredFieldValidator ID="rfvCompanyCity" runat="server" ControlToValidate="txtCompanyCity" ErrorMessage="Company city is required." ValidationGroup="vgClient" />
    <asp:RequiredFieldValidator ID="rfvCompanyState" runat="server" ControlToValidate="txtCompanyState" ErrorMessage="Company state is required." ValidationGroup="vgClient" />
    <asp:RequiredFieldValidator ID="rfvCompanyZip" runat="server" ControlToValidate="txtCompanyZip" ErrorMessage="Company zip is required." ValidationGroup="vgClient" />
    <asp:RequiredFieldValidator ID="rfvContactName" runat="server" ControlToValidate="txtContactName" ErrorMessage="Contact name is required." ValidationGroup="vgClient" />
    <asp:RequiredFieldValidator ID="rfvContactEmail" runat="server" ControlToValidate="txtContactEmail" ErrorMessage="Contact email is required." ValidationGroup="vgClient" />
</asp:View>
<asp:View ID="vwShippers" runat="server">
    <div class="subtitle">Shippers for <asp:Label ID="lblShippersClient" runat="server" Text="" /></div>
    <br />
    <div style="width:890px; height:275px; margin:0px 0px 0px 0px; padding:0px 0px 0px 0px; overflow-x:hidden; overflow-y:scroll; white-space:nowrap;">
        <asp:UpdatePanel runat="server" ID="upnlShippers" UpdateMode="Conditional">
        <ContentTemplate>
        <asp:GridView ID="grdShippers" runat="server" Width="100%" DataSourceID="odsShippers" DataKeyNames="ID" AutoGenerateColumns="false" OnSelectedIndexChanged="OnShipperSelected" >
            <Columns>
                <asp:CommandField HeaderStyle-Width="16px" ButtonType="Image" ShowSelectButton="True" SelectImageUrl="~/App_Themes/Argix/Images/select.gif" />
                <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="ClientID" HeaderText="ClientID" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="ClientName" HeaderText="ClientName" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-Wrap="False" ItemStyle-Width="125px" Visible="True" />
                <asp:BoundField DataField="AddressLine1" HeaderText="Address" ItemStyle-Wrap="False" ItemStyle-Width="125px" Visible="True" />
                <asp:BoundField DataField="AddressLine2" HeaderText="AddressLine2" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="City" HeaderText="City" ItemStyle-Wrap="False" ItemStyle-Width="100px" Visible="True" />
                <asp:BoundField DataField="State" HeaderText="State" ItemStyle-Wrap="False" ItemStyle-Width="40px" Visible="True" />
                <asp:BoundField DataField="Zip" HeaderText="Zip" ItemStyle-Wrap="False" ItemStyle-Width="50px" Visible="True" />
                <asp:BoundField DataField="Zip4" HeaderText="Zip4" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="WindowStartTime" HeaderText="Open" ItemStyle-Wrap="False" ItemStyle-Width="75px" Visible="True" HtmlEncode="true" DataFormatString="{0:HH:mm}" />
                <asp:BoundField DataField="WindowEndTime" HeaderText="Close" ItemStyle-Wrap="False" ItemStyle-Width="75px" Visible="True" HtmlEncode="true" DataFormatString="{0:HH:mm}" />
                <asp:BoundField DataField="ContactName" HeaderText="Contact" ItemStyle-Wrap="False" ItemStyle-Width="100px" Visible="True" />
                <asp:BoundField DataField="ContactPhone" HeaderText="Phone" ItemStyle-Wrap="False" ItemStyle-Width="75px" Visible="True" />
                <asp:BoundField DataField="ContactEmail" HeaderText="Email" ItemStyle-Wrap="False" ItemStyle-Width="100px" Visible="True" />
                <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Wrap="False" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center" Visible="True" />
                <asp:BoundField DataField="LastUpdated" HeaderText="LastUpdated" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="UserID" HeaderText="UserID" ItemStyle-Wrap="False" Visible="False" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="odsShippers" runat="server" TypeName="Argix.Freight.FreightGateway" SelectMethod="ViewLTLShippers">
            <SelectParameters>
                <asp:Parameter Name="clientID" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <br />
    <div>
        <asp:UpdatePanel ID="upnlShippersCommands" runat="server" UpdateMode="Always" >
        <ContentTemplate>
        <asp:Button ID="btnShipperNew" runat="server" Text="  New  " CssClass="submit" CommandName="ShipperNew" OnCommand="OnManageCommand" />
        <asp:Button ID="btnShipperUpdate" runat="server" Text="Update" CssClass="submit" CommandName="ShipperUpdate" OnCommand="OnManageCommand" />
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:View>
<asp:View ID="vwConsignees" runat="server">
    <div class="subtitle">Consignees for <asp:Label ID="lblConsigneesClient" runat="server" Text="" /></div>
    <br />
    <div style="width:890px; height:275px; margin:0px 0px 0px 0px; padding:0px 0px 0px 0px; overflow-x:hidden; overflow-y:scroll; white-space:nowrap;">
        <asp:UpdatePanel runat="server" ID="upnlConsignees" UpdateMode="Conditional">
        <ContentTemplate>
        <asp:GridView ID="grdConsignees" runat="server" Width="100%" DataSourceID="odsConsignees" DataKeyNames="ID" AutoGenerateColumns="false" OnSelectedIndexChanged="OnConsigneeSelected" >
            <Columns>
                <asp:CommandField HeaderStyle-Width="16px" ButtonType="Image" ShowSelectButton="True" SelectImageUrl="~/App_Themes/Argix/Images/select.gif" />
                <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="ClientID" HeaderText="ClientID" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="ClientName" HeaderText="ClientName" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-Wrap="False" ItemStyle-Width="125px" Visible="True" />
                <asp:BoundField DataField="AddressLine1" HeaderText="Address" ItemStyle-Wrap="False" ItemStyle-Width="125px" Visible="True" />
                <asp:BoundField DataField="AddressLine2" HeaderText="AddressLine2" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="City" HeaderText="City" ItemStyle-Wrap="False" ItemStyle-Width="100px" Visible="True" />
                <asp:BoundField DataField="State" HeaderText="State" ItemStyle-Wrap="False" ItemStyle-Width="40px" Visible="True" />
                <asp:BoundField DataField="Zip" HeaderText="Zip" ItemStyle-Wrap="False" ItemStyle-Width="50px" Visible="True" />
                <asp:BoundField DataField="Zip4" HeaderText="Zip4" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="WindowStartTime" HeaderText="Open" ItemStyle-Wrap="False" ItemStyle-Width="75px" Visible="True" HtmlEncode="true" DataFormatString="{0:HH:mm}" />
                <asp:BoundField DataField="WindowEndTime" HeaderText="Close" ItemStyle-Wrap="False" ItemStyle-Width="75px" Visible="True" HtmlEncode="true" DataFormatString="{0:HH:mm}" />
                <asp:BoundField DataField="ContactName" HeaderText="Contact" ItemStyle-Wrap="False" ItemStyle-Width="100px" Visible="True" />
                <asp:BoundField DataField="ContactPhone" HeaderText="Phone" ItemStyle-Wrap="False" ItemStyle-Width="75px" Visible="True" />
                <asp:BoundField DataField="ContactEmail" HeaderText="Email" ItemStyle-Wrap="False" ItemStyle-Width="100px" Visible="True" />
                <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Wrap="False" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center" Visible="True" />
                <asp:BoundField DataField="LastUpdated" HeaderText="LastUpdated" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="UserID" HeaderText="UserID" ItemStyle-Wrap="False" Visible="False" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="odsConsignees" runat="server" TypeName="Argix.Freight.FreightGateway" SelectMethod="ViewLTLConsignees">
            <SelectParameters>
                <asp:Parameter Name="clientID" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <br />
    <div>
        <asp:UpdatePanel ID="upnlConsigneesCommands" runat="server" UpdateMode="Always" >
        <ContentTemplate>
        <asp:Button ID="btnConsigneeNew" runat="server" Text="  New  " CssClass="submit" CommandName="ConsigneeNew" OnCommand="OnManageCommand" />
        <asp:Button ID="btnConsigneeUpdate" runat="server" Text="Update" CssClass="submit" CommandName="ConsigneeUpdate" OnCommand="OnManageCommand" />
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:View>
</asp:MultiView>
<br />
</div>
</asp:Content>
