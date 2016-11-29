<%@ Page Title="Loghips" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="ManageLog.aspx.cs" Inherits="ManageLog" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cBody" runat="server" ContentPlaceHolderID="cpBody">
    <asp:LinkButton id="lnkProfile" CausesValidation="False" runat="server" PostBackUrl="~/Members/Profile.aspx">Profile...</asp:LinkButton>&nbsp;&nbsp;&nbsp;MANAGE ERROR LOG
    <br /><br />
    <div class="form">
    <table width="880px" border="0px" cellpadding="0px" cellspacing="0px">
        <tr style="font-size:1px; height:6px"><td colspan="2">&nbsp;</td></tr>
        <tr>
            <td colspan="2" valign="top">
                <asp:Panel id="pnlLog" runat="server" Width="880px" Height="332px" BorderStyle="None" ScrollBars="Auto">
                    <asp:UpdatePanel ID="upnlLog" runat="server" UpdateMode="Conditional" >
                    <ContentTemplate>
                        <asp:GridView ID="grdLog" runat="server" Width="880px" Height="100%" DataSourceID="odsLog" DataKeyNames="ID" AutoGenerateColumns="False" AllowSorting="True" >
                            <Columns>
                                <asp:CommandField HeaderStyle-Width="16px" ButtonType="Image" ShowSelectButton="True" SelectImageUrl="~/App_Themes/Argix/Images/select.gif" />
                                <asp:BoundField DataField="ID" HeaderText="ID" HeaderStyle-Width="50px" ItemStyle-Wrap="false" Visible="false"  />
                                <asp:BoundField DataField="Name" HeaderText="Name" HeaderStyle-Width="100px" ItemStyle-Wrap="false" Visible="false"  />
                                <asp:BoundField DataField="Level" HeaderText="Level" HeaderStyle-Width="50px" ItemStyle-Wrap="false" Visible="false"  />
                                <asp:BoundField DataField="Date" HeaderText="Date" HeaderStyle-Width="125px" DataFormatString="{0:MM-dd-yyyy hh:mm tt}" HtmlEncode="False" />
                                <asp:BoundField DataField="Source" HeaderText="Source" HeaderStyle-Width="75px" ItemStyle-Wrap="false" Visible="false" />
                                <asp:BoundField DataField="User" HeaderText="User" HeaderStyle-Width="75px" ItemStyle-Wrap="false" SortExpression="User ASC" />
                                <asp:BoundField DataField="Message" HeaderText="Message" />
                            </Columns>
                        </asp:GridView>
                        <asp:ObjectDataSource ID="odsLog" runat="server" TypeName="EnterpriseGateway" SelectMethod="LogEntriesRead" />
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:UpdateProgress ID="upgLog" runat="server" AssociatedUpdatePanelID="upnlLog"><ProgressTemplate>updating...</ProgressTemplate></asp:UpdateProgress>
                </asp:Panel>
            </td>
        </tr>
        <tr style="font-size:1px; height:12px"><td colspan="2">&nbsp;</td></tr>
    </table>
    </div>
</asp:Content>
