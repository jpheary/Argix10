<%@ Page Language="C#" masterpagefile="~/Default.master" AutoEventWireup="true" CodeFile="Badges.aspx.cs" Inherits="_Badges" %>
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
            $("#<%=btnEmployeeNew.ClientID %>").button();
            $("#<%=btnEmployeeUpdate.ClientID %>").button();
            $("#<%=btnVendorNew.ClientID %>").button();
            $("#<%=btnVendorUpdate.ClientID %>").button();
        }
        function scroll(gridname, panelname, number) {
            var grd = document.getElementById(gridname);
            for (var i = 1; i < grd.rows.length; i++) {
                var cell = grd.rows[i].cells[2];
                if (cell.innerHTML.substr(0, number.length).toLowerCase() == number) {
                    var pnl = document.getElementById(panelname);
                    pnl.scrollTop = i * (grd.clientHeight / grd.rows.length);
                    break;
                }
            }
        }
    </script>
    <div class="subtitle">ID Badges</div>
    <div id="tabs">
        <ul>
            <li><a href="#tabDrivers">Drivers</a></li>
            <li><a href="#tabEmployees">Employees</a></li>
            <li><a href="#tabVendors">Vendors</a></li>
        </ul>
        <div id="tabDrivers">
            <div class="gridviewtitle">
                <div class="gridviewtitlelabel">Driver Badges</div>
                <div class="gridviewtitlesearch">
                    <asp:UpdatePanel runat="server" ID="upnlDriversHeader" UpdateMode="Always" >
                    <ContentTemplate>
                        <asp:TextBox ID="txtFindDriver" runat="server" Width="75px" BorderStyle="Inset" BorderWidth="1px" ToolTip="Enter a last name... <press Enter>" AutoPostBack="True" OnTextChanged="OnSearchDrivers"></asp:TextBox>
                        <asp:ImageButton ID="imgFindDriver" runat="server" Height="16px" ImageAlign="Middle" ImageUrl="~/App_Themes/Argix/Images/search.gif" ToolTip="Search for a driver..." OnClick="OnFindDriver" />&nbsp;
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div id="idDrivers" class="gridviewbody">
                <asp:UpdatePanel ID="upnlDrivers" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>
                    <asp:GridView ID="grdDriverBadges" runat="server" width="100%" AutoGenerateColumns="False" DataSourceID="odsDriverBadges" DataKeyNames="IDNumber" AllowSorting="true" OnSelectedIndexChanged="OnBadgeSelected">
                        <Columns>
                            <asp:CommandField ButtonType="Image" HeaderStyle-Width="16px" SelectImageUrl="~/App_Themes/Argix/Images/select.gif" ShowSelectButton="True" />
				            <asp:BoundField DataField="IDNumber" HeaderText="ID#" HeaderStyle-Width="75px" Visible="false" />
				            <asp:BoundField DataField="BadgeNumber" HeaderText="Badge#" HeaderStyle-Width="75px" SortExpression="BadgeNumber" />
				            <asp:BoundField DataField="LastName" HeaderText="Last" HeaderStyle-Width="200px" SortExpression="LastName" />
				            <asp:BoundField DataField="FirstName" HeaderText="First" HeaderStyle-Width="150px" SortExpression="FirstName" />
				            <asp:BoundField DataField="Middle" HeaderText="Mid" HeaderStyle-Width="100px" />
				            <asp:BoundField DataField="Suffix" HeaderText="Suffix" HeaderStyle-Width="60px" />
				            <asp:BoundField DataField="Location" HeaderText="Location" HeaderStyle-Width="200px" SortExpression="Location" />
				            <asp:BoundField DataField="Department" HeaderText="Deptartment" HeaderStyle-Width="200px" SortExpression="Department" />
				            <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-Width="75px" SortExpression="Status" />
				            <asp:BoundField DataField="IssueDate" HeaderText="Issued" HeaderStyle-Width="125px" HtmlEncode="false" DataFormatString="{0:MM/dd/yyyy}" Visible="false" />
				            <asp:BoundField DataField="ExpirationDate" HeaderText="Expires" HeaderStyle-Width="125px" HtmlEncode="false" DataFormatString="{0:MM/dd/yyyy}" Visible="false" />
				            <asp:BoundField DataField="HireDate" HeaderText="Hired" HeaderStyle-Width="125px" HtmlEncode="false" DataFormatString="{0:MM/dd/yyyy}" Visible="false" />
				            <asp:BoundField DataField="HasPhoto" HeaderText="Pic?" HeaderStyle-Width="50px" Visible="false" />
				            <asp:BoundField DataField="HasSignature" HeaderText="Sig?" HeaderStyle-Width="50px" Visible="false" />
                        </Columns>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="odsDriverBadges" runat="server" TypeName="Argix.HR.BadgeGateway" SelectMethod="ViewDriverBadges" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txtFindDriver" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="imgFindDriver" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="services">
                <asp:UpdatePanel ID="upnlDriverServices" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>
                    &nbsp;
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="tabEmployees">
            <div class="gridviewtitle">
                <div class="gridviewtitlelabel">Employee Badges</div>
                <div class="gridviewtitlesearch">
                    <asp:UpdatePanel runat="server" ID="upnlEmployeesHeader" UpdateMode="Always" >
                    <ContentTemplate>
                        <asp:TextBox ID="txtFindEmployee" runat="server" Width="75px" BorderStyle="Inset" BorderWidth="1px" ToolTip="Enter a last name... <press Enter>" AutoPostBack="True" OnTextChanged="OnSearchEmployees"></asp:TextBox>
                        <asp:ImageButton ID="imgFindEmployee" runat="server" Height="16px" ImageAlign="Middle" ImageUrl="~/App_Themes/Argix/Images/search.gif" ToolTip="Search for an employee..." OnClick="OnFindEmployee" />&nbsp;
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div id="idEmployees" class="gridviewbody">
                <asp:UpdatePanel ID="upnlEmployees" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>
                    <asp:GridView ID="grdEmployeeBadges" runat="server" width="100%" AutoGenerateColumns="False" DataSourceID="odsEmployeeBadges" DataKeyNames="IDNumber" AllowSorting="true" OnSelectedIndexChanged="OnBadgeSelected">
                        <Columns>
                            <asp:CommandField ButtonType="Image" HeaderStyle-Width="16px" SelectImageUrl="~/App_Themes/Argix/Images/select.gif" ShowSelectButton="True" />
				            <asp:BoundField DataField="IDNumber" HeaderText="ID#" HeaderStyle-Width="75px" Visible="false" />
				            <asp:BoundField DataField="BadgeNumber" HeaderText="Badge#" HeaderStyle-Width="75px" SortExpression="BadgeNumber" />
				            <asp:BoundField DataField="LastName" HeaderText="Last" HeaderStyle-Width="200px" SortExpression="LastName" />
				            <asp:BoundField DataField="FirstName" HeaderText="First" HeaderStyle-Width="150px" SortExpression="FirstName" />
				            <asp:BoundField DataField="Middle" HeaderText="Mid" HeaderStyle-Width="100px" />
				            <asp:BoundField DataField="Suffix" HeaderText="Suffix" HeaderStyle-Width="60px" />
				            <asp:BoundField DataField="SSN" HeaderText="SSN" HeaderStyle-Width="75px" ItemStyle-Wrap="false" />
				            <asp:BoundField DataField="Location" HeaderText="Location" HeaderStyle-Width="200px" SortExpression="Location" />
				            <asp:BoundField DataField="SubLocation" HeaderText="Sub-Location" HeaderStyle-Width="200px" SortExpression="SubLocation" />
				            <asp:BoundField DataField="Department" HeaderText="Deptartment" HeaderStyle-Width="200px" SortExpression="Department" />
				            <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-Width="75px" SortExpression="Status" />
				            <asp:BoundField DataField="IssueDate" HeaderText="Issued" HeaderStyle-Width="125px" HtmlEncode="false" DataFormatString="{0:MM/dd/yyyy}" Visible="false" />
				            <asp:BoundField DataField="ExpirationDate" HeaderText="Expires" HeaderStyle-Width="125px" HtmlEncode="false" DataFormatString="{0:MM/dd/yyyy}" Visible="false" />
				            <asp:BoundField DataField="HireDate" HeaderText="Hired" HeaderStyle-Width="125px" HtmlEncode="false" DataFormatString="{0:MM/dd/yyyy}" SortExpression="HireDate" Visible="true" />
				            <asp:BoundField DataField="HasPhoto" HeaderText="Pic?" HeaderStyle-Width="50px" Visible="false" />
				            <asp:BoundField DataField="HasSignature" HeaderText="Sig?" HeaderStyle-Width="50px" Visible="false" />
                        </Columns>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="odsEmployeeBadges" runat="server" TypeName="Argix.HR.BadgeGateway" SelectMethod="ViewEmployeeBadges" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txtFindEmployee" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="imgFindEmployee" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="services">
                <asp:UpdatePanel ID="upnlEmployeeServices" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>
                    <asp:Button ID="btnEmployeeNew" runat="server" Text="  New  " CommandName="EmployeeNew" OnCommand="OnManageCommand" />
                    <asp:Button ID="btnEmployeeUpdate" runat="server" Text="Update" CommandName="EmployeeUpdate" OnCommand="OnManageCommand" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txtFindEmployee" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="imgFindEmployee" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="grdEmployeeBadges" EventName="SelectedIndexChanged" />
                </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="tabVendors">
            <div class="gridviewtitle">
                <div class="gridviewtitlelabel">Vendor Badges</div>
                <div class="gridviewtitlesearch">
                    <asp:UpdatePanel runat="server" ID="upnlVendorsHeader" UpdateMode="Always" >
                    <ContentTemplate>
                        <asp:TextBox ID="txtFindVendor" runat="server" Width="75px" BorderStyle="Inset" BorderWidth="1px" ToolTip="Enter a last name... <press Enter>" AutoPostBack="True" OnTextChanged="OnSearchVendors"></asp:TextBox>
                        <asp:ImageButton ID="imgFindVendor" runat="server" Height="16px" ImageAlign="Middle" ImageUrl="~/App_Themes/Argix/Images/search.gif" ToolTip="Search for a vendor..." OnClick="OnFindVendor" />&nbsp;
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div id="idVendors" class="gridviewbody">
                <asp:UpdatePanel ID="upnlVendors" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>
                    <asp:GridView ID="grdVendorBadges" runat="server" width="100%" AutoGenerateColumns="False" DataSourceID="odsVendorBadges" DataKeyNames="IDNumber" AllowSorting="true" OnSelectedIndexChanged="OnBadgeSelected">
                        <Columns>
                            <asp:CommandField ButtonType="Image" HeaderStyle-Width="16px" SelectImageUrl="~/App_Themes/Argix/Images/select.gif" ShowSelectButton="True" />
				            <asp:BoundField DataField="IDNumber" HeaderText="ID#" HeaderStyle-Width="75px" Visible="false" />
				            <asp:BoundField DataField="BadgeNumber" HeaderText="Badge#" HeaderStyle-Width="75px" SortExpression="BadgeNumber" />
				            <asp:BoundField DataField="LastName" HeaderText="Last" HeaderStyle-Width="200px" SortExpression="LastName" />
				            <asp:BoundField DataField="FirstName" HeaderText="First" HeaderStyle-Width="150px" SortExpression="FirstName" />
				            <asp:BoundField DataField="Middle" HeaderText="Mid" HeaderStyle-Width="100px" />
				            <asp:BoundField DataField="Suffix" HeaderText="Suffix" HeaderStyle-Width="60px" />
				            <asp:BoundField DataField="Location" HeaderText="Location" HeaderStyle-Width="200px" SortExpression="Location" />
				            <asp:BoundField DataField="Department" HeaderText="Deptartment" HeaderStyle-Width="200px" SortExpression="Department" />
				            <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-Width="75px" SortExpression="Status" />
				            <asp:BoundField DataField="IssueDate" HeaderText="Issued" HeaderStyle-Width="125px" HtmlEncode="false" DataFormatString="{0:MM/dd/yyyy}" Visible="false" />
				            <asp:BoundField DataField="ExpirationDate" HeaderText="Expires" HeaderStyle-Width="125px" HtmlEncode="false" DataFormatString="{0:MM/dd/yyyy}" Visible="false" />
				            <asp:BoundField DataField="HireDate" HeaderText="Hired" HeaderStyle-Width="125px" HtmlEncode="false" DataFormatString="{0:MM/dd/yyyy}" Visible="false" />
				            <asp:BoundField DataField="HasPhoto" HeaderText="Pic?" HeaderStyle-Width="50px" Visible="false" />
				            <asp:BoundField DataField="HasSignature" HeaderText="Sig?" HeaderStyle-Width="50px" Visible="false" />
                        </Columns>
                    </asp:GridView>
                    <asp:ObjectDataSource ID="odsVendorBadges" runat="server" TypeName="Argix.HR.BadgeGateway" SelectMethod="ViewVendorBadges" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txtFindVendor" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="imgFindVendor" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="services">
                <asp:UpdatePanel ID="upnlVendorServices" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>
                    <asp:Button ID="btnVendorNew" runat="server" Text="  New  " CommandName="VendorNew" OnCommand="OnManageCommand" />
                    <asp:Button ID="btnVendorUpdate" runat="server" Text="Update" CommandName="VendorUpdate" OnCommand="OnManageCommand" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txtFindVendor" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="imgFindVendor" EventName="Click" />
                    <asp:AsyncPostBackTrigger ControlID="grdVendorBadges" EventName="SelectedIndexChanged" />
                </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
