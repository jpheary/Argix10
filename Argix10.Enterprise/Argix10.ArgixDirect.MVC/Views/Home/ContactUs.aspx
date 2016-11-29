<%@ Page Title="Argix Direct - Contact Us" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Argix.Models.ContactModel>"  %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="MetaContent">
</asp:Content>
<asp:Content ID="cLeft" runat="server" ContentPlaceHolderID="LeftContent">
    <div id="title">Contact Us</div>
</asp:Content>
<asp:Content ID="cRight" runat="server" ContentPlaceHolderID="RightContent">
    <% using (Html.BeginForm()) { %>
    <div class="textContainer_Contact"> 
        <h4>RETAIL TODAY NEEDS EVERY POSSIBLE ADVANTAGE</h4>
        <p>Contact us to find out how our unique approach can help you manage inventory, support store management and reduce costs. We promise to provide the kinds of tangible answers you need to compete more effectively in these challenging times.</p>
        <p>Your stores gain a competitive edge when they use a delivery service designed for the way you do business. We would like the opportunity to demonstrate Argix Direct to you. We offer a fundamentally better process design and technology-driven solution than traditional parcel or LTL service providers.</p>
        <p>Check all that apply:</p>
        <table>
            <tr><td class="boldText"><%: Html.LabelFor(m => m.Brochure) %></td><td><%: Html.CheckBoxFor(m => m.Brochure)%></td></tr>
            <tr><td class="boldText"><%: Html.LabelFor(m => m.Assessment) %></td><td><%: Html.CheckBoxFor(m => m.Assessment)%></td></tr>
            <tr><td class="boldText"><%: Html.LabelFor(m => m.Tour) %></td><td><%: Html.CheckBoxFor(m => m.Tour)%></td></tr>
            <tr><td class="boldText"><%: Html.LabelFor(m => m.CallMe) %></td><td><%: Html.CheckBoxFor(m => m.CallMe)%></td></tr>
            <tr><td class="boldText"><%: Html.LabelFor(m => m.EmailMe) %></td><td><%: Html.CheckBoxFor(m => m.EmailMe)%></td></tr>
        </table>
    </div> 
    <div class="formContainer_Contact">
        <table>
            <tr><td class="boldText"><%: Html.LabelFor(m => m.Name) %><%: Html.ValidationMessageFor(m => m.Name) %></td></tr><tr><td><%: Html.TextBoxFor(m => m.Name) %></td></tr>
            <tr><td><%: Html.LabelFor(m => m.Title) %></td></tr><tr><td><%: Html.TextBoxFor(m => m.Title)%></td></tr>
            <tr><td><%: Html.LabelFor(m => m.Company) %></td></tr><tr><td><%: Html.TextBoxFor(m => m.Company)%></td></tr>
            <tr><td><%: Html.LabelFor(m => m.Address) %></td></tr><tr><td><%: Html.TextAreaFor(m => m.Address,3, 22, null)%></td></tr>
            <tr><td class="boldText"><%: Html.LabelFor(m => m.Email) %><%: Html.ValidationMessageFor(m => m.Email)%></td></tr><tr><td><%: Html.TextBoxFor(m => m.Email)%></td></tr>
            <tr><td><%: Html.LabelFor(m => m.Phone) %></td></tr><tr><td><%: Html.TextBoxFor(m => m.Phone)%></td></tr>
            <tr><td><%: Html.LabelFor(m => m.Fax) %></td></tr><tr><td><%: Html.TextBoxFor(m => m.Fax)%></td></tr>
        </table>
        <br />
        <input type="submit" value="Contact" />
        <%: Html.ValidationSummary(true, "Please enter required fields.") %>
    </div>
    <% } %>
</asp:Content>
