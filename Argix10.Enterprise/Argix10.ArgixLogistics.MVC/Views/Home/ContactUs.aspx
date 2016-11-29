<%@ Page Title="Argix Direct - Contact Us" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Argix.Models.ContactModel>"  %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpContent" runat="server">
    <% using (Html.BeginForm()) { %>
     <div id="wrap">
        <div id="header"><a href="<%: Url.Action("Login", "Home")%>"><img src="<%= Url.Content("~/Content/Images/img_01.gif") %>" alt="carton tracking click here" width="240" height="64" /></a><a href="<%: Url.Action("ContactUs", "Home")%>"><img src="<%= Url.Content("~/Content/Images/img_02.gif") %>" alt="request information click here" width="240" height="64" /></a></div>
        <div id="contact"><span class="contact-top">For more information, contact us at 732.656.2550</span><br /></div>
        <div id="billboard">
           <div id="textContainer_Contact"> 
                <p class="body-copy">Contact us to find out how our unique approach can help you manage inventory, support store management and reduce costs. We promise to provide the kinds of tangible answers you need to compete more effectively in these challenging times.</p>
                <p class="body-copy">Your stores gain a competitive edge when they use a delivery service designed for the way you do business. We would like the opportunity to demonstrate Argix Logistics to you. We offer a fundamentally better process design and technology-driven solution than traditional parcel or LTL service providers.</p>
                <p class="body-copy">Check all that apply:</p>
                <table>
                    <tr><td class="body-copy"><%: Html.LabelFor(m => m.SendBrochure)%></td><td><%: Html.CheckBoxFor(m => m.SendBrochure)%></td></tr>
                    <tr><td class="body-copy"><%: Html.LabelFor(m => m.ContactMe)%></td><td><%: Html.CheckBoxFor(m => m.ContactMe)%></td></tr>
                </table>
            </div> 
            <div id="formContainer_Contact">
                <table>
                    <tr><td class="body-copy"><%: Html.LabelFor(m => m.Name) %><%: Html.ValidationMessageFor(m => m.Name) %></td></tr><tr><td><%: Html.TextBoxFor(m => m.Name) %></td></tr>
                    <tr><td class="body-copy"><%: Html.LabelFor(m => m.Email) %><%: Html.ValidationMessageFor(m => m.Email)%></td></tr><tr><td><%: Html.TextBoxFor(m => m.Email)%></td></tr>
                    <tr><td class="body-copy"><%: Html.LabelFor(m => m.Phone) %></td></tr><tr><td><%: Html.TextBoxFor(m => m.Phone)%></td></tr>
                    <tr><td class="body-copy"><%: Html.LabelFor(m => m.Company) %></td></tr><tr><td><%: Html.TextBoxFor(m => m.Company)%></td></tr>
                    <tr><td class="body-copy"><%: Html.LabelFor(m => m.Address) %></td></tr><tr><td><%: Html.TextAreaFor(m => m.Address,3, 22, null)%></td></tr>
                </table>
                <br />
                <input type="submit" value="Submit" class="body-copy" />
                <%: Html.ValidationSummary(true, "Please enter required fields.") %>
            </div>
        </div>
        <div id="clear"></div>
        <div id="comingsoon">
            <p class="headline">New Website Coming Soon</p>
            <hr size="11" noshade="noshade" color="#ee2a24" /><br />
        </div>
        <div id="bottom_copy">
            <p class="body-copy">We are excited to inform you that we have changed our name from Argix Direct to Argix Logistics to <br />
            better communicate our new portfolio of services.
            </p>
            <div id="subnav"> 
                <p class="main-nav">&gt;  An &quot;a la carte&quot; approach to business
                </p>
                <p class="main-nav-sm">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Get ready to experience the Argix difference.</p>
                <p class="mich-sub">Transportation <br />
                Distribution <br />
                Logistics Management</p>
            </div>
            <div id="logo_space"><img src="<%= Url.Content("~/Content/Images/argix-logo.gif") %>" width="322" height="109" alt="Argix" /></div>
        </div>
    </div>
    <% } %>
</asp:Content>
