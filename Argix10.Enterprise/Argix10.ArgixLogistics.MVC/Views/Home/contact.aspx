<%@Page Title="Argix:: Contact" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Argix.Models.ContactModel>" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpContent" runat="server">
<% using (Html.BeginForm()) { %>
    <div class="header-blank"></div>
    <div class="content">
        <span class="big_caps">R</span><span class="titles">equest Information</span>
        <p class="body-copy">Contact us to find out more on how the Argix Network can provide your organization with a faster, 
        smarter and more affordable logistics solution. You'll enjoy a logistics program that is as unique as your brand.<br />
        <br />
        There's a better way to improve your performance, let us show you how.</p>
        <div style="padding-left:55px;">
            <table width="500" border="0">
                <tr>
                    <td>
                        <table width="250">
                            <tr><td valign="middle" align="right" width="80"><strong><%: Html.LabelFor(m => m.Name) %><%: Html.ValidationMessageFor(m => m.Name) %></strong></td><td valign="middle" align="left"><%: Html.TextBoxFor(m => m.Name) %></td></tr>
                            <tr><td valign="middle" align="right"><strong><%: Html.LabelFor(m => m.Company) %><%: Html.ValidationMessageFor(m => m.Company) %></strong></td><td valign="middle" align="left"><%: Html.TextBoxFor(m => m.Company)%></td></tr>
                            <tr><td valign="middle" align="right"><strong><%: Html.LabelFor(m => m.Title)%><%: Html.ValidationMessageFor(m => m.Title) %></strong>:</td><td valign="middle" align="left"><%: Html.TextBoxFor(m => m.Title)%></td></tr>
                            <tr><td valign="middle" align="left" colspan="2" style="padding-top:10px;">
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr><td valign="middle" align="left"><%: Html.CheckBoxFor(m => m.SendBrochure)%>&nbsp;<%: Html.LabelFor(m => m.SendBrochure)%></td></tr>
                                    <tr><td valign="middle" align="left"><%: Html.CheckBoxFor(m => m.FreeAssessment)%>&nbsp;<%: Html.LabelFor(m => m.FreeAssessment)%></td></tr>
                                    <tr><td valign="middle" align="left"><%: Html.CheckBoxFor(m => m.ScheduleTour)%>&nbsp;<%: Html.LabelFor(m => m.ScheduleTour)%></td></tr>
                                    <tr><td valign="middle" align="left"><%: Html.CheckBoxFor(m => m.ContactMe)%>&nbsp;<%: Html.LabelFor(m => m.ContactMe)%></td></tr>
                                </table>
                            </td></tr>
                            <tr><td valign="middle" align="left" colspan="2" style="padding-top:7px;">
                                <input name="Submit" type="submit" class="button" id="Submit" value="Submit" />
                                <%: Html.ValidationSummary(true, "Please enter required fields.") %>
                            </td></tr>
                        </table>
                    </td>
                    <td valign="top">
                        <table width="250">
                            <tr><td valign="middle" align="right" width="164"><strong><%: Html.LabelFor(m => m.Email)%><%: Html.ValidationMessageFor(m => m.Email)%></strong>:</td><td width="217" align="left" valign="middle"><%: Html.TextBoxFor(m => m.Email)%></td></tr>
                            <tr><td valign="middle" align="right"><strong><%: Html.LabelFor(m => m.Phone)%><%: Html.ValidationMessageFor(m => m.Phone)%></strong></td><td valign="middle" align="left"><%: Html.TextBoxFor(m => m.Phone)%></td></tr>
                            <tr><td valign="middle" align="right"><strong><%: Html.LabelFor(m => m.Address)%><%: Html.ValidationMessageFor(m => m.Address)%></strong>:</td><td valign="middle" align="left"><%: Html.TextBoxFor(m => m.Address)%></td></tr>
                            <tr><td valign="middle" align="left" colspan="2" style="padding-top:10px;"><table cellpadding="0" cellspacing="0" border="0">
                        </table>
                    </td>
                </tr>
                <tr><td valign="middle" align="left" colspan="2" style="padding-top:10px;"><%: Html.LabelFor(m => m.AdditionalRequests)%><br /><%: Html.TextBoxFor(m => m.AdditionalRequests)%></td></tr>
            </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="side_bar_industries">
      <p class="sub-nav"><span class="sub-nav-bigger-red">Argix Logistics</span><br />
        100 Middlesex Center Blvd.<br />
        Jamesburg, NJ 08831<br />
        Tel: 732.656.2550</p>
    </div>
<% } %>
</asp:Content>
