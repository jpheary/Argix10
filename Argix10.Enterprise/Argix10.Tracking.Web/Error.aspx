<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="Error.aspx.cs" Inherits="App_ErrorMsgs_Error" Title="System Error" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cBody" runat="server" ContentPlaceHolderID="cpBody">
    <div>
        <asp:Label ID="lblError" runat="server" Width="100%" Height="100%" Text=""></asp:Label>
    </div>
</asp:Content>

