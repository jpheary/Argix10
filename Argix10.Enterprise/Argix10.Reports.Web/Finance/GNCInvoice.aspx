<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="GNCInvoice.aspx.cs" Inherits="GNCInvoice" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cHead" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
         .subtitle {
            margin: 0px 0px 10px 0px;
            padding: 0px 0px 0px 5px;
	        color: #004276;
            font-size: 18px;
            font-weight: 600;
            text-align: left;
        }
        .setup {
            width:100%;
            margin: 0px auto;
        }
        .setup p {
            margin: 10px 30px;
        }
        .gridviewtitle {
            height:18px;
            margin: 3px 0px;
            padding: 2px 10px;
            background-color: Highlight;
            font-size: 1.0em;
            font-weight: bold;
        }
        .gridviewbody {
            height: 450px;
            margin:0px;
            padding:0px;
            border-bottom: 1px solid #c5c7c9;
            overflow-x:auto;
            overflow-y:auto;
            white-space:nowrap;
        }   
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Setup" Runat="Server">
    <div class="setup">
        <div class="subtitle">Setup</div>
        <p>&nbsp;</p>
        <div class="gridviewtitle">GNC Invoices</div>
        <div class="gridviewbody">
            <asp:GridView ID="grdInvoices" runat="server" Width="100%" AutoGenerateColumns="False" AllowSorting="True" OnSelectedIndexChanged="OnInvoiceSelected">
                <Columns>
                    <asp:TemplateField HeaderText="" HeaderStyle-Width="25px" >
                        <ItemTemplate><asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="OnInvoiceSelected"/></ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="InvoiceNumber" HeaderText="Invoice#" HeaderStyle-Width="75px" SortExpression="InvoiceNumber" />
                    <asp:BoundField DataField="InvoiceDate" HeaderText="Invoiced" HeaderStyle-Width="75px" SortExpression="InvoiceDate" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" />
                    <asp:BoundField DataField="PostToARDate" HeaderText="AR Posted" HeaderStyle-Width="75px" SortExpression="PostToARDate" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" />
                    <asp:BoundField DataField="Cartons" HeaderText="Cartons" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Right" SortExpression="Cartons" />
                    <asp:BoundField DataField="Pallets" HeaderText="Pallets" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Right" SortExpression="Pallets" />
                    <asp:BoundField DataField="Weight" HeaderText="Weight" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Right" SortExpression="Weight" />
                    <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Right" SortExpression="Amount" />
                    <asp:BoundField DataField="ReleaseDate" HeaderText="ReleaseDate" HeaderStyle-Width="75px" SortExpression="ReleaseDate" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" />
                    <asp:BoundField DataField="InvoiceTypeCode" HeaderText="InvoiceTypeCode" HeaderStyle-Width="50px" Visible="false" />
                    <asp:BoundField DataField="InvoiceTypeDescription" HeaderText="InvoiceTypeDescription" HeaderStyle-Width="50px" Visible="false" />
                    <asp:BoundField DataField="BillTo" HeaderText="BillTo" HeaderStyle-Width="150px" SortExpression="BillTo" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>

