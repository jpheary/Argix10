<%@ Page Title="Load Tenders" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpBody">
<script type="text/jscript">
    $(document).ready(function () {
        $("#<%=txtFromDate.ClientID %>").datepicker({ minDate: -730, maxDate: +0 });
        $("#<%=txtToDate.ClientID %>").datepicker({ minDate: -730, maxDate: +0 });
    });
</script>
<div class="form">
    <table style="width:700px; height:30px">
        <tr>
            <td style="width:325px">Client&nbsp;<asp:DropDownList ID="cboClient" runat="server" width="250px" DataTextField="ClientName" DataValueField="ClientNumber" AutoPostBack="true" OnSelectedIndexChanged="OnClientChanged" onclick="javascript:document.body.style.cursor='wait';" />
                &nbsp;<asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/App_Themes/Argix/Images/refresh.png" Height="18px" ImageAlign="Top" ToolTip="Refresh load tenders" CommandName="Refresh" OnCommand="OnCommand" OnClientClick="javascript:document.body.style.cursor='wait';" />
            </td>
            <td style="width:25px">&nbsp;</td>
            <td>
                From&nbsp;<asp:TextBox ID="txtFromDate" runat="server" Width="75px" OnTextChanged="OnDateChanged" />
                &nbsp;&nbsp;To&nbsp;<asp:TextBox ID="txtToDate" runat="server" Width="75px" OnTextChanged="OnDateChanged" />
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
</div>
<div class="view">
    <asp:UpdatePanel ID="upnlTenders" runat="server" UpdateMode="Conditional" >
    <ContentTemplate>
        <asp:GridView ID="grdTenders" runat="server" width="100%" AutoGenerateColumns="False" DataSourceID="odsTenders" DataKeyNames="Load" AllowSorting="True">
            <Columns>
                <asp:TemplateField HeaderText="" HeaderStyle-Width="24px" >
                    <HeaderTemplate><asp:CheckBox ID="chkAll" runat="server" Enabled="true" AutoPostBack="true" OnCheckedChanged="OnAllTendersSelected"/></HeaderTemplate>
                    <ItemTemplate><asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="OnTenderSelected"/></ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Load" HeaderText="Load#" HeaderStyle-Width="50px" SortExpression="Load" />
                <asp:BoundField DataField="ReferenceNumber" HeaderText="Ref #" HeaderStyle-Width="50px" SortExpression="ReferenceNumber" />
                <asp:BoundField DataField="PONumber" HeaderText="PO #" HeaderStyle-Width="50px" SortExpression="PONumber" />
                <asp:BoundField DataField="Location" HeaderText="Location" HeaderStyle-Width="150px" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="AddressLine1" HeaderText="Address" HtmlEncode="False" NullDisplayText=" " ItemStyle-Wrap="false" />
                <asp:BoundField DataField="AddressLine2" HeaderText="Address Line 2" HtmlEncode="False" NullDisplayText=" " ItemStyle-Wrap="false" Visible="false" />
                <asp:BoundField DataField="City" HeaderText="City" HeaderStyle-Width="100px" />
                <asp:BoundField DataField="StateOrProvince" HeaderText="State" HeaderStyle-Width="25px" />
                <asp:BoundField DataField="PostalCode" HeaderText="Zip" HeaderStyle-Width="50px" />
                <asp:BoundField DataField="LocationNumber" HeaderText="Loc #" HeaderStyle-Width="50px" SortExpression="LocationNumber" />
                <asp:BoundField DataField="LocationID" HeaderText="Loc ID" HeaderStyle-Width="75px" SortExpression="LocationID" />
                <asp:BoundField DataField="Barcode1" HeaderText="Barcode1" HeaderStyle-Width="50px" />
                <asp:BoundField DataField="Barcode2" HeaderText="Barcode2" HeaderStyle-Width="50px" />
            </Columns>
        </asp:GridView>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="cboClient" EventName="SelectedIndexChanged" />
        <asp:AsyncPostBackTrigger ControlID="txtFromDate" EventName="TextChanged" />
        <asp:AsyncPostBackTrigger ControlID="txtToDate" EventName="TextChanged" />
        <asp:AsyncPostBackTrigger ControlID="btnRefresh" EventName="Command" />
    </Triggers>
    </asp:UpdatePanel>
</div>
<br />
<div>
    <asp:UpdatePanel ID="upnlServices" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:Button ID="btnGo" runat="server" Text="  GO  " CssClass="submit" CommandName="Submit" OnCommand="OnCommand" />
    </ContentTemplate>
    <Triggers><asp:AsyncPostBackTrigger ControlID="grdTenders" EventName="DataBound" /></Triggers>
    </asp:UpdatePanel>
</div>
<asp:ObjectDataSource ID="odsTenders" runat="server" TypeName="Argix.TsortGateway" SelectMethod="GetLoadTenders" EnableCaching="false" CacheExpirationPolicy="Sliding" CacheDuration="300" >
    <SelectParameters>
        <asp:ControlParameter Name="clientNumber" ControlID="cboClient" PropertyName="SelectedValue" Type="string" />
        <asp:ControlParameter Name="startDate" ControlID="txtFromDate" PropertyName="Text" Type="String" />
        <asp:ControlParameter Name="endDate" ControlID="txtToDate" PropertyName="Text" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
</asp:Content>
