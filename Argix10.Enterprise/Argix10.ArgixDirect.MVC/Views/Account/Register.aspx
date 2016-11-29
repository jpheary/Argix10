<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Argix.Models.RegisterModel>" %>

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
        <h2>Register</h2>
        <h3>Create a New Account</h3>
        <p>Use the form below to create a new account. Passwords are required to be a minimum of <%: ViewData["PasswordLength"] %> characters in length.</p>
        <% using(Html.BeginForm()) { %>
            <div>
                <fieldset>
                    <legend>Account Information</legend>
                    <%: Html.ValidationSummary(true, "Account creation was unsuccessful. Please correct the errors and try again.") %>
                    <div class="editor-label"><%: Html.LabelFor(m => m.UserName) %></div>
                    <div class="editor-field"><%: Html.TextBoxFor(m => m.UserName) %><%: Html.ValidationMessageFor(m => m.UserName) %></div>
                    <div class="editor-label"><%: Html.LabelFor(m => m.Email) %></div>
                    <div class="editor-field"><%: Html.TextBoxFor(m => m.Email) %><%: Html.ValidationMessageFor(m => m.Email) %></div>
                    <div class="editor-label"><%: Html.LabelFor(m => m.Password) %></div>
                    <div class="editor-field"><%: Html.PasswordFor(m => m.Password) %><%: Html.ValidationMessageFor(m => m.Password) %></div>
                    <div class="editor-label"><%: Html.LabelFor(m => m.ConfirmPassword) %></div>
                    <div class="editor-field"><%: Html.PasswordFor(m => m.ConfirmPassword) %><%: Html.ValidationMessageFor(m => m.ConfirmPassword) %></div>
                    <br />
                    <p><input type="submit" value="Register" /></p>
                </fieldset>
            </div>
        <% } %>
    </div>
</asp:Content>
