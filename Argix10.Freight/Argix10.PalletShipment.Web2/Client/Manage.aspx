<%@ Page Title="Manage" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="Manage.aspx.cs" Inherits="Manage" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpBody">
    <script type="text/jscript">
        $(document).ready(function () {
            $("#tabs").tabs( { active:<%=this.mView %> } );
       
            $("#<%=btnAccountUpdate.ClientID %>").button();
            $("#<%=txtContactPhone.ClientID %>").inputmask({ "mask": "999-999-9999" });
            $("#<%=btnShipperNew.ClientID %>").button();
            $("#<%=btnShipperUpdate.ClientID %>").button();        
            $("#<%=btnConsigneeNew.ClientID %>").button();
            $("#<%=btnConsigneeUpdate.ClientID %>").button();
        });

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(OnBeginRequest);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(OnEndRequest);
        function OnBeginRequest(sender, args) { }
        function OnEndRequest(sender, args) {
            $("#<%=btnAccountUpdate.ClientID %>").button();
            $("#<%=txtContactPhone.ClientID %>").inputmask({ "mask": "999-999-9999" });
            $("#<%=btnShipperNew.ClientID %>").button();
            $("#<%=btnShipperUpdate.ClientID %>").button();        
            $("#<%=btnConsigneeNew.ClientID %>").button();
            $("#<%=btnConsigneeUpdate.ClientID %>").button();
        }
</script>
<div class="subtitle">Manage Account for&nbsp;<asp:Label ID="lblClientName" runat="server" Text="" /></div>
<div id="tabs">
  <ul>
    <li><a href="#tabAccount">Client</a></li>
    <li><a href="#tabShippers">Shippers</a></li>
    <li><a href="#tabConsignees">Consignees</a></li>
  </ul>
  <div id="tabAccount" class="enroll">
    <div style="float:right"><asp:HyperLink ID="lnkChangePassword" runat="server" NavigateUrl="~/Account/ChangePassword.aspx">Change your password</asp:HyperLink>.</div>
    <asp:ValidationSummary ID="vsClient" runat="server" ValidationGroup="vgClient" />
    <asp:CustomValidator id="cvStatus" runat="server" ValidationGroup="vgClient" EnableClientScript="False" />
    <table>
        <tr><td><label for="txtCompanyName">Company Name</label></td></tr>
        <tr><td><asp:TextBox ID="txtCompanyName" runat="server" Width="275px" ReadOnly="true" /></td></tr>
        <tr><td><label for="txtCompanyStreet">Company Address</label></td></tr>
        <tr><td><asp:TextBox ID="txtCompanyStreet" runat="server" Width="275px" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCompanyCity" runat="server" Width="175px" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCompanyState" MaxLength="2" runat="server" Width="50px" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCompanyZip" MaxLength="5" runat="server" Width="75px" />
        </td></tr>
        <tr><td><div class="sublabel" style="float:left; width:275px; margin-right:18px">Street</div><div class="sublabel" style="float:left; width:175px; margin-right:18px">City</div><div class="sublabel" style="float:left; width:50px; margin-right:18px">State</div><div class="sublabel" style="float:left; width:75px">Zipcode</div></td></tr>
        <tr><td><label for="txtContactName">Contact Info</label></td></tr>
        <tr><td><asp:TextBox ID="txtContactName" runat="server" Width="275px" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtContactPhone" runat="server" Width="100px" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtContactEmail" runat="server" Width="175px" />
        </td></tr>
        <tr><td><div class="sublabel" style="float:left; width:275px; margin-right:18px">Name</div><div class="sublabel" style="float:left; width:100px; margin-right:18px">Phone Number</div><div class="sublabel" style="float:left; width:175px">Email Address</div></td></tr>
    </table>
    <br /><div class="redline"></div><br />
    <table>
        <tr><td><label for="txtCorporateName">Corporate Name</label></td></tr>
        <tr><td><asp:TextBox ID="txtCorporateName" runat="server" Width="275px" /></td></tr>
        <tr><td><label for="txtCorporateStreet">Corporate Address</label></td></tr>
        <tr><td><asp:TextBox ID="txtCorporateStreet" runat="server" Width="275px" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCorporateCity" runat="server" Width="175px" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCorporateState" MaxLength="2" runat="server" Width="50px" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCorporateZip" MaxLength="5" runat="server" Width="75px" />
        </td></tr>
        <tr><td><div class="sublabel" style="float:left; width:275px; margin-right:18px">Street</div><div class="sublabel" style="float:left; width:175px; margin-right:18px">City</div><div class="sublabel" style="float:left; width:50px; margin-right:18px">State</div><div class="sublabel" style="float:left; width:75px">Zipcode</div></td></tr>
        <tr><td>&nbsp;</td></tr>
        <tr><td><asp:TextBox ID="txtTaxID" runat="server" Width="100px" Visible="false" /></td></tr>
    </table>
    <br /><div class="redline"></div><br />
    <table>
        <tr><td><label for="txtBillingStreet">Billing Address</label></td></tr>
        <tr><td><asp:TextBox ID="txtBillingStreet" runat="server" Width="275px" ReadOnly="true" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBillingCity" runat="server" Width="175px" ReadOnly="true" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBillingState" MaxLength="2" runat="server" Width="50px" ReadOnly="true" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBillingZip" MaxLength="5" runat="server" Width="75px" ReadOnly="true" />
        </td></tr>
        <tr><td><div class="sublabel" style="float:left; width:275px; margin-right:18px">Street</div><div class="sublabel" style="float:left; width:175px; margin-right:18px">City</div><div class="sublabel" style="float:left; width:50px; margin-right:18px">State</div><div class="sublabel" style="float:left; width:75px">Zipcode</div></td></tr>
    </table>
    <br />
    <div>
        <asp:Button ID="btnAccountUpdate" runat="server" Text="Update" CommandName="AccountUpdate" OnCommand="OnManageCommand" />
    </div>
    <asp:RequiredFieldValidator ID="rfvCompanyName" runat="server" ControlToValidate="txtCompanyName" ErrorMessage="Company name is required." ValidationGroup="vgClient" />
    <asp:RequiredFieldValidator ID="rfvCompanyStreet" runat="server" ControlToValidate="txtCompanyStreet" ErrorMessage="Company street is required." ValidationGroup="vgClient" />
    <asp:RequiredFieldValidator ID="rfvCompanyCity" runat="server" ControlToValidate="txtCompanyCity" ErrorMessage="Company city is required." ValidationGroup="vgClient" />
    <asp:RequiredFieldValidator ID="rfvCompanyState" runat="server" ControlToValidate="txtCompanyState" ErrorMessage="Company state is required." ValidationGroup="vgClient" />
    <asp:RequiredFieldValidator ID="rfvCompanyZip" runat="server" ControlToValidate="txtCompanyZip" ErrorMessage="Company zip is required." ValidationGroup="vgClient" />
    <asp:RequiredFieldValidator ID="rfvContactName" runat="server" ControlToValidate="txtContactName" ErrorMessage="Contact name is required." ValidationGroup="vgClient" />
    <asp:RequiredFieldValidator ID="rfvContactEmail" runat="server" ControlToValidate="txtContactEmail" ErrorMessage="Contact email is required." ValidationGroup="vgClient" />
  </div>
  <div id="tabShippers">
    <br />
    <div class="sublabel">Select a shipper using the black arrows, and then use the buttons below (if applicable).</div>
    <div class="gridviewbody">
        <asp:UpdatePanel runat="server" ID="upnlShippers" UpdateMode="Conditional">
        <ContentTemplate>
        <asp:GridView ID="grdShippers" runat="server" Width="100%" DataSourceID="odsShippers" DataKeyNames="ShipperNumber" AutoGenerateColumns="false" OnSelectedIndexChanged="OnShipperSelected" >
            <Columns>
                <asp:CommandField HeaderStyle-Width="16px" ButtonType="Image" ShowSelectButton="True" SelectImageUrl="~/App_Themes/Argix/Images/select.gif" />
                <asp:BoundField DataField="ShipperNumber" HeaderText="Number" ItemStyle-Wrap="False" ItemStyle-Width="50px" Visible="False" />
                <asp:BoundField DataField="ClientNumber" HeaderText="Client#" ItemStyle-Wrap="False" ItemStyle-Width="50px" Visible="False" />
                <asp:BoundField DataField="ClientName" HeaderText="Client" ItemStyle-Wrap="False" ItemStyle-Width="100px" Visible="False" />
                <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-Wrap="False" ItemStyle-Width="125px" Visible="True" />
                <asp:BoundField DataField="AddressLine1" HeaderText="Address Line 1" ItemStyle-Wrap="False" ItemStyle-Width="125px" Visible="True" />
                <asp:BoundField DataField="AddressLine2" HeaderText="Address Line 2" ItemStyle-Wrap="True" ItemStyle-Width="100px" Visible="True" />
                <asp:BoundField DataField="City" HeaderText="City" ItemStyle-Wrap="False" ItemStyle-Width="75px" Visible="True" />
                <asp:BoundField DataField="State" HeaderText="State" ItemStyle-Wrap="False" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Center" Visible="True" />
                <asp:BoundField DataField="Zip" HeaderText="Zip" ItemStyle-Wrap="False" ItemStyle-Width="50px" Visible="True" />
                <asp:BoundField DataField="Zip4" HeaderText="Zip4" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="ContactName" HeaderText="Contact" ItemStyle-Wrap="False" ItemStyle-Width="75px" Visible="True" />
                <asp:BoundField DataField="ContactPhone" HeaderText="Phone" ItemStyle-Wrap="False" ItemStyle-Width="75px" Visible="False" />
                <asp:BoundField DataField="ContactEmail" HeaderText="Email" ItemStyle-Wrap="False" ItemStyle-Width="100px" Visible="False" />
                <asp:BoundField DataField="WindowTimeStart" HeaderText="Open" ItemStyle-Wrap="False" ItemStyle-Width="50px" Visible="False" HtmlEncode="true" DataFormatString="{0:HH:mm}" />
                <asp:BoundField DataField="WindowTimeEnd" HeaderText="Close" ItemStyle-Wrap="False" ItemStyle-Width="50px" Visible="False" HtmlEncode="true" DataFormatString="{0:HH:mm}" />
                <asp:BoundField DataField="IsActive" HeaderText="IsActive" ItemStyle-Wrap="False" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center" Visible="False" />
                <asp:BoundField DataField="LastUpdated" HeaderText="LastUpdated" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="UserID" HeaderText="UserID" ItemStyle-Wrap="False" Visible="False" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="odsShippers" runat="server" TypeName="Argix.Freight.FreightGateway" SelectMethod="ViewLTLShippers">
            <SelectParameters>
                <asp:Parameter Name="clientNumber" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <br />
    <div>
        <asp:UpdatePanel ID="upnlShippersCommands" runat="server" UpdateMode="Always" >
        <ContentTemplate>
            <asp:Button ID="btnShipperNew" runat="server" Text="  New  " CommandName="ShipperNew" OnCommand="OnManageCommand" />
            <asp:Button ID="btnShipperUpdate" runat="server" Text="Edit" CommandName="ShipperUpdate" OnCommand="OnManageCommand" />
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
  </div>
  <div id="tabConsignees">
    <br />
    <div class="sublabel">Select a consignee using the black arrows, and then use the buttons below (if applicable).</div>
    <div class="gridviewbody">
        <asp:UpdatePanel runat="server" ID="upnlConsignees" UpdateMode="Conditional">
        <ContentTemplate>
        <asp:GridView ID="grdConsignees" runat="server" Width="100%" DataSourceID="odsConsignees" DataKeyNames="ConsigneeNumber" AutoGenerateColumns="false" OnSelectedIndexChanged="OnConsigneeSelected" >
            <Columns>
                <asp:CommandField HeaderStyle-Width="16px" ButtonType="Image" ShowSelectButton="True" SelectImageUrl="~/App_Themes/Argix/Images/select.gif" />
                <asp:BoundField DataField="ConsigneeNumber" HeaderText="Number" ItemStyle-Wrap="False" ItemStyle-Width="50px" Visible="False" />
                <asp:BoundField DataField="ClientNumber" HeaderText="Client#" ItemStyle-Wrap="False" ItemStyle-Width="50px" Visible="False" />
                <asp:BoundField DataField="ClientName" HeaderText="Client" ItemStyle-Wrap="False" ItemStyle-Width="100px" Visible="False" />
                <asp:BoundField DataField="Name" HeaderText="Name" ItemStyle-Wrap="False" ItemStyle-Width="125px" Visible="True" />
                <asp:BoundField DataField="AddressLine1" HeaderText="Address Line 1" ItemStyle-Wrap="False" ItemStyle-Width="125px" Visible="True" />
                <asp:BoundField DataField="AddressLine2" HeaderText="Address Line 2" ItemStyle-Wrap="True" ItemStyle-Width="100px" Visible="True" />
                <asp:BoundField DataField="City" HeaderText="City" ItemStyle-Wrap="False" ItemStyle-Width="75px" Visible="True" />
                <asp:BoundField DataField="State" HeaderText="State" ItemStyle-Wrap="False" ItemStyle-Width="40px" Visible="True" />
                <asp:BoundField DataField="Zip" HeaderText="Zip" ItemStyle-Wrap="False" ItemStyle-Width="30px" ItemStyle-HorizontalAlign="Center" Visible="True" />
                <asp:BoundField DataField="Zip4" HeaderText="Zip4" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="ContactName" HeaderText="Contact" ItemStyle-Wrap="False" ItemStyle-Width="75px" Visible="True" />
                <asp:BoundField DataField="ContactPhone" HeaderText="Phone" ItemStyle-Wrap="False" ItemStyle-Width="75px" Visible="False" />
                <asp:BoundField DataField="ContactEmail" HeaderText="Email" ItemStyle-Wrap="False" ItemStyle-Width="100px" Visible="False" />
                <asp:BoundField DataField="WindowTimeStart" HeaderText="Open" ItemStyle-Wrap="False" ItemStyle-Width="50px" Visible="False" HtmlEncode="true" DataFormatString="{0:HH:mm}" />
                <asp:BoundField DataField="WindowTimeEnd" HeaderText="Close" ItemStyle-Wrap="False" ItemStyle-Width="50px" Visible="False" HtmlEncode="true" DataFormatString="{0:HH:mm}" />
                <asp:BoundField DataField="IsActive" HeaderText="IsActive" ItemStyle-Wrap="False" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center" Visible="False" />
                <asp:BoundField DataField="LastUpdated" HeaderText="LastUpdated" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="UserID" HeaderText="UserID" ItemStyle-Wrap="False" Visible="False" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="odsConsignees" runat="server" TypeName="Argix.Freight.FreightGateway" SelectMethod="ViewLTLConsignees">
            <SelectParameters>
                <asp:Parameter Name="clientNumber" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <br />
    <div>
        <asp:UpdatePanel ID="upnlConsigneesCommands" runat="server" UpdateMode="Always" >
        <ContentTemplate>
            <asp:Button ID="btnConsigneeNew" runat="server" Text="  New  " CommandName="ConsigneeNew" OnCommand="OnManageCommand" />
            <asp:Button ID="btnConsigneeUpdate" runat="server" Text="Edit" CommandName="ConsigneeUpdate" OnCommand="OnManageCommand" />
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
  </div>
</div>
</asp:Content>
