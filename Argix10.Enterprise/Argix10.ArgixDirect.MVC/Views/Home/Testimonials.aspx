<%@ Page Title="Argix Direct - Testimonials" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="MetaContent">
    <meta name="keywords" content=" testimonials, Ann Taylor, Barnes&Noble, Barnes & Noble, Barnes and Noble, B&N, B&N.com" />
    <meta name="description" content="Argix Direct has delivered to Barnes & Noble stores since 1980.  Today we service over 700 Barnes & Noble stores nationwide."/>
    <meta name="description" content="Argix Direct has delivered to Ann Taylor stores since 1998."/>
</asp:Content>
<asp:Content ID="cLeft" runat="server" ContentPlaceHolderID="LeftContent">
    <div id="title">Home</div>
    <div id="submenu">
		<ul>
			<li><%: Html.ActionLink("Testimonials","Testimonials","Home")%></li>
		</ul>
	</div>
</asp:Content>
<asp:Content ID="cRight" runat="server" ContentPlaceHolderID="RightContent">
    <div class="textContainer">
        <h4>TESTIMONIALS</h4>
        <div id="testimonyMenu">
            <ul>
                <li><a href="javascript: unhide('bn'); hide('anntaylor');">Barnes &amp; Noble</a></li>
                <li><a href="javascript: hide('bn'); unhide('anntaylor');">Ann Taylor</a></li>
            </ul>
        </div>
        <div id="bn" class="testimonial unhidden">
   	        <img src="<%= Url.Content("~/content/Images/bn.jpg") %>"  alt="Barnes &amp; Noble testimony" width="400" />
        </div>  
	    <div id="anntaylor" class="testimonial hidden">
   	        <img src="<%= Url.Content("~/content/Images/anntaylor.jpg") %>" alt="Ann Taylor testimony" width="400" />
        </div>  
    </div>  
    <script type="text/javascript">
        function hide(divID) {
            var item = document.getElementById(divID);
            if (item) item.className = 'testimonial hidden';
        }
        function unhide(divID) {
            var item = document.getElementById(divID);
            if (item) item.className = 'testimonial unhidden';
        }
    </script>
</asp:Content>
