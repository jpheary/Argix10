<%@ Page Title="Quick Quote" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="Enroll.aspx.cs" Inherits="Enroll" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpBody">
<script type="text/jscript">
    $(document).ready(function () {
        $("#<%=txtContactPhone.ClientID %>").inputmask({ "mask": "999-999-9999" });

        $("#<%=btnSubmit.ClientID %>").button();
    });
    function useCompanyForCorporate() {
        var chkUseCompanyForCorporate = document.getElementById('chkUseCompanyForCorporate');
        if (chkUseCompanyForCorporate.checked) {
            document.getElementById('<%=txtCorporateName.ClientID %>').value = document.getElementById('<%=txtCompanyName.ClientID %>').value;
            document.getElementById('<%=txtCorporateStreet.ClientID %>').value = document.getElementById('<%=txtCompanyStreet.ClientID %>').value;
            document.getElementById('<%=txtCorporateCity.ClientID %>').value = document.getElementById('<%=txtCompanyCity.ClientID %>').value;
            document.getElementById('<%=txtCorporateState.ClientID %>').value = document.getElementById('<%=txtCompanyState.ClientID %>').value;
            document.getElementById('<%=txtCorporateZip.ClientID %>').value = document.getElementById('<%=txtCompanyZip.ClientID %>').value;
        }
        else {
            document.getElementById('<%=txtCorporateName.ClientID %>').value = '';
            document.getElementById('<%=txtCorporateStreet.ClientID %>').value = '';
            document.getElementById('<%=txtCorporateCity.ClientID %>').value = '';
            document.getElementById('<%=txtCorporateState.ClientID %>').value = '';
            document.getElementById('<%=txtCorporateZip.ClientID %>').value = '';
        }
    }
    function useCompanyForBilling() {
        var chkUseCompanyForBilling = document.getElementById('chkUseCompanyForBilling');
        if (chkUseCompanyForBilling.checked) {
            document.getElementById('<%=txtBillingStreet.ClientID %>').value = document.getElementById('<%=txtCompanyStreet.ClientID %>').value;
            document.getElementById('<%=txtBillingCity.ClientID %>').value = document.getElementById('<%=txtCompanyCity.ClientID %>').value;
            document.getElementById('<%=txtBillingState.ClientID %>').value = document.getElementById('<%=txtCompanyState.ClientID %>').value;
            document.getElementById('<%=txtBillingZip.ClientID %>').value = document.getElementById('<%=txtCompanyZip.ClientID %>').value;
        }
        else {
            document.getElementById('<%=txtBillingStreet.ClientID %>').value = '';
            document.getElementById('<%=txtBillingCity.ClientID %>').value = '';
            document.getElementById('<%=txtBillingState.ClientID %>').value = '';
            document.getElementById('<%=txtBillingZip.ClientID %>').value = '';
        }
    }
</script>
<div class="subtitle">Enroll</div>
<div class="enroll">
    <p>Please provide the following information so that we may acknowledge and process your request for enrollment.</p>
    <asp:ValidationSummary ID="vsEnroll" runat="server" ValidationGroup="vgEnroll" />
    <table>
        <tr><td><label for="txtUserName">Username</label></td></tr>
        <tr><td><asp:TextBox ID="txtUserName" runat="server" Width="150px" /></td></tr>
        <tr><td><label for="txtPassword">Password</label></td></tr>
        <tr><td><asp:TextBox ID="txtPassword" runat="server" Width="150px" TextMode="Password" /></td></tr>
        <tr><td><label for="txtConfirmPassword">Confirm Password</label></td></tr>
        <tr><td><asp:TextBox ID="txtConfirmPassword" runat="server" Width="150px" TextMode="Password" /></td></tr>
    </table>
    <br /><div class="redline"></div><br />
    <table>
        <tr><td><label for="txtCompanyName">Company Name</label></td></tr>
        <tr><td><asp:TextBox ID="txtCompanyName" runat="server" Width="275px" /></td></tr>
        <tr><td><label for="txtCompanyStreet">Company Address</label></td></tr>
        <tr><td><asp:TextBox ID="txtCompanyStreet" runat="server" Width="275px" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCompanyCity" runat="server" Width="175px" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCompanyState" runat="server" MaxLength="2" Width="50px" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCompanyZip" runat="server" MaxLength="5" Width="75px" />
        </td></tr>
        <tr><td><div class="sublabel" style="float:left; width:275px; margin-right:18px">Street</div><div class="sublabel" style="float:left; width:175px; margin-right:18px">City</div><div class="sublabel" style="float:left; width:50px; margin-right:18px">State</div><div class="sublabel" style="float:left; width:75px">Zipcode</div></td></tr>
        <tr><td><label for="txtContactName">Contact Info</label></td></tr>
        <tr><td><asp:TextBox ID="txtContactName" runat="server" Width="275px" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtContactPhone" runat="server" Width="100px" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtContactEmail" runat="server" Width="200px" />
        </td></tr>
        <tr><td><div class="sublabel" style="float:left; width:275px; margin-right:18px">Name</div><div class="sublabel" style="float:left; width:100px; margin-right:18px">Phone Number</div><div class="sublabel" style="float:left; width:200px">Email Address</div></td></tr>
    </table>
    <br /><div class="redline"></div><br />
    <table>
        <tr><td><input id="chkUseCompanyForCorporate" type="checkbox" value="false" onclick="javascript: useCompanyForCorporate();" />Use Company Name and Address</td></tr>
        <tr><td><label for="txtCorporateName">Corporate Name</label></td></tr>
        <tr><td><asp:TextBox ID="txtCorporateName" runat="server" Width="275px" /></td></tr>
        <tr><td><label for="txtCorporateStreet">Corporate Address</label></td></tr>
        <tr><td><asp:TextBox ID="txtCorporateStreet" runat="server" Width="275px" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCorporateCity" runat="server" Width="175px" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCorporateState" runat="server" MaxLength="2" Width="50px" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCorporateZip" runat="server" MaxLength="5" Width="75px" />
        </td></tr>
        <tr><td><div class="sublabel" style="float:left; width:275px; margin-right:18px">Street</div><div class="sublabel" style="float:left; width:175px; margin-right:18px">City</div><div class="sublabel" style="float:left; width:50px; margin-right:18px">State</div><div class="sublabel" style="float:left; width:75px">Zipcode</div></td></tr>
    </table>
    <br /><div class="redline"></div><br />
    <table>
        <tr><td><input id="chkUseCompanyForBilling" type="checkbox" value="false" onclick="javascript: useCompanyForBilling();" />Use Company Address</td></tr>
        <tr><td><label for="txtBillingStreet">Billing Address</label></td></tr>
        <tr><td><asp:TextBox ID="txtBillingStreet" runat="server" Width="275px" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBillingCity" runat="server" Width="175px" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBillingState" runat="server" MaxLength="2" Width="50px" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBillingZip" runat="server" MaxLength="5" Width="75px" />
        </td></tr>
        <tr><td><div class="sublabel" style="float:left; width:275px; margin-right:18px">Street</div><div class="sublabel" style="float:left; width:175px; margin-right:18px">City</div><div class="sublabel" style="float:left; width:50px; margin-right:18px">State</div><div class="sublabel" style="float:left; width:75px">Zipcode</div></td></tr>
    </table>
    <br /><br />
    <div><asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="vgEnroll" OnClick="OnSubmit" /></div>
    <br />
    <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="txtUserName" ErrorMessage="User name is required." ValidationGroup="vgEnroll" />
    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Password is required." ValidationGroup="vgEnroll" />
    <asp:RequiredFieldValidator ID="rfvConfirmPassword" ControlToValidate="txtConfirmPassword" ErrorMessage="Confirm Password is required." runat="server" ValidationGroup="vgEnroll" />
    <asp:CompareValidator ID="cvPasswordCompare" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" ErrorMessage="The Password and Confirmation Password must match." ValidationGroup="vgEnroll" />
    <asp:RequiredFieldValidator ID="rfvCompanyName" runat="server" ControlToValidate="txtCompanyName" ErrorMessage="Company name is required." ValidationGroup="vgEnroll" />
    <asp:RequiredFieldValidator ID="rfvCompanyStreet" runat="server" ControlToValidate="txtCompanyStreet" ErrorMessage="Company street is required." ValidationGroup="vgEnroll" />
    <asp:RequiredFieldValidator ID="rfvCompanyCity" runat="server" ControlToValidate="txtCompanyCity" ErrorMessage="Company city is required." ValidationGroup="vgEnroll" />
    <asp:RequiredFieldValidator ID="rfvCompanyState" runat="server" ControlToValidate="txtCompanyState" ErrorMessage="Company state is required." ValidationGroup="vgEnroll" />
    <asp:RequiredFieldValidator ID="rfvCompanyZip" runat="server" ControlToValidate="txtCompanyZip" ErrorMessage="Company zip code is required." ValidationGroup="vgEnroll" />
    <asp:RequiredFieldValidator ID="rfvContactName" runat="server" ControlToValidate="txtContactName" ErrorMessage="Contact name is required."  ValidationGroup="vgEnroll" />
    <asp:RequiredFieldValidator ID="rfvContactEmail" runat="server" ControlToValidate="txtContactEmail" ErrorMessage="Contact email is required." ValidationGroup="vgEnroll" />
</div>
</asp:Content>
