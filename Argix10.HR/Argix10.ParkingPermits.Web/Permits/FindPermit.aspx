<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="FindPermit.aspx.cs" Inherits="_FindPermit" %>
<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cphMeta">
</asp:Content>
<asp:Content ID="cBody" runat="server" ContentPlaceHolderID="cphBody">
    <script type="text/javascript">
        $(document).ready(function () {
            jQueryBind();
        });

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(OnBeginRequest);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(OnEndRequest);
        function OnBeginRequest(sender, args) { }
        function OnEndRequest(sender, args) { jQueryBind(); }
        function jQueryBind() {
            $("#<%=btnSearch.ClientID %>").button();
        }
    </script>
    <div class="subtitle">Search Permits</div>
    <div class="permits">
        <label for="dduSearchBy">By</label><asp:DropDownList ID="dduSearchBy" runat="server" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="OnSearchByChanged"><asp:ListItem Text="Permit#" /><asp:ListItem Text="License Plate#" /><asp:ListItem Text="Vehicle Description" /></asp:DropDownList>
        &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" CommandName="Search" OnCommand="OnManageCommand" />
        <asp:UpdatePanel ID="upnlView" runat="server" UpdateMode="Conditional" >
        <ContentTemplate>
            <asp:MultiView runat="server" ID="mvSearch" ActiveViewIndex="0">
                <asp:View ID="vwPermit" runat="server">
                    <div>
                        <fieldset>
                            <legend>By Permit#</legend>
                            <label for="txtPermitNumber">Permit#</label><asp:TextBox ID="txtPermitNumber" runat="server" MaxLength="5" Width="50px" /><br />
                            <br />
                        </fieldset>
                    </div>
                </asp:View>
                <asp:View ID="vwPlate" runat="server">
                    <div>
                        <fieldset>
                            <legend>By License Plate#</legend>
                            <label for="dduStates">State</label><asp:DropDownList ID="dduStates" runat="server" Width="75px" DataSourceID="odsStates" DataTextField="Name" DataValueField="Name" />
                            <label for="txtPlate">Plate#</label><asp:TextBox ID="txtPlate" runat="server" MaxLength="10" Width="75px" /><br />
                            <br />
                        </fieldset>
                    </div>
                </asp:View>
                <asp:View ID="vwVehicle" runat="server">
                    <div>
                        <fieldset>
                            <legend>By Vehicle Description</legend>
                            <label for="txtYear">Year</label><asp:TextBox ID="txtYear" runat="server" MaxLength="4" Width="50px" />
                            <label for="txtColor">Color</label><asp:TextBox ID="txtColor" runat="server" MaxLength="20" Width="75px" /><br />
                            <label for="txtMake">Make</label><asp:TextBox ID="txtMake" runat="server" MaxLength="20" Width="200px" /><br />
                            <label for="txtModel">Model</label><asp:TextBox ID="txtModel" runat="server" MaxLength="20" Width="200px" /><br />
                            <br />
                        </fieldset>
                    </div>
                </asp:View>
            </asp:MultiView>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="dduSearchBy" EventName="SelectedIndexChanged" />
        </Triggers>
        </asp:UpdatePanel>
        <div class="listviewtitle">Parking Permits</div>
        <div class="listviewbody">
            <asp:UpdatePanel ID="upnlPermits" runat="server" UpdateMode="Conditional" >
            <ContentTemplate>
                <asp:ListView ID="lsvPermits" runat="server" >
                    <LayoutTemplate>
                        <div id="itemPlaceholder" runat="server" style="width:100%" ></div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <table style="width:100%; background-color:#ffffff">
                            <tr><td style="width:150px; text-align:left; font-weight:bold"><%# Eval("Number")%></td><td style="text-align:right;"><%# GetStatusDate(Eval("Activated"), Eval("Inactivated"))%></td>
                                <td rowspan="4" style="width:24px">&nbsp;</td></tr>
                            <tr><td><%# GetVehiclePlate(Eval("IssueState"), Eval("PlateNumber"))%></td><td style="text-align:right;"><%# GetStatusReason(Eval("Inactivated"), Eval("InactiveReason"))%></td></tr>
                            <tr><td colspan="2"><%# GetVehicle(Eval("Year"), Eval("Make"), Eval("Model"), Eval("Color"))%></td></tr>
                            <tr><td><%# GetContact(Eval("ContactFirstName"), Eval("ContactMiddleName"), Eval("ContactLastName"), Eval("ContactPhoneNumber"))%></td><td style="text-align:right"><%# Eval("ContactPhoneNumber")%></td></tr>
                            <tr><td colspan="3"><hr /></td></tr>
                        </table>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table style="width:100%; background-color:#ffffff">
                            <tr style="background-color:white; height:100px"><td>No permits found.</td></tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dduSearchBy" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Command" />
            </Triggers>
            </asp:UpdatePanel>
        </div>
        <div class="gridviewbody">
            <asp:UpdatePanel ID="upnlSearch" runat="server" UpdateMode="Conditional" >
            <ContentTemplate>
                <asp:GridView ID="grdSearch" runat="server" width="100%" AutoGenerateColumns="False" DataKeyNames="ID" AllowSorting="true">
                    <Columns>
                        <asp:CommandField ButtonType="Image" HeaderStyle-Width="16px" SelectImageUrl="~/App_Themes/Argix/Images/select.gif" ShowSelectButton="True" />
				        <asp:BoundField DataField="ID" HeaderText="ID#" HeaderStyle-Width="75px" Visible="false" />
				        <asp:BoundField DataField="Number" HeaderText="Number" HeaderStyle-Width="75px" SortExpression="Number" />
				        <asp:BoundField DataField="VehicleID" HeaderText="VehicleID" HeaderStyle-Width="75px" Visible="false" />
				        <asp:BoundField DataField="IssueState" HeaderText="State" HeaderStyle-Width="50px" SortExpression="IssueState" />
				        <asp:BoundField DataField="PlateNumber" HeaderText="Plate#" HeaderStyle-Width="75px" SortExpression="PlateNumber" />
                        <asp:BoundField DataField="Year" HeaderText="Year" HeaderStyle-Width="50px" SortExpression="Year" />
                        <asp:BoundField DataField="Make" HeaderText="Make" HeaderStyle-Width="100px" SortExpression="Make" />
                        <asp:BoundField DataField="Model" HeaderText="Model" HeaderStyle-Width="100px" SortExpression="Model" />
                        <asp:BoundField DataField="Color" HeaderText="Color" HeaderStyle-Width="75px" />				            
				        <asp:BoundField DataField="ContactFirstName" HeaderText="First Name" HeaderStyle-Width="200px" SortExpression="ContactFirstName" />
				        <asp:BoundField DataField="ContactMiddleName" HeaderText="Middle" HeaderStyle-Width="75px" />
                        <asp:BoundField DataField="ContactLastName" HeaderText="Last Name" HeaderStyle-Width="200px" SortExpression="ContactLastName" />
				        <asp:BoundField DataField="ContactPhoneNumber" HeaderText="Phone" HeaderStyle-Width="75px" />
				        <asp:BoundField DataField="BadgeNumber" HeaderText="Badge#" HeaderStyle-Width="75px" SortExpression="BadgeNumber" />
				        <asp:BoundField DataField="Activated" HeaderText="Activated" HeaderStyle-Width="100px" HtmlEncode="false" DataFormatString="{0:MM/dd/yyyy}" SortExpression="Activated" Visible="true" />
				        <asp:BoundField DataField="ActivatedBy" HeaderText="ActivatedBy" HeaderStyle-Width="75px" SortExpression="ActivatedBy" />
				        <asp:BoundField DataField="Inactivated" HeaderText="Inactivated" HeaderStyle-Width="100px" HtmlEncode="false" DataFormatString="{0:MM/dd/yyyy}" SortExpression="Inactivated" Visible="true" />
				        <asp:BoundField DataField="InactivatedBy" HeaderText="InactivatedBy" HeaderStyle-Width="75px" />
				        <asp:BoundField DataField="InactiveReason" HeaderText="Inactive Reason" HeaderStyle-Width="200px" />
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dduSearchBy" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Command" />
            </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:ObjectDataSource ID="odsStates" runat="server" TypeName="Argix.HR.PermitGateway" SelectMethod="GetStateList" />
</asp:Content>
