<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="cLeft" runat="server" ContentPlaceHolderID="LeftContent">
    <div id="title">Home</div>
    <div id="submenu">
		<ul>
			<li><a runat="server" href="~/Testimonials.aspx">Testimonials</a></li>
		</ul>
	</div>
</asp:Content>
<asp:Content ID="cRight" runat="server" ContentPlaceHolderID="RightContent">
    <img runat="server" src="~/styles/images/services.gif" alt="Argix Services" />
</asp:Content>
