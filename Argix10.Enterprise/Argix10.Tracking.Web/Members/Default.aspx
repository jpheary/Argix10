<%@ Page Language="C#" Title="Argix Logistics Carton Tracking" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="Login" />
    <meta name="description" content="Login"/>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cpBody" Runat="Server">
    <div class="tritron">
         <asp:HyperLink ID="lnkTracking" runat="server" NavigateUrl="~/Members/Tracking.aspx" Text="">
            <img id="imgTracking" runat="server" src="~/App_Themes/Argix/Images/tracking.png" alt="Tracking" style="border: 0;" />
        </asp:HyperLink>   
    </div>
    <div class="tritron">
        <asp:HyperLink ID="lnkReports" runat="server" NavigateUrl="~/Members/Reports.aspx" Text="">
            <img id="imgReports" runat="server" src="~/App_Themes/Argix/Images/reports.png" alt="Reports" style="border: 0;" />
        </asp:HyperLink>
    </div>
    <div class="tritron">
        <asp:HyperLink ID="lnkProfile" runat="server" NavigateUrl="~/Members/Profile.aspx" Text="">
            <img id="imgProfile" runat="server" src="~/App_Themes/Argix/Images/profile.png" alt="Profile" style="border: 0;" />
        </asp:HyperLink>
    </div>
</asp:Content>


