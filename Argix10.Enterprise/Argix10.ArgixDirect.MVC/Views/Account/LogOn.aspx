<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Argix.Models.LogOnModel>" %>

<asp:Content ID="cLeft" runat="server" ContentPlaceHolderID="LeftContent">
    <div id="title">Client Login</div>
    <div id="submenu">
		<ul>
			<li><%: Html.ActionLink("Register","Register","Account",new { area="" },null)%></li>
		</ul>
	</div>
</asp:Content>
<asp:Content ID="cRight" runat="server" ContentPlaceHolderID="RightContent">
    <div  class="textContainer"> 
        <h2>Client Login</h2>
        <p>Please enter your username and password. <%: Html.ActionLink("Register", "Register") %> if you don't have an account.</p>
        <% using(Html.BeginForm()) { %>
            <div>
                <fieldset>
                    <legend>Account Information</legend>
                    <%: Html.ValidationSummary(true, "Login was unsuccessful. Please correct the errors and try again.") %>
                    <div class="editor-label"><%: Html.LabelFor(m => m.UserName) %></div>
                    <div class="editor-field"><%: Html.TextBoxFor(m => m.UserName) %><%: Html.ValidationMessageFor(m => m.UserName) %></div>
                    <div class="editor-label"><%: Html.LabelFor(m => m.Password) %></div>
                    <div class="editor-field"><%: Html.PasswordFor(m => m.Password) %><%: Html.ValidationMessageFor(m => m.Password) %></div>
                    <div class="editor-label"><%: Html.CheckBoxFor(m => m.RememberMe) %><%: Html.LabelFor(m => m.RememberMe) %></div>
                    <br />
                    <p><input type="submit" value="Log On" /></p>
                </fieldset>
            </div>
        <% } %>
    </div>
</asp:Content>
