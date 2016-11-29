<%@ Page Title="Argix Direct - Contact Us" Language="C#" MasterPageFile="~/Areas/Mobile/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Argix.Models.ContactModel>" %>

<asp:Content ID="cMain" ContentPlaceHolderID="MainContent" runat="server">
    <% using (Html.BeginForm()) { %>
    <div class="textContainer"> 
        <h4>RETAIL TODAY NEEDS EVERY POSSIBLE ADVANTAGE</h4>
        <p>Contact us to find out how our unique approach can help you manage inventory, support store management and reduce costs. We promise to provide the kinds of tangible answers you need to compete more effectively in these challenging times.</p>
        <p>Your stores gain a competitive edge when they use a delivery service designed for the way you do business. We would like the opportunity to demonstrate Argix Direct to you. We offer a fundamentally better process design and technology-driven solution than traditional parcel or LTL service providers.</p>
    </div> 
	  
    <div class="txtContainer">
        <table>
            <tr><td class="boldText"><%: Html.LabelFor(m => m.Name) %><%: Html.ValidationMessageFor(m => m.Name) %></td><td><%: Html.TextBoxFor(m => m.Name) %></td></tr>
            <tr><td><%: Html.LabelFor(m => m.Title) %></td><td><%: Html.TextBoxFor(m => m.Title)%></td></tr>
            <tr><td><%: Html.LabelFor(m => m.Company) %></td><td><%: Html.TextBoxFor(m => m.Company)%></td></tr>
            <tr><td><%: Html.LabelFor(m => m.Address) %></td><td><%: Html.TextAreaFor(m => m.Address,3, 20, null)%></td></tr>
            <tr><td class="boldText"><%: Html.LabelFor(m => m.Email) %><%: Html.ValidationMessageFor(m => m.Email)%></td><td><%: Html.TextBoxFor(m => m.Email)%></td></tr>
            <tr><td><%: Html.LabelFor(m => m.Phone) %></td><td><%: Html.TextBoxFor(m => m.Phone)%></td></tr>
        </table>
        <br />
        <input type="submit" value="Contact" />
        <%: Html.ValidationSummary(true, "Please enter required fields.") %>
    </div>
    <% } %>
</asp:Content>
