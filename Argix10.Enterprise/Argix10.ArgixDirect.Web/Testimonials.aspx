<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Testimonials.aspx.cs" Inherits="Testimonials" %>

<asp:Content ID="Content1" ContentPlaceHolderID="LeftContent" Runat="Server">
    <div id="title">Home</div>
    <div id="submenu">
		<ul>
			<li><a id="A1" runat="server" href="~/Testimonials.aspx">Testimonials</a></li>
		</ul>
	</div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="RightContent" Runat="Server">
    <div class="textContainer"> 
        <h4> TESTIMONIALS </h4>
        <div class="testimonial">
   	        <img runat="server" src="~/styles/images/bn.jpg"  alt="" width="400" />
        </div>  
        <br /><br />
	    <div class="testimonial">
   	        <img runat="server" src="~/styles/images/anntaylor.jpg" alt="" width="400" />
        </div>  
    </div>  
</asp:Content>

