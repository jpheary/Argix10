<%@ Page Title="Contact Us" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ContactUs.aspx.cs" Inherits="ContactUs" %>

<asp:Content ID="cLeft" runat="server" ContentPlaceHolderID="LeftContent">
    <div id="title">Contact Us</div>
</asp:Content>
<asp:Content ID="cRight" runat="server" ContentPlaceHolderID="RightContent">
<div class="textContainer_Contact"> 
    <h4 align="left">RETAIL TODAY NEEDS EVERY POSSIBLE ADVANTAGE</h4>
    <p align="left">Contact us to find out how our unique approach can help you manage inventory, support store management and reduce costs. We promise to provide the kinds of tangible answers you need to compete more effectively in these challenging times.</p>
    <p align="left">Your stores gain a competitive edge when they use a delivery service designed for the way you do business. We would like the opportunity to demonstrate Argix Direct to you.</p>
    <p align="left">We offer a fundamentally better process design and technology-driven solution than traditional parcel or LTL service providers.</p>
    <p align="left" class="boldText">Check all that apply:</p>
    <table border="0" cellspacing="0" cellpadding="0">
        <tr>
            <td valign="top" width="367" bgcolor="#cccccc">
                <table cellspacing="2" cellpadding="0" width="100%" border="1">
            <tbody>
                <tr>
                    <td class="boldText"><label for="BrochureLabel">Send Brochure</label></td>
                    <td><input tabindex="1" type="checkbox" value="Send Brochure" name="Brochure" /></td>
                </tr>
                <tr>
                    <td class="boldText"><label for="AssessmentLabel">Provide Free Logistics Assessment</label></td>
                    <td><input tabindex="2" type="checkbox" value="Provide Free Logistics Assessment" name="Assessment" /></td>
                </tr>
                <tr>
                    <td class="boldText"><label for="TourEastLabel">Arrange Retail Sort Center Tour </label></td>
                    <td><input tabindex="3" type="checkbox" value="Arrange Retail Sort Center Tour" name="TourEast" /></td>
                </tr>
                <tr>
                    <td class="boldText"><label for="CallEmailLabel">Call Me</label></td>
                    <td><input tabindex="5" type="checkbox" value="Call Me" name="CallMe" /></td>
                </tr>
                <tr>
                    <td class="boldText"><label for="CallEmailLabel">Email Me</label></td>
                    <td><input tabindex="6" type="checkbox" value="Email Me" name="EmailMe" /></td>
                </tr>
            </tbody>
            </table></td>
            <td valign="top" bgcolor="#cccccc" style="width: 2px"></td>
        </tr>
    </table>
    <h4>&nbsp;</h4>
</div>
<div class="formContainer_Contact">  
    <p>&nbsp;</p>
    <p class="boldText">Name<br />
            <asp:TextBox id="name" runat="server" ></asp:TextBox>
            <asp:RequiredFieldValidator ID="RFV_Name" runat="server" ControlToValidate="name" ErrorMessage="Name required." ValidationGroup="Contact">*</asp:RequiredFieldValidator>
    </p>
    <p class="boldText">Title<br />
            <label>
            <asp:TextBox id="TextBox1" runat="server" ></asp:TextBox>
        </label>
    </p>
    <p class="boldText">Company<br />
            <label>
            <asp:TextBox id="company" runat="server" ></asp:TextBox>
        </label>
    </p>
    <p class="boldText">Address<br />
        <label>
            <asp:TextBox id="address" runat="server" TextMode="MultiLine" Columns="30" Rows="5"></asp:TextBox>
        </label>
    </p>
    <p class="boldText">Email<br />
        <label>
            <asp:TextBox id="email" runat="server" ></asp:TextBox>
            <asp:RequiredFieldValidator ID="RFV_eMail" runat="server" ControlToValidate="email" ErrorMessage="Email required." ValidationGroup="Contact">*</asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="email" ErrorMessage="Invalid email." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Contact">*</asp:RegularExpressionValidator>
        </label>
    </p>
    <p class="boldText">Phone<br />
        <label>
            <asp:TextBox id="tel" runat="server" ></asp:TextBox>
        </label>
    </p>
    <p class="boldText">Fax<br />
        <label>
            <asp:TextBox id="fax" runat="server" ></asp:TextBox>
        </label>
    </p>
    <label>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="Contact" />      
        <br /><br />
        <asp:Button ID="submit" runat="server" Text="Submit" ValidationGroup="Contact" />      
    </label>
</div>
</asp:Content>
