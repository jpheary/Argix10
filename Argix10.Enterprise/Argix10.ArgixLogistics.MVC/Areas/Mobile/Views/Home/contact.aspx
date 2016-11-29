<%@ Page Title="Argix Logistics - Contact Us" Language="C#" MasterPageFile="~/Areas/Mobile/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Argix.Models.ContactModel>" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpContent" runat="server">
    <% using (Html.BeginForm()) { %>
    <div class="content">
        <span class="big_caps">R</span><span class="titles">equest Information</span>
        <p class="body-copy">
            Contact us to find out more on how the Argix Network can provide your organization with a faster, smarter and 
            more affordable logistics solution. You'll enjoy a logistics program that is as unique as your brand.<br />
            <br />
            There's a better way to improve your performance, let us show you how.
        </p>
        <%: Html.ValidationSummary(true, "Please enter required fields.") %>
        <table>
            <tr><td class="boldText"><%: Html.LabelFor(m => m.Name) %></td><td><%: Html.TextBoxFor(m => m.Name) %><%: Html.ValidationMessageFor(m => m.Name) %></td></tr>
            <tr><td><%: Html.LabelFor(m => m.Title) %></td><td><%: Html.TextBoxFor(m => m.Title)%></td></tr>
            <tr><td><%: Html.LabelFor(m => m.Company) %></td><td><%: Html.TextBoxFor(m => m.Company)%></td></tr>
            <tr><td><%: Html.LabelFor(m => m.Address) %></td><td><%: Html.TextAreaFor(m => m.Address,3, 20, null)%></td></tr>
            <tr><td class="boldText"><%: Html.LabelFor(m => m.Email) %></td><td><%: Html.TextBoxFor(m => m.Email)%><%: Html.ValidationMessageFor(m => m.Email)%></td></tr>
            <tr><td><%: Html.LabelFor(m => m.Phone) %></td><td><%: Html.TextBoxFor(m => m.Phone)%></td></tr>
            <tr><td colspan="2">&nbsp;</td></tr>
            <tr><td>&nbsp;</td>
                <td>
                    <table cellpadding="0" cellspacing="0" border="0">
                        <tr><td valign="middle" align="left"><%: Html.CheckBoxFor(m => m.SendBrochure)%>&nbsp;<%: Html.LabelFor(m => m.SendBrochure)%></td></tr>
                        <tr><td valign="middle" align="left"><%: Html.CheckBoxFor(m => m.FreeAssessment)%>&nbsp;<%: Html.LabelFor(m => m.FreeAssessment)%></td></tr>
                        <tr><td valign="middle" align="left"><%: Html.CheckBoxFor(m => m.ScheduleTour)%>&nbsp;<%: Html.LabelFor(m => m.ScheduleTour)%></td></tr>
                        <tr><td valign="middle" align="left"><%: Html.CheckBoxFor(m => m.ContactMe)%>&nbsp;<%: Html.LabelFor(m => m.ContactMe)%></td></tr>
                    </table>
                </td>
            </tr>
            <tr><td colspan="2">&nbsp;</td></tr>
            <tr><td>&nbsp;</td><td style="text-align:right; padding:0 25px 0 0"><input type="submit" value="Contact" /></td></tr>
        </table>
    </div>
    <% } %>
</asp:Content>
