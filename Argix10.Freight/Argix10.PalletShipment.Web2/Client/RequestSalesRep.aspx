<%@ Page Title="Request Sales Rep" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="RequestSalesRep.aspx.cs" Inherits="Client_RequestSalesRep" %>
<%@ MasterType VirtualPath="~//MasterPages/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpMeta" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpBody" Runat="Server">
<script type="text/jscript">
    $(document).ready(function () {
        $("#<%=btnSubmit.ClientID %>").button();
        $("#<%=btnCancel.ClientID %>").button();
    });
</script>
<div class="subtitle">Request a Sales Rep</div>
<div style="width:600px">
    <p>
        An Argix Logistics Sales Representative can serve as a proxy, on your behalf, for 
        obtaining quotes, creating shipments, and tracking shipments.
    </p>
    <p>
        If you would like to be represented by an Argix Logistics sales representative,
        click the Submit button below and we will contact you regarding your request.
    </p>
    <div>
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="submit" CommandName="Submit" OnCommand="OnCommand" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="submit" CommandName="Cancel" OnCommand="OnCommand" />
    </div>
</div>
</asp:Content>

