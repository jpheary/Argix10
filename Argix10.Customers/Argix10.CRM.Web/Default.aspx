<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/Default.master" %>

<asp:Content ID="cHead" runat="server" ContentPlaceHolderID="cpHead">
</asp:Content>
<asp:Content ID="cBody" runat="server" ContentPlaceHolderID="cpBody">
<asp:UpdatePanel ID="upnlTimer" runat="server" UpdateMode="Always">
    <ContentTemplate>
        <asp:Timer ID="tmrRefresh" runat="server" Interval="30000" Enabled="false" OnTick="OnIssueTimerTick"></asp:Timer>
    </ContentTemplate>
</asp:UpdatePanel>
<asp:UpdatePanel ID="upnlIssues" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
    <div id="toppane">
    <div id="issuestoolbar">
        <table>
            <tr style="height:25px; width:100%">
                <td>Terminal&nbsp;
                    <asp:DropDownList ID="cboTerminal" runat="server" Width="150px" DataSourceID="odsTerminals" DatatextField="Description" DataValueField="AgentID" ToolTip="Local Terminals" AutoPostBack="True" OnSelectedIndexChanged="OnTerminalChanged"></asp:DropDownList>
                    <asp:ObjectDataSource ID="odsTerminals" runat="server" SelectMethod="GetTerminals" TypeName="Argix.Customers.CustomersGateway" CacheExpirationPolicy="Absolute" CacheDuration="900" EnableCaching="true" >
                        <SelectParameters>
                            <asp:SessionParameter Name="agentNumber" SessionField="agentNumber" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
                <td style="width:5px">&nbsp;|&nbsp;</td>
                <td style="width:200px">
                    View&nbsp;
                    <asp:DropDownList ID="cboView" runat="server" Width="150px" ToolTip="Issue views" AutoPostBack="True" OnSelectedIndexChanged="OnViewChanged">
                        <asp:ListItem Text="Current Issues" Value="Current" Selected="True" />
                        <asp:ListItem Text="Search Issues" Value="Search" />
                    </asp:DropDownList>
                </td>
                <td style="width:5px">&nbsp;|&nbsp;</td>
                <td style="width:25px"><asp:ImageButton ID="btnIssuesNew" runat="server" ImageUrl="~/App_Themes/Argix/Images/file.gif" ToolTip="Create a new issue..." CommandName="New" OnCommand="OnIssueToolbarClick" /></td>
                <td style="width:5px">&nbsp;|&nbsp;</td>
                <td style="width:25px"><asp:ImageButton ID="btnIssuesPrint" runat="server" ImageUrl="~/App_Themes/Argix/Images/print.gif" ToolTip="Print issue view" /></td>
                <td style="width:25px"><asp:ImageButton ID="btnIssuesRefresh" runat="server" ImageUrl="~/App_Themes/Argix/Images/refresh.gif" ToolTip="Refresh issue view" CommandName="Refresh" OnCommand="OnIssueToolbarClick" /></td>
                <td style="width:5px">&nbsp;|&nbsp;</td>
                <td style="width:250px">
                    Find
                    &nbsp;<asp:TextBox ID="txtSearch" Width="144px" ToolTip="Search issue text" runat="server" />
                    &nbsp;<asp:ImageButton ID="btnIssuesSearch" ImageAlign="Middle" runat="server" ImageUrl="~/App_Themes/Argix/Images/findreplace.gif" ToolTip="Search issue text" CommandName="Search" OnCommand="OnIssueToolbarClick" />
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
    <div id="issuestitle">Issues</div>
    <div id="issuesview">
        <asp:GridView ID="grdIssues" runat="server" Width="100%" BackColor="Window" AutoGenerateColumns="False" DataSourceID="odsIssues" DataKeyNames="ID"  AllowSorting="true" OnRowDataBound="OnIssueRowDataBound" OnSelectedIndexChanged="OnIssueSelected" OnSorting="OnIssuesSorting" OnSorted="OnIssuesSorted" >
            <Columns>
                <asp:CommandField ButtonType="Image" HeaderStyle-Width="16px" SelectImageUrl="~/App_Themes/Argix/Images/select.gif" ShowSelectButton="True" />
                <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-Width="0px" ItemStyle-Width="0px" Visible="True" />
                <asp:BoundField DataField="Zone" HeaderText="Zone" HeaderStyle-Width="72px" ItemStyle-Wrap="false" SortExpression="Zone" />
                <asp:BoundField DataField="StoreNumber" HeaderText="Store" HeaderStyle-Width="72px" ItemStyle-Wrap="false" SortExpression="StoreNumber" />
                <asp:BoundField DataField="AgentNumber" HeaderText="Agent" HeaderStyle-Width="72px" ItemStyle-Wrap="false" SortExpression="AgentNumber" />
                <asp:BoundField DataField="CompanyName" HeaderText="Company" HeaderStyle-Width="144px" ItemStyle-Wrap="false" SortExpression="Company" />
                <asp:BoundField DataField="Type" HeaderText="Type" HeaderStyle-Width="72px" ItemStyle-Wrap="false" SortExpression="Type" />
                <asp:BoundField DataField="LastActionDescription" HeaderText="Action" HeaderStyle-Width="72px" ItemStyle-Wrap="false" SortExpression="LastActionDescription" />
                <asp:BoundField DataField="LastActionCreated" HeaderText="Received" HeaderStyle-Width="144px" ItemStyle-Wrap="false" SortExpression="LastActionCreated" HtmlEncode="true" DataFormatString="{0}" />
                <asp:BoundField DataField="Subject" HeaderText="Subject" HeaderStyle-Width="240px" ItemStyle-Width="240px" ItemStyle-Wrap="true" SortExpression="Subject" />
                <asp:BoundField DataField="Contact" HeaderText="Contact" HeaderStyle-Width="144px" ItemStyle-Wrap="false" SortExpression="ContactName" />
                <asp:BoundField DataField="LastActionUserID" HeaderText="Last User" HeaderStyle-Width="96px" ItemStyle-Wrap="false" SortExpression="LastActionUserID" />
                <asp:BoundField DataField="FirstActionUserID" HeaderText="Originator" HeaderStyle-Width="96px" ItemStyle-Wrap="false" SortExpression="FirstActionUserID" ></asp:BoundField>
                <asp:BoundField DataField="Coordinator" HeaderText="Coordinator" HeaderStyle-Width="96px" ItemStyle-Wrap="false" SortExpression="Coordinator" ></asp:BoundField>
                <asp:BoundField DataField="ClientRep" HeaderText="ClientRep" HeaderStyle-Width="96px" ItemStyle-Wrap="false" SortExpression="ClientRep" ></asp:BoundField>
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="odsIssues" runat="server" TypeName="Argix.Customers.CustomersGateway" SelectMethod="ViewIssues">
            <SelectParameters>
                <asp:ControlParameter Name="agentNumber" ControlID="cboTerminal" PropertyName="SelectedValue" DefaultValue="" ConvertEmptyStringToNull="true" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsSearch" runat="server" TypeName="Argix.Customers.CustomersGateway" SelectMethod="SearchIssues">
            <SelectParameters>
                <asp:ControlParameter Name="agentNumber" ControlID="cboTerminal" PropertyName="SelectedValue" DefaultValue="" ConvertEmptyStringToNull="true" />
                <asp:ControlParameter Name="searchText" ControlID="txtSearch" PropertyName="Text" ConvertEmptyStringToNull="true" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    </div>
    <div id="bottompane">
    <table style="width:100%">
        <tr>
            <td style="width:335px; vertical-align:top">
                <div id="actionstitle"><asp:Label ID="lblActions" runat="server" Text="Actions" /></div>
                <div id="actionstoolbar">
                    <table>
                        <tr style="height:25px; width:100%">
                            <td style="width:25px"><asp:ImageButton ID="btnActionsNew" runat="server" ImageUrl="~/App_Themes/Argix/Images/file.gif" ToolTip="Add a new action..." CommandName="New" OnCommand="OnActionToolbarClick" /></td>
                            <td style="width:5px">&nbsp;|&nbsp;</td>
                            <td style="width:25px"><asp:ImageButton ID="btnActionsPrint" runat="server" ImageUrl="~/App_Themes/Argix/Images/print.gif" ToolTip="Print actions" /></td>
                            <td style="width:25px"><asp:ImageButton ID="btnActionsRefresh" runat="server" ImageUrl="~/App_Themes/Argix/Images/refresh.gif" ToolTip="Refresh action view" CommandName="Refresh" OnCommand="OnActionToolbarClick" /></td>
                            <td style="width:5px">&nbsp;|&nbsp;</td>
                            <td style="width:25px"><asp:ImageButton ID="btnAttachmentNew" runat="server" ImageUrl="~/App_Themes/Argix/Images/attach.gif" ToolTip="Add a new attachment to the selected action..." CommandName="NewAttachment" OnCommand="OnActionToolbarClick" /></td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </div>
                <div id="actionsview">
                    <asp:ListView ID="lsvActions" runat="server" DataSourceID="odsActions" DataKeyNames="ID, IssueID" OnSelectedIndexChanged="OnActonSelected">
                        <LayoutTemplate>
                            <table id="Table1" runat="server" border="0" cellpadding="1" cellspacing="1">
                                <tr id="Tr1" runat="server" style="background-color:ButtonFace;">
                                    <td id="Td1" runat="server" style="width:24px; border:inset 1px ButtonShadow;">&nbsp;</td>
                                    <td id="Td2" runat="server" style="width:24px; border:inset 1px ButtonShadow;">&nbsp;</td>
                                    <td id="Td3" runat="server" style="width:120px; border:inset 1px ButtonShadow;">&nbsp;Created</td>
                                    <td id="Td4" runat="server" style="width:120px; border:inset 1px ButtonShadow;">&nbsp;User</td>
                                </tr>
                                <tr id="itemPlaceholder" runat="server" />
                                <tr><td>&nbsp;</td></tr>
                            </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <tr id="Tr2" runat="server" style="background-color:white">
                                <td id="Td5" runat="server" valign="top"><asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/App_Themes/Argix/Images/select.gif" CommandName="Select" /></td>
                                <td id="Td6" runat="server" valign="top"><asp:Image ID="imgAttachments" runat="server" ImageUrl="~/App_Themes/Argix/Images/attachment.gif" Visible='<%# (Convert.ToInt32(Eval("AttachmentCount")) > 0 ? true : false) %>' /></td>
                                <td id="Td7" runat="server" valign="top"><asp:Label ID="lblCreated" runat="server" Text='<%# Eval("Created") %>' Width="144px" /></td>
                                <td id="Td8" runat="server" valign="top"><asp:Label ID="lblUser" runat="server" Text='<%# Eval("UserID") %>' Width="120px" /></td>
                            </tr>
                        </ItemTemplate>
                        <SelectedItemTemplate>
                            <tr id="Tr3" runat="server" style="color:HighlightText; background-color:Highlight">
                                <td id="Td9" runat="server" valign="top"><asp:ImageButton ID="ibSelect" runat="server" ImageUrl="~/App_Themes/Argix/Images/select.gif" CommandName="Select" /></td>
                                <td id="Td10" runat="server" valign="top"><asp:Image ID="imgAttachments" runat="server" ImageUrl="~/App_Themes/Argix/Images/attachment.gif" Visible='<%# (Convert.ToInt32(Eval("AttachmentCount")) > 0 ? true : false) %>' /></td>
                                <td id="Td11" runat="server" valign="top"><asp:Label ID="lblCreated" runat="server" Text='<%# Eval("Created") %>' Width="144px" /></td>
                                <td id="Td12" runat="server" valign="top"><asp:Label ID="lblUser" runat="server" Text='<%# Eval("UserID") %>' Width="120px" /></td>
                            </tr>
                        </SelectedItemTemplate>
                        <EmptyDataTemplate>
                            <table id="Table2" runat="server" border="0" cellpadding="1" cellspacing="1">
                                <tr id="Tr4" runat="server" style="background-color:ButtonFace;">
                                    <td id="Td13" runat="server" style="width:24px; border:inset 1px ButtonShadow;">&nbsp;</td>
                                    <td id="Td14" runat="server" style="width:24px; border:inset 1px ButtonShadow;">&nbsp;</td>
                                    <td id="Td15" runat="server" style="width:120px; border:inset 1px ButtonShadow;">&nbsp;Created</td>
                                    <td id="Td16" runat="server" style="width:120px; border:inset 1px ButtonShadow;">&nbsp;User</td>
                                </tr>
                                <tr id="Tr5" runat="server" style="background-color:white; height:192px"><td id="Td17" runat="server" style="width:24px;">&nbsp;</td><td id="Td18" runat="server" style="width:24px;">&nbsp;</td><td id="Td19" runat="server" style="width:120px;">&nbsp;</td><td id="Td20" runat="server" style="width:120px;">&nbsp;</td></tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                    <asp:ObjectDataSource ID="odsActions" runat="server" SelectMethod="GetIssueActions" TypeName="Argix.Customers.CustomersGateway">
                        <SelectParameters>
                            <asp:Parameter Name="issueID" DefaultValue="" ConvertEmptyStringToNull="true" DbType="Int64" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
            </td>
            <td style="vertical-align:top">
                <div id="actiontitle"><asp:Label ID="lblSubject" runat="server" Text="[Subject]" /></div>
                <div id="attachments">
                    <div id="attachmentstitle"><asp:Image ID="imgAttachments" runat="server" ImageUrl="~/App_Themes/Argix/Images/attachments.gif" ToolTip="Attachments" BorderStyle="None" /></div>
                    <div id="attachmentsview">
                        <asp:ListView ID="lsvAttachments" runat="server" DataSourceID="odsAttachments" DataKeyNames="Filename">
                            <LayoutTemplate>
                                <table id="Table3" runat="server" border="0" cellpadding="8" cellspacing="6" >
                                    <tr ID="itemPlaceholderContainer" runat="server">
                                        <td ID="itemPlaceholder" runat="server"></td>
                                    </tr>
                                </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                <td id="Td21" runat="server" style="border: 0px none ButtonFace">
                                    <asp:HyperLink ID="lnkFilename" runat="server" Target="_blank" NavigateUrl='<%# "~/Attachment.aspx?id=" + Eval("ID") + "&name=" + HttpUtility.UrlEncode(Eval("Filename").ToString()) %>'><%# Eval("Filename") %></asp:HyperLink>
                                </td>
                            </ItemTemplate>
                            <EmptyDataTemplate>
                                <table id="Table4" runat="server" border="0" cellpadding="8" cellspacing="6">
                                    <tr id="Tr6" runat="server"><td id="Td22" runat="server">&nbsp;</td></tr>
                                </table>
                            </EmptyDataTemplate>
                        </asp:ListView>
                        <asp:ObjectDataSource ID="odsAttachments" runat="server" SelectMethod="GetAttachments" TypeName="Argix.Customers.CustomersGateway">
                            <SelectParameters>
                                <asp:Parameter Name="issueID" DefaultValue="" ConvertEmptyStringToNull="true" DbType="Int64" />
                                <asp:Parameter Name="actionID" DefaultValue="" ConvertEmptyStringToNull="true" DbType="Int64" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </div>
                </div>
                <div class="clear"></div>
                <div id="actionview">
                    <asp:ListView ID="lsvAction" runat="server"  DataSourceID="odsActionDetail">
                        <LayoutTemplate>
                            <div id="itemPlaceholder" style="width:100%" runat="server" ></div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <table id="Table5" runat="server" style="width:100%; background-color:#ffffff">
                                <tr id="Tr7" runat="server"><td style="font-weight:bold"><%# Eval("Created") %>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%# Eval("UserID") %>,&nbsp;&nbsp;<%# Eval("TypeName") %></td></tr>
                                <tr id="Tr8" runat="server"><td>&nbsp;</td></tr>
                                <tr id="Tr9" runat="server"><td style="white-space:normal"><%# Eval("Comment") %></td></tr>
                                <tr id="Tr10" runat="server"><td><hr /></td></tr>
                            </table>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table id="Table6" runat="server" style="width:100%; background-color:#ffffff">
                                <tr id="Tr11" runat="server" style="background-color:white; height:192px"><td>&nbsp;</td></tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                    <asp:ObjectDataSource ID="odsActionDetail" runat="server" SelectMethod="GetIssueActions" TypeName="Argix.Customers.CustomersGateway">
                        <SelectParameters>
                            <asp:Parameter Name="issueID" DefaultValue="" ConvertEmptyStringToNull="true" DbType="Int64" />
                            <asp:Parameter Name="actionID" DefaultValue="" ConvertEmptyStringToNull="true" DbType="Int64" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
            </td>
        </tr>
    </table>
    </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="tmrRefresh" EventName="Tick" />
    </Triggers>
</asp:UpdatePanel>
</asp:Content>
