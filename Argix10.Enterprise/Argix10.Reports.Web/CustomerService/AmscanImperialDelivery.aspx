<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="AmscanImperialDelivery.aspx.cs" Inherits="AmscanImperialDelivery" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cHead" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="cSetup" ContentPlaceHolderID="Setup" Runat="Server">
<script type="text/javascript">
    $(function () {
        $("#<%=txtDeliveryDate.ClientID %>").datepicker({ minDate: -30, maxDate: +30 });
    });
</script>
<div>
    <div style="margin:50px 0px 0px 25px">
        <label for="txtDeliveryDate">Delivery Date&nbsp;</label>
        <asp:TextBox ID="txtDeliveryDate" runat="server" Width="100px" />
    </div>
</div>
</asp:Content>

