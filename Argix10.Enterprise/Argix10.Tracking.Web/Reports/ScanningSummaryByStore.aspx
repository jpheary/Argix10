<%@ Page Language="C#" MasterPageFile="~/MasterPages/Reports.master" AutoEventWireup="true" CodeFile="ScanningSummaryByStore.aspx.cs" Inherits="ScanningSummaryByStore" %>
<%@ MasterType VirtualPath="~/MasterPages/Reports.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cScript" runat="server" ContentPlaceHolderID="cpScript">
</asp:Content>
<asp:Content ID="cSetup" ContentPlaceHolderID="cpSetup" runat="Server">
    <div class="reports">
        <div>
            <fieldset>
                <legend>Setup</legend>
                <label for="cboDateParam">Retail</label>
                    <asp:DropDownList id="cboDateParam" runat="server" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="OnDateParamChanged">
                        <asp:ListItem Text="Week" Value="Week" Selected="True" />
                        <asp:ListItem Text="Month" Value="Month" />
                        <asp:ListItem Text="Quarter" Value="Quarter" />
                        <asp:ListItem Text="YTD" Value="YTD" />
                    </asp:DropDownList>
                    &nbsp;
                    <asp:DropDownList id="cboDateValue" runat="server" Width="250px" DataSourceID="odsDates" DataTextField="Value" DataValueField="Value" AutoPostBack="True" />
                <br />
                <label for="cboClient">Client</label><asp:DropDownList id="cboClient" runat="server" Width="300px" DataSourceID="odsClients" DataTextField="ClientName" DataValueField="ClientNumber" AutoPostBack="True" OnSelectedIndexChanged="OnClientChanged" /><br />
                <label for="cboParam">Scope</label>
                    <asp:DropDownList id="cboParam" runat="server" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="OnScopeParamChanged">
                        <asp:ListItem Text="Divisions" Value="Divisions" Selected="True" />
                        <asp:ListItem Text="Store" Value="Stores" />
                    </asp:DropDownList>
                    &nbsp;
                    <asp:TextBox ID="txtStore" runat="server" Width="100px" Visible="false" AutoPostBack="True" OnTextChanged="OnStoreChanged" />
                <br />
            </fieldset>
        </div>
    </div>
    <asp:ObjectDataSource ID="odsDates" runat="server" TypeName="Argix.EnterpriseService" SelectMethod="GetRetailDates" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="600">
        <SelectParameters>
            <asp:ControlParameter Name="scope" ControlID="cboDateParam" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsClients" runat="server" TypeName="Argix.EnterpriseService" SelectMethod="GetSecureClients" EnableCaching="false" CacheExpirationPolicy="Sliding" CacheDuration="900">
        <SelectParameters>
            <asp:Parameter Name="activeOnly" DefaultValue="true" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>