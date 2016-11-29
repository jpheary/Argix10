<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="RequestedDeliveryDate.aspx.cs" Inherits="RequestedDeliveryDate" %>
<%@ Register Src="../DualDateTimePicker.ascx" TagName="DualDateTimePicker" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Setup" Runat="Server">
<div class="form" style="width:800px">
    <uc1:DualDateTimePicker ID="ddpDelivery" runat="server" Width="350px" LabelWidth="100px" FromLabel="Delivery Day" ToVisible="false" DateDaysBack="7" DateDaysForward="0" DateDaysSpread="0" OnDateTimeChanged="OnFromToDateChanged" />
</div>
</asp:Content>