<%@ Page Language="C#" masterpagefile="~/Default.master" AutoEventWireup="true" CodeFile="Permits.aspx.cs" Inherits="_Permits" %>
<%@ MasterType VirtualPath="~/Default.master" %>

<asp:Content ID="cBody" runat="server" ContentPlaceHolderID="cpBody">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#tabs").tabs({ active: "<%=this.mView %>" });
            jQueryBind();
        });

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(OnBeginRequest);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(OnEndRequest);
        function OnBeginRequest(sender, args) { }
        function OnEndRequest(sender, args) { jQueryBind(); }
        function jQueryBind() {
            $("#<%=btnRegister.ClientID %>").button();
            $("#<%=btnReplace.ClientID %>").button();
            $("#<%=btnRevoke.ClientID %>").button();
            $("#<%=btnChangeVehicle.ClientID %>").button();
            $("#<%=btnSearch.ClientID %>").button();
        }
    </script>
    <div class="subtitle">Parking Permits</div>
    <div id="tabs">
        <ul>
            <li><a href="#tabPermits">Permits</a></li>
            <li><a href="#tabSearch">Search</a></li>
        </ul>
        <div id="tabPermits">
            <div class="gridviewtitle">All Parking Permits</div>
            <div class="gridviewbody">
                <asp:UpdatePanel ID="upnlPermits" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>
                    <asp:GridView ID="grdPermits" runat="server" width="100%" AutoGenerateColumns="False" DataSourceID="odsPermits" DataKeyNames="ID,Inactivated" AllowSorting="true" OnSelectedIndexChanged="OnPermitSelected">
                        <Columns>
                            <asp:CommandField ButtonType="Image" HeaderStyle-Width="16px" SelectImageUrl="~/App_Themes/Argix/Images/select.gif" ShowSelectButton="True" />
				            <asp:BoundField DataField="ID" HeaderText="ID#" HeaderStyle-Width="75px" Visible="false" />
                            <asp:TemplateField HeaderText="Number" ItemStyle-Width="75px" ItemStyle-Wrap="false" SortExpression="Number">
                                <ItemTemplate>
                                    <a style="<%# Eval("Number").ToString().Substring(0, 1).ToLower() == "r" ? "color:#ee2a24" : "color:blue" %>" href="" onclick="javascript:var w=window.open('<%# "Permit.aspx?id=" + Eval("ID").ToString() %>','_blank','width=500px,height=500px,resizable=no,scrollbars=yes,titlebar=no,menubar=no,toolbar=yes,status=no');return false;" title="Click here to view this permit.">&nbsp;<%# Eval("Number").ToString() %></a>
                                </ItemTemplate>
                            </asp:TemplateField>
				            <asp:BoundField DataField="VehicleID" HeaderText="VehicleID" HeaderStyle-Width="75px" ItemStyle-Wrap="false" Visible="false" />
				            <asp:BoundField DataField="IssueState" HeaderText="State" HeaderStyle-Width="50px" ItemStyle-Wrap="false" SortExpression="IssueState" />
				            <asp:BoundField DataField="PlateNumber" HeaderText="Plate#" HeaderStyle-Width="75px" ItemStyle-Wrap="false" SortExpression="PlateNumber" />
                            <asp:BoundField DataField="Year" HeaderText="Year" HeaderStyle-Width="50px" ItemStyle-Wrap="false" SortExpression="Year" />
                            <asp:BoundField DataField="Make" HeaderText="Make" HeaderStyle-Width="100px" ItemStyle-Wrap="false" SortExpression="Make" />
                            <asp:BoundField DataField="Model" HeaderText="Model" HeaderStyle-Width="100px" ItemStyle-Wrap="false" SortExpression="Model" />
                            <asp:BoundField DataField="Color" HeaderText="Color" HeaderStyle-Width="75px" ItemStyle-Wrap="false" />				            
				            <asp:BoundField DataField="ContactFirstName" HeaderText="First Name" HeaderStyle-Width="200px" SortExpression="ContactFirstName" />
				            <asp:BoundField DataField="ContactMiddleName" HeaderText="Middle" HeaderStyle-Width="75px" />
                            <asp:BoundField DataField="ContactLastName" HeaderText="Last name" HeaderStyle-Width="200px" SortExpression="ContactLastName" />
				            <asp:BoundField DataField="ContactPhoneNumber" HeaderText="Phone" HeaderStyle-Width="75px" ItemStyle-Wrap="false" />
				            <asp:BoundField DataField="BadgeNumber" HeaderText="Badge#" HeaderStyle-Width="75px" ItemStyle-Wrap="false" SortExpression="BadgeNumber" />
				            <asp:BoundField DataField="Activated" HeaderText="Activated" HeaderStyle-Width="100px" ItemStyle-Wrap="false" HtmlEncode="false" DataFormatString="{0:MM/dd/yyyy}" SortExpression="Activated" Visible="true" />
				            <asp:BoundField DataField="ActivatedBy" HeaderText="ActivatedBy" HeaderStyle-Width="75px" ItemStyle-Wrap="false" SortExpression="ActivatedBy" />
				            <asp:BoundField DataField="Inactivated" HeaderText="Inactivated" HeaderStyle-Width="100px" ItemStyle-Wrap="false" HtmlEncode="false" DataFormatString="{0:MM/dd/yyyy}" SortExpression="Inactivated" Visible="true" />
				            <asp:BoundField DataField="InactivatedBy" HeaderText="InactivatedBy" HeaderStyle-Width="75px" ItemStyle-Wrap="false" />
				            <asp:BoundField DataField="InactiveReason" HeaderText="Inactive Reason" HeaderStyle-Width="200px" />
                       </Columns>
                    </asp:GridView>
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="services">
                <asp:UpdatePanel ID="upnlServices" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>
                    <asp:Button ID="btnRegister" runat="server" Text="Register" CommandName="Register" OnCommand="OnManageCommand" />
                    <asp:Button ID="btnReplace" runat="server" Text="Replace" CommandName="Replace" OnCommand="OnManageCommand" />
                    <asp:Button ID="btnRevoke" runat="server" Text="Revoke" CommandName="Revoke" OnCommand="OnManageCommand" />
                    <asp:Button ID="btnChangeVehicle" runat="server" Text="Change Vehicle" CommandName="ChangeVehicle" OnCommand="OnManageCommand" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="grdPermits" EventName="SelectedIndexChanged" />
                </Triggers>
                </asp:UpdatePanel>
            </div>
            <asp:ObjectDataSource ID="odsPermits" runat="server" TypeName="Argix.HR.Permits.PermitGateway" SelectMethod="ViewPermits" />
        </div>
        <div id="tabSearch">
            <div class="permits">
                <label for="dduSearchBy">By</label><asp:DropDownList ID="dduSearchBy" runat="server" Width="200px" AutoPostBack="true" OnSelectedIndexChanged="OnSearchByChanged"><asp:ListItem Text="Permit#" /><asp:ListItem Text="License Plate#" /><asp:ListItem Text="Vehicle Description" /></asp:DropDownList>
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
                                    <label for="txtYear">Year</label><asp:TextBox ID="txtYear" runat="server" MaxLength="4" Width="75px" />
                                    <label for="txtMake">Make</label><asp:TextBox ID="txtMake" runat="server" MaxLength="20" Width="200px" />
                                    <label for="txtModel">Model</label><asp:TextBox ID="txtModel" runat="server" MaxLength="20" Width="200px" />
                                    <label for="txtColor">Color</label><asp:TextBox ID="txtColor" runat="server" MaxLength="20" Width="200px" /><br />
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
                <div class="gridviewtitle">Found Permits</div>
                <div class="gridviewbody">
                    <asp:UpdatePanel ID="upnlSearch" runat="server" UpdateMode="Conditional" >
                    <ContentTemplate>
                        <asp:GridView ID="grdSearch" runat="server" width="100%" AutoGenerateColumns="False" DataKeyNames="ID" AllowSorting="true">
                            <Columns>
                                <asp:CommandField ButtonType="Image" HeaderStyle-Width="16px" SelectImageUrl="~/App_Themes/Argix/Images/select.gif" ShowSelectButton="True" />
				                <asp:BoundField DataField="ID" HeaderText="ID#" HeaderStyle-Width="75px" Visible="false" />
                                <asp:TemplateField HeaderText="Number" ItemStyle-Width="75px" ItemStyle-Wrap="false" SortExpression="Number">
                                    <ItemTemplate>
                                        <a class="popupLink" href="" onclick="javascript:var w=window.open('<%# "Permit.aspx?id=" + Eval("ID").ToString() %>','_blank','width=800px,height=500px,resizable=no,scrollbars=yes,titlebar=no,menubar=no,toolbar=yes,status=no');return false;" title="Click here to view this permit.">&nbsp;<%# Eval("Number").ToString() %></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
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
        </div>
    </div>
    <asp:ObjectDataSource ID="odsStates" runat="server" TypeName="Argix.HR.Permits.PermitGateway" SelectMethod="GetStateList" />
</asp:Content>
