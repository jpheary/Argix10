<%@ Page Title="Argix Logistics Carton Tracking" Language="C#" MasterPageFile="~/Views/Shared/Tracking.Master" Inherits="System.Web.Mvc.ViewPage<Argix.Models.TrackByItemRequest>" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpBody" runat="server">
    <div id="title">Track By Item</div>
    <div class="form">
    <table>
        <tr>
            <td style="text-align:right"><%: Html.LabelFor(m => m.TrackBy)%></td>
            <td><%: Html.DropDownListFor(m => m.TrackBy,new SelectList(Argix.Models.TrackByItemRequest.TrackByItemTypes,"Description","Description"))%></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td style="text-align:right; vertical-align:top"><%: Html.LabelFor(m => m.TrackingNumbers)%>&nbsp;</td>
            <td><%: Html.TextAreaFor(m => m.TrackingNumbers,10, 25, new { maxlength="400" })%></td>
            <td><%: Html.ValidationMessageFor(m => m.TrackingNumbers)%></td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
	    <tr><td colspan="3" style="height:19px; text-align:right"><input type="submit" value="Track" /></td></tr>
        <tr style="font-size:1px; height:12px"><td colspan="3">&nbsp;</td></tr>
        <tr style="font-size:1px; height:12px"><td colspan="3"><%: Html.ValidationSummary(true, "") %></td></tr>
        <tr style="font-size:1px; height:12px"><td colspan="3">&nbsp;</td></tr>
    </table>
    <p>Track up to ten cartons at a time. Enter one tracking# per line, or separate each with a comma.</p>
    </div>
    <script type="text/javascript">
        function checkTextLen(field, maxlimit) {
            if (field.value.length > maxlimit) {
                field.value = field.value.substring(0, maxlimit);
                alert('Length of the carton number exceeded the maximum allowed.');
                return false;
            }
        }
        function checkEmptyTextBox(field) {
            if (field.value.replace(/^\s+/, '').replace(/\s+$/, '') == '') {
                alert('No valid tracking numbers were entered.');
                return false;
            }
            else
                return true;
        }
        function removeNonNumerics(evt) {
            if (document.all.cboSearchBy[1].checked) {
                var keyCode = evt.which ? evt.which : evt.keyCode;
                if ((keyCode > '0'.charCodeAt() && keyCode <= '9'.charCodeAt()) || (keyCode == 13 || keyCode == 188))
                    return true;
                else 
                    return false;
            }
            else 
                return true; 
        }
    </script>
</asp:Content>

