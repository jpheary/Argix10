<%@ Page Title="Argix :: Difference" Language="C#" MasterPageFile="~/Areas/Mobile/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Argix.Models.ContactModel>" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpContent" runat="server">
    <div class="header-difference">
        <span class="headline">An </span><span class="headline-red">a la carte</span><span class="headline"> approach<br />to business</span>
    </div>
    <div class="content_sm">
        <span class="big_caps_t">T</span><span class="titles">he Argix Difference</span>
        <p class="body-copy-sm">Our comprehensive selection of transportation, distribution and supply chain management services allows us to mix and match options so we can develop a program that has everything you want and nothing you don't.</p>
        <p class="body-copy-sm">You'll enjoy a solution designed for your unique business model – with exceptional reliability and performance to help you reduce costs, increase efficiency and eliminate risks. That means you no longer have to worry about hidden costs that can wreak havoc on your bottom line. Argix positions your business to broaden your supply chain options without investment, strengthening your company and allowing you to focus on growth.</p>
    </div>
    <a class="pageMenu" href="javascript: unhide('network');">The Argix Network</a>
    <div id="network" class="hidden">
        <div class="content_sm">
            <p class="body-copy-sm">The Argix Network is a dynamic interaction of terminals, systems, expertise on a global platform. Linked by integrated smart systems to international supply chains, the Argix Network provides precision delivery and distribution solutions for a wide range of B2B and B2C customers.</p>
            <p class="body-copy-sm">We're experts at creating effective strategies and forming strong partnerships with our clients. Our collaborative approach allows you to select the Network in its entirety or in its many individual components – you choose what you need and we deliver.</p>
        </div>
    </div>
    <a class="pageMenu" href="javascript: unhide('about');">About Argix</a>
    <div id="about" class="hidden">
        <div class="content">
            <p class="body-copy">With over 30 years of building our Network and servicing our long-term customers, Argix 
            Logistics has established itself as an expert logistics solution provider.<br />
            <br />
            We have an outstanding reputation for developing successful strategic partnerships to bolster our in-house 
            capabilities and other outside opportunities. Our flexible approach to business means we are able to accommodate 
            your specific needs and help you keep costs under control without compromising the hard-earned reputation of 
            your brand.<br />
            <br />
            With Argix, you can count on excellence every step of the way. From optimum communication and team involvement 
            to DC bypass and fewer &quot;touches&quot; throughout the process. All supported by our tracking and reporting 
            technology which eliminates worries on your end.</p>
        </div>
    </div>
    <a class="pageMenu" href="javascript: unhide('contact');">Contact</a>
    <div id="contact" class="hidden">
        <% using (Html.BeginForm()) { %>
        <div class="content">
            <p class="body-copy">
                Contact us to find out more on how the Argix Network can provide your organization with a faster, smarter and 
                more affordable logistics solution. You'll enjoy a logistics program that is as unique as your brand.<br />
                <br />
                There's a better way to improve your performance, let us show you how.<br />
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
                        <table>
                            <tr><td><%: Html.CheckBoxFor(m => m.SendBrochure)%>&nbsp;<%: Html.LabelFor(m => m.SendBrochure)%></td></tr>
                            <tr><td><%: Html.CheckBoxFor(m => m.FreeAssessment)%>&nbsp;<%: Html.LabelFor(m => m.FreeAssessment)%></td></tr>
                            <tr><td><%: Html.CheckBoxFor(m => m.ScheduleTour)%>&nbsp;<%: Html.LabelFor(m => m.ScheduleTour)%></td></tr>
                            <tr><td><%: Html.CheckBoxFor(m => m.ContactMe)%>&nbsp;<%: Html.LabelFor(m => m.ContactMe)%></td></tr>
                        </table>
                    </td>
                </tr>
                <tr><td colspan="2">&nbsp;</td></tr>
                <tr><td>&nbsp;</td><td style="text-align:right; padding:0 25px 0 0"><input type="submit" value="Contact" /></td></tr>
            </table>
        </div>
        <% } %>
    </div>
</asp:Content>
