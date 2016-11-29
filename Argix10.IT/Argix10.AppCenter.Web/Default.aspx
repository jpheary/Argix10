<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpBody">
<div id="sideMenu">
	<ul>
		<li><asp:Button ID="navAllTerminals" runat="server" Text="All Terminals" OnCommand="OnNavigate" CommandName="AllTerminals" /></li>
		<li><asp:Button ID="navAtlanta" runat="server" Text="Atlanta" OnCommand="OnNavigate" CommandName="Atlanta" /></li>
		<li><asp:Button ID="navCarson" runat="server" Text="Carson" OnCommand="OnNavigate" CommandName="Carson" /></li>
		<li><asp:Button ID="navCharlotte" runat="server" Text="Charlotte" OnCommand="OnNavigate" CommandName="Charlotte" /></li>
		<li><asp:Button ID="navChicago" runat="server" Text="Chicago" OnCommand="OnNavigate" CommandName="Chicago" /></li>
		<li><asp:Button ID="navDallas" runat="server" Text="Dallas" OnCommand="OnNavigate" CommandName="Dallas" /></li>
		<li><asp:Button ID="navJamesburg" runat="server" Text="Jamesburg" OnCommand="OnNavigate" CommandName="Jamesburg" /></li>
		<li><asp:Button ID="navLakeland" runat="server" Text="Lakeland" OnCommand="OnNavigate" CommandName="Lakeland" /></li>
		<li><asp:Button ID="navMedley" runat="server" Text="Medley" OnCommand="OnNavigate" CommandName="Medley" /></li>
		<li><asp:Button ID="navRidgefield" runat="server" Text="Ridgefield" OnCommand="OnNavigate" CommandName="Ridgefield" /></li>
		<li><asp:Button ID="navSouthWindsor" runat="server" Text="South Windsor" OnCommand="OnNavigate" CommandName="SouthWindsor" /></li>
		<li><asp:Button ID="navWilmingtom" runat="server" Text="Wilmingtom" OnCommand="OnNavigate" CommandName="Wilmingtom" /></li>
	</ul>
</div>
<div id="content">
    <div>
        <asp:MultiView runat="server" ID="mvwPage" ActiveViewIndex="0">
            <asp:View ID="View1" runat="server">
                <div class="subtitle">All Terminals</div>
                <div class="terminalcontent">
                    <div class="sectiontitle">Applications for All Terminals</div>
                    <table>
                        <thead><tr><th class="urlheader">URL</th><th class="descheader">Description</th></tr></thead>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/argix10/tracking/login.aspx">Carton Tracking</a></td><td><span class="sectiondesc">Carton tracking web application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://ddu.argixdirect.com/argix10/bntracking">Consumer Carton Tracking</a></td><td><span class="sectiondesc">Consumer Direct Carton Tracking</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/argix10/apps/customers/crm/crm.application">CRM</a></td><td><span class="sectiondesc">Customer Relationship Management application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/argix08/apps/it/cubeanalyst/cubeanalyst.application">Cube Analyst</a></td><td><span class="sectiondesc">Cube scanner analyzer</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/Argix08/Apps/Terminals/DeliveryPoints/DeliveryPoints.application">Delivery Points</a></td><td><span class="sectiondesc">Synchronize Roadshow-AS/400 delivery points</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/Argix10/Apps/Terminals/DeliveryPoints/DeliveryPoints.application">Delivery Points 10</a></td><td><span class="sectiondesc">Synchronize Roadshow-AS/400 delivery points</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/Argix10/Imaging/TsortImages.aspx">Imaging Search</a></td><td><span class="sectiondesc">Search document images stored in SharePoint</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/argix10/apps/finance/invoicing/invoicing.application">Invoicing</a></td><td><span class="sectiondesc">Create client/vendor electronic (i.e. Excel) invoices.</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="https://dataexchange.syncada.com/">Invoicing: Web Trader</a></td><td><span class="sectiondesc">Upload Coach invoices to Syncada WebTrader</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/Argix10/Reports/Default.aspx">Reports 2013</a></td><td><span class="sectiondesc">Argix Direct web-based reporting application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/Reports/Pages/Folder.aspx?ItemPath=%2fShowcase+Reports&ViewMode=List">Showcase Reports</a></td><td><span class="sectiondesc">Showcase Reports using Sql Reporting</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/Argix10/Apps/Freight/TLViewer/TLViewer.application?terminal=5">TLViewer 10</a></td><td><span class="sectiondesc">View open TL's and calculate trailer load weight/cube</span></td></tr>
                    </table>
                </div>
           </asp:View>
            <asp:View ID="vwAtlanta" runat="server">
                <div class="subtitle">Atlanta</div>
                <div class="terminalcontent">
                    <div class="sectiontitle">Local Applications</div>
                    <table>
                        <thead><tr><th class="urlheader">URL</th><th class="descheader">Description</th></tr></thead>
                        <tr><td><a class="sectionurl" target="_blank" href="http://atrgxad1/argix08/apps/agentlinehaul/BearwareRouteImport/BearwareRouteImport.application?terminal=AT">Bearware Roadshow Import</a></td><td><span class="sectiondesc">Creates a Bearware import file from a Roadshow route file.</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/argix10/crm/default.aspx?agentNumber=0031">CRM</a></td><td><span class="sectiondesc">Issue management for local terminals</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/argix10/apps/freight/dispatch/dispatch.application?terminal=0031">Dispatch</a></td><td><span class="sectiondesc">Dispatch application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://atrgxad1/Argix08/Apps/Freight/FreightAssign/FreightAssign.application?terminal=AT">Freight Assignment</a></td><td><span class="sectiondesc">Tsort Freight Assignment application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://atrgxad1/Argix08/Apps/AgentLineHaul/ISDExport/ISDExport.application?terminal=AT">ISD Export</a></td><td><span class="sectiondesc">ISD (Inbound Scan Data) Export application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://atrgxad1/argix08/loadtenders">Load Tenders</a></td><td><span class="sectiondesc">Pratt Load Tenders web application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://atrgxad1/Argix08/Apps/AgentLineHaul/ShipSchedule/ShipSchedule.application?terminal=AT">Ship Schedule</a></td><td><span class="sectiondesc">Ship Schedule application for outbound line hauls</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/Argix08/TLViewer/Default.aspx?location=31">TL Viewer</a></td><td><span class="sectiondesc">TL Viewer web application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://atrgxad1/Argix08/Apps/Freight/ZoneClosing/ZoneClosing.application?terminal=AT">Zone Closing</a></td><td><span class="sectiondesc">Tsort Zone Closing application</span></td></tr>
                    </table>
                </div>
                <div class="roadshowcontent">
                    <div class="sectiontitle">Roadshow Links</div>
                    <ul>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Daily+Customer+Pickup&rs:Command=Render&RouteClass=A">Daily Customer Pickup Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Daily+Floor+Loadout&rs:Command=Render&RouteClass=A">Daily Floor Loadout Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Driver+Return+Times&rs:Command=Render&RouteClass=A">Driver Return Times Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Load+Percent+Summary&rs:Command=Render&RouteClass=A">Load Percent Summary Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Auto&rs:Command=Render&RouteClass=A">Routing (Auto) Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Auto+Summary&rs:Command=Render">Routing Summary (Auto) Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Edit&rs:Command=Render&RouteClass=A">Routing (Edit) Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Edit+Summary&rs:Command=Render">Routing Summary (Edit) Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Edit+Final&rs:Command=Render&RouteClass=A">Roadshow Routes Edit Final Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Auto+vs+Edit&rs:Command=Render">Routing Auto vs Edit Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmweb/argix08/apps/terminals/rsreports/rsreports.application?terminal=A">RSReports Application</a></li>
                    </ul>
                </div>
            </asp:View>
            <asp:View ID="vwCarson" runat="server">
                <div class="subtitle">Carson</div>
                <div class="terminalcontent">
                    <div class="sectiontitle">Local Applications</div>
                    <table>
                        <thead><tr><th class="urlheader">URL</th><th class="descheader">Description</th></tr></thead>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/Argix10/Apps/Freight/FreightAssign/FreightAssign.application?terminal=11">Freight Assignment</a></td><td><span class="sectiondesc">Tsort Freight Assignment application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/Argix10/Apps/AgentLineHaul/ShipSchedule/ShipSchedule.application?terminal=11">Ship Schedule</a></td><td><span class="sectiondesc">Ship Schedule application for outbound line hauls</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/Argix08/TLViewer/Default.aspx?location=11">TL Viewer</a></td><td><span class="sectiondesc">TL Viewer web application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/Argix10/Apps/Freight/ZoneClosing/ZoneClosing.application?terminal=11">Zone Closing</a></td><td><span class="sectiondesc">Tsort Zone Closing application</span></td></tr>
                    </table>
                </div>
           </asp:View>
            <asp:View ID="vwCharlotte" runat="server">
                <div class="subtitle">Charlotte</div>
                <div class="terminalcontent">
                    <div class="sectiontitle">Local Applications</div>
                    <table>
                        <thead><tr><th class="urlheader">URL</th><th class="descheader">Description</th></tr></thead>
                        <tr><td><a class="sectionurl" target="_blank" href="http://chrgxvmsql/argix08/apps/agentlinehaul/BearwareRouteImport/BearwareRouteImport.application?terminal=AT">Bearware Roadshow Import</a></td><td><span class="sectiondesc">Creates a Bearware import file from a Roadshow route file.</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/argix10/crm/default.aspx?agentNumber=0032">CRM</a></td><td><span class="sectiondesc">Issue management for local terminals</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/argix10/apps/freight/dispatch/dispatch.application?terminal=0032">Dispatch</a></td><td><span class="sectiondesc">Dispatch application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://chrgxvmsql/Argix08/Apps/Freight/FreightAssign/FreightAssign.application?terminal=CH">Freight Assignment</a></td><td><span class="sectiondesc">Tsort Freight Assignment application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://chrgxvmsql/Argix08/Apps/AgentLineHaul/ISDExport/ISDExport.application?terminal=CH">ISD Export</a></td><td><span class="sectiondesc">ISD (Inbound Scan Data) Export application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://chrgxvmsql/Argix08/Apps/AgentLineHaul/ShipSchedule/ShipSchedule.application?terminal=CH">Ship Schedule</a></td><td><span class="sectiondesc">Ship Schedule application for outbound line hauls</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/Argix08/TLViewer/Default.aspx?location=32">TL Viewer</a></td><td><span class="sectiondesc">TL Viewer web application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://chrgxvmsql/Argix08/Apps/Freight/ZoneClosing/ZoneClosing.application?terminal=CH">Zone Closing</a></td><td><span class="sectiondesc">Tsort Zone Closing application</span></td></tr>
                    </table>
                </div>
                 <div class="roadshowcontent">
                    <div class="sectiontitle">Roadshow Links</div>
                    <ul>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Daily+Customer+Pickup&rs:Command=Render&RouteClass=C">Daily Customer Pickup Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Daily+Floor+Loadout&rs:Command=Render&RouteClass=C">Daily Floor Loadout Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Driver+Return+Times&rs:Command=Render&RouteClass=C">Driver Return Times Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Load+Percent+Summary&rs:Command=Render&RouteClass=C">Load Percent Summary Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Auto&rs:Command=Render&RouteClass=C">Routing (Auto) Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Auto+Summary&rs:Command=Render">Routing Summary (Auto) Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Edit&rs:Command=Render&RouteClass=C">Routing (Edit) Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Edit+Summary&rs:Command=Render">Routing Summary (Edit) Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Edit+Final&rs:Command=Render&RouteClass=C">Roadshow Routes Edit Final Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Auto+vs+Edit&rs:Command=Render">Routing Auto vs Edit Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmweb/argix08/apps/terminals/rsreports/rsreports.application?terminal=C">RSReports Application</a></li>
                    </ul>
                </div>
            </asp:View>
            <asp:View ID="vwChicago" runat="server">
                <div class="subtitle">Chicago</div>
                <div class="terminalcontent">
                    <div class="sectiontitle">Local Applications</div>
                    <table>
                        <thead><tr><th class="urlheader">URL</th><th class="descheader">Description</th></tr></thead>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/Argix10/Apps/Freight/FreightAssign/FreightAssign.application?terminal=29">Freight Assignment</a></td><td><span class="sectiondesc">Tsort Freight Assignment application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/Argix10/Apps/AgentLineHaul/ISDExport/ISDExport.application?terminal=29">ISD Export</a></td><td><span class="sectiondesc">ISD (Inbound Scan Data) Export application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/Argix10/Apps/AgentLineHaul/ShipSchedule/ShipSchedule.application?terminal=29">Ship Schedule</a></td><td><span class="sectiondesc">Ship Schedule application for outbound line hauls</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/Argix08/TLViewer/Default.aspx?location=29">TL Viewer</a></td><td><span class="sectiondesc">TL Viewer web application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/Argix10/Apps/Freight/ZoneClosing/ZoneClosing.application?terminal=29">Zone Closing</a></td><td><span class="sectiondesc">Tsort Zone Closing application</span></td></tr>
                    </table>
                </div>
            </asp:View>
            <asp:View ID="vwDallas" runat="server">
                <div class="subtitle">Dallas</div>
                <div class="terminalcontent">
                    <div class="sectiontitle">Local Applications</div>
                    <table>
                        <thead><tr><th class="urlheader">URL</th><th class="descheader">Description</th></tr></thead>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/Argix10/Apps/Freight/FreightAssign/FreightAssign.application?terminal=55">Freight Assignment</a></td><td><span class="sectiondesc">Tsort Freight Assignment application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/Argix10/Apps/AgentLineHaul/ShipSchedule/ShipSchedule.application?terminal=55">Ship Schedule</a></td><td><span class="sectiondesc">Ship Schedule application for outbound line hauls</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/Argix08/TLViewer/Default.aspx?location=55">TL Viewer</a></td><td><span class="sectiondesc">TL Viewer web application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/Argix10/Apps/Freight/ZoneClosing/ZoneClosing.application?terminal=55">Zone Closing</a></td><td><span class="sectiondesc">Tsort Zone Closing application</span></td></tr>
                    </table>
                </div>
            </asp:View>
            <asp:View ID="vwJamesburg" runat="server">
                <div class="subtitle">Jamesburg</div>
                <div class="terminalcontent">
                    <div class="sectiontitle">Local Applications</div>
                    <table>
                        <thead><tr><th class="urlheader">URL</th><th class="descheader">Description</th></tr></thead>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/argix08/apps/agentlinehaul/BearwareRouteImport/BearwareRouteImport.application?terminal=JA">Bearware Roadshow Import</a></td><td><span class="sectiondesc">Creates a Bearware import file from a Roadshow route file.</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/argix10/crm/default.aspx?agentNumber=">CRM</a></td><td><span class="sectiondesc">Issue management for local terminals</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/argix10/apps/freight/dispatch/dispatch.application?terminal=">Dispatch</a></td><td><span class="sectiondesc">Description</span></td></tr>                               
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/Argix10/Dispatch/Default.aspx">Dispatch</a></td><td><span class="sectiondesc">Dispatch application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/Argix08/Apps/Freight/FreightAssign/FreightAssign.application?terminal=RF">Freight Assignment</a></td><td><span class="sectiondesc">Tsort Freight Assignment application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/argix08/apps/Freight/IndirectFreightAssign/IndirectFreightAssign.application?terminal=RF">Indirect Freight Assignment</a></td><td><span class="sectiondesc">Tsort Freight Assignment application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/argix08/loadtenders">Load Tenders</a></td><td><span class="sectiondesc">Pratt Load Tenders web application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/Argix08/Apps/AgentLineHaul/ISDExport/ISDExport.application?terminal=JA">ISD Export</a></td><td><span class="sectiondesc">ISD (Inbound Scan Data) Export application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/Argix08/Apps/AgentLineHaul/ShipSchedule/ShipSchedule.application?terminal=JA">Ship Schedule - JA</a></td><td><span class="sectiondesc">Jamesburg Ship Schedules</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/Argix08/Apps/AgentLineHaul/ShipSchedule/ShipSchedule.application?terminal=Shipper">Ship Schedule - Shippers</a></td><td><span class="sectiondesc">Shipper Ship Schedules</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/argix08/apps/agentlinehaul/shipscheduletemplates/shipscheduletemplates.application?terminal=">Ship Schedule Templates</a></td><td><span class="sectiondesc">Ship Schedule Templates application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/Argix08/TLViewer/Default.aspx?location=5">TL Viewer</a></td><td><span class="sectiondesc">TL Viewer web application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/Argix08/Apps/Freight/ZoneClosing/ZoneClosing.application?terminal=JA">Zone Closing</a></td><td><span class="sectiondesc">Tsort Zone Closing application</span></td></tr>
                    </table>
                </div>
            </asp:View>
            <asp:View ID="vwLakeland" runat="server">
                <div class="subtitle">Lakeland</div>
                <div class="terminalcontent">
                    <div class="sectiontitle">Local Applications</div>
                    <table>
                        <thead><tr><th class="urlheader">URL</th><th class="descheader">Description</th></tr></thead>
                        <tr><td><a class="sectionurl" target="_blank" href="http://llrgxad1/argix08/apps/agentlinehaul/BearwareRouteImport/BearwareRouteImport.application?terminal=LL">Bearware Roadshow Import</a></td><td><span class="sectiondesc">Creates a Bearware import file from a Roadshow route file.</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/argix10/crm/default.aspx?agentNumber=0130">CRM</a></td><td><span class="sectiondesc">Issue management for local terminals</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/argix10/apps/freight/dispatch/dispatch.application?terminal=0130">Dispatch</a></td><td><span class="sectiondesc">Dispatch application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://llrgxad1/Argix08/Apps/Freight/FreightAssign/FreightAssign.application?terminal=LL">Freight Assignment</a></td><td><span class="sectiondesc">Tsort Freight Assignment application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://llrgxad1/Argix08/Apps/AgentLineHaul/ISDExport/ISDExport.application?terminal=LL">ISD Export</a></td><td><span class="sectiondesc">ISD (Inbound Scan Data) Export application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://llrgxad1/argix08/loadtenders">Load Tenders</a></td><td><span class="sectiondesc">Pratt Load Tenders web application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://llrgxad1/Argix08/Apps/AgentLineHaul/ShipSchedule/ShipSchedule.application?terminal=LL">Ship Schedule</a></td><td><span class="sectiondesc">Ship Schedule application for outbound line hauls</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/Argix08/TLViewer/Default.aspx?location=13">TL Viewer</a></td><td><span class="sectiondesc">TL Viewer web application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://llrgxad1/Argix08/Apps/Freight/ZoneClosing/ZoneClosing.application?terminal=LL">Zone Closing</a></td><td><span class="sectiondesc">Tsort Zone Closing application</span></td></tr>
                    </table>
                </div>
                 <div class="roadshowcontent">
                    <div class="sectiontitle">Roadshow Links</div>
                    <ul>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Daily+Customer+Pickup&rs:Command=Render&RouteClass=L">Daily Customer Pickup Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Daily+Floor+Loadout&rs:Command=Render&RouteClass=L">Daily Floor Loadout Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Driver+Return+Times&rs:Command=Render&RouteClass=L">Driver Return Times Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Load+Percent+Summary&rs:Command=Render&RouteClass=L">Load Percent Summary Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Auto&rs:Command=Render&RouteClass=L">Routing (Auto) Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Auto+Summary&rs:Command=Render">Routing Summary (Auto) Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Edit&rs:Command=Render&RouteClass=L">Routing (Edit) Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Edit+Summary&rs:Command=Render">Routing Summary (Edit) Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Edit+Final&rs:Command=Render&RouteClass=L">Roadshow Routes Edit Final Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Auto+vs+Edit&rs:Command=Render">Routing Auto vs Edit Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmweb/argix08/apps/terminals/rsreports/rsreports.application?terminal=L">RSReports Application</a></li>
                    </ul>
                </div>
            </asp:View>
            <asp:View ID="vwMedley" runat="server">
                <div class="subtitle">Medley</div>
                <div class="terminalcontent">
                    <div class="sectiontitle">Local Applications</div>
                    <table>
                        <thead><tr><th class="urlheader">URL</th><th class="descheader">Description</th></tr></thead>
                        <tr><td><a class="sectionurl" target="_blank" href="http://mmrgxad1/argix08/apps/agentlinehaul/BearwareRouteImport/BearwareRouteImport.application?terminal=MM">Bearware Roadshow Import</a></td><td><span class="sectiondesc">Creates a Bearware import file from a Roadshow route file.</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/argix10/crm/default.aspx?agentNumber=0129">CRM</a></td><td><span class="sectiondesc">Issue management for local terminals</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/argix10/apps/freight/dispatch/dispatch.application?terminal=0129">Dispatch</a></td><td><span class="sectiondesc">Dispatch application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://mmrgxad1/Argix08/Apps/Freight/FreightAssign/FreightAssign.application?terminal=MM">Freight Assignment</a></td><td><span class="sectiondesc">Tsort Freight Assignment application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://mmrgxad1/Argix08/Apps/AgentLineHaul/ISDExport/ISDExport.application?terminal=MM">ISD Export</a></td><td><span class="sectiondesc">ISD (Inbound Scan Data) Export application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://mmrgxad1/Argix08/Apps/AgentLineHaul/ShipSchedule/ShipSchedule.application?terminal=MM">Ship Schedule</a></td><td><span class="sectiondesc">Ship Schedule application for outbound line hauls</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/Argix08/TLViewer/Default.aspx?location=12">TL Viewer</a></td><td><span class="sectiondesc">TL Viewer web application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://mmrgxad1/Argix08/Apps/Freight/ZoneClosing/ZoneClosing.application?terminal=MM">Zone Closing</a></td><td><span class="sectiondesc">Tsort Zone Closing application</span></td></tr>
                    </table>
                </div>
                 <div class="roadshowcontent">
                    <div class="sectiontitle">Roadshow Links</div>
                    <ul>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Daily+Customer+Pickup&rs:Command=Render&RouteClass=M">Daily Customer Pickup Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Daily+Floor+Loadout&rs:Command=Render&RouteClass=M">Daily Floor Loadout Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Driver+Return+Times&rs:Command=Render&RouteClass=M">Driver Return Times Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Load+Percent+Summary&rs:Command=Render&RouteClass=M">Load Percent Summary Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Auto&rs:Command=Render&RouteClass=M">Routing (Auto) Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Auto+Summary&rs:Command=Render">Routing Summary (Auto) Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Edit&rs:Command=Render&RouteClass=M">Routing (Edit) Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Edit+Summary&rs:Command=Render">Routing Summary (Edit) Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Edit+Final&rs:Command=Render&RouteClass=M">Roadshow Routes Edit Final Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Auto+vs+Edit&rs:Command=Render">Routing Auto vs Edit Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmweb/argix08/apps/terminals/rsreports/rsreports.application?terminal=M">RSReports Application</a></li>
                    </ul>
                </div>
            </asp:View>
            <asp:View ID="vwRidgefield" runat="server">
                <div class="subtitle">Ridgefield</div>
                <div class="terminalcontent">
                    <div class="sectiontitle">Local Applications</div>
                    <table>
                        <thead><tr><th class="urlheader">URL</th><th class="descheader">Description</th></tr></thead>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rfrgxweb/argix08/apps/agentlinehaul/BearwareRouteImport/BearwareRouteImport.application?terminal=RF">Bearware Roadshow Import</a></td><td><span class="sectiondesc">Creates a Bearware import file from a Roadshow route file.</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/argix10/crm/default.aspx?agentNumber=0001">CRM</a></td><td><span class="sectiondesc">Issue management for local terminals</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/argix10/apps/freight/dispatch/dispatch.application?terminal=0001">Dispatch</a></td><td><span class="sectiondesc">Dispatch application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rfrgxweb/Argix08/Apps/Freight/FreightAssign/FreightAssign.application?terminal=RF">Freight Assignment</a></td><td><span class="sectiondesc">Tsort Freight Assignment application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rfrgxweb/argix08/apps/Freight/IndirectFreightAssign/IndirectFreightAssign.application?terminal=RF">Indirect Freight Assignment</a></td><td><span class="sectiondesc">Tsort Freight Assignment application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rfrgxweb/argix08/apps/Freight/IndirectSort/IndirectSort.application?terminal=RF">Indirect Sort</a></td><td><span class="sectiondesc">Tsort Freight Assignment application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rfrgxweb/Argix08/Apps/AgentLineHaul/ISDExport/ISDExport.application?terminal=RF">ISD Export</a></td><td><span class="sectiondesc">ISD (Inbound Scan Data) Export application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/Argix08/TLViewer/Default.aspx?location=2">TL Viewer</a></td><td><span class="sectiondesc">TL Viewer web application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rfrgxweb/Argix08/Apps/Freight/ZoneClosing/ZoneClosing.application?terminal=RF">Zone Closing</a></td><td><span class="sectiondesc">Tsort Zone Closing application</span></td></tr>
                    </table>
                </div>
                 <div class="roadshowcontent">
                    <div class="sectiontitle">Roadshow Links</div>
                    <ul>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Daily+Customer+Pickup&rs:Command=Render&RouteClass=2">Daily Customer Pickup Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Daily+Floor+Loadout&rs:Command=Render&RouteClass=2">Daily Floor Loadout Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Driver+Return+Times&rs:Command=Render&RouteClass=2">Driver Return Times Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/Reports/Pages/Report.aspx?ItemPath=%2fFinance%2fDrivers+Used+Per+Day">Drivers Used Per Day Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Load+Percent+Summary&rs:Command=Render&RouteClass=2">Load Percent Summary Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Auto&rs:Command=Render&RouteClass=2">Routing (Auto) Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Auto+Summary&rs:Command=Render">Routing Summary (Auto) Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Edit&rs:Command=Render&RouteClass=2">Routing (Edit) Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Edit+Summary&rs:Command=Render">Routing Summary (Edit) Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Edit+Final&rs:Command=Render&RouteClass=2">Roadshow Routes Edit Final Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Auto+vs+Edit&rs:Command=Render">Routing Auto vs Edit Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/Reports/Pages/Report.aspx?ItemPath=%2fTerminals%2fRoadshow+Routes+Events">Routing Events Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmweb/argix08/apps/terminals/rsreports/rsreports.application?terminal=2">RSReports Application</a></li>
                    </ul>
                </div>
            </asp:View>
            <asp:View ID="vwSouthWindsor" runat="server">
                <div class="subtitle">South Windsor</div>
                <div class="terminalcontent">
                    <div class="sectiontitle">Local Applications</div>
                    <table>
                        <thead><tr><th class="urlheader">URL</th><th class="descheader">Description</th></tr></thead>
                        <tr><td><a class="sectionurl" target="_blank" href="http://ctrgxweb/argix08/apps/agentlinehaul/BearwareRouteImport/BearwareRouteImport.application?terminal=CT">Bearware Roadshow Import</a></td><td><span class="sectiondesc">Creates a Bearware import file from a Roadshow route file.</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/argix10/crm/default.aspx?agentNumber=0101">CRM</a></td><td><span class="sectiondesc">Issue management for local terminals</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/argix10/apps/freight/dispatch/dispatch.application?terminal=0101">Dispatch</a></td><td><span class="sectiondesc">Dispatch application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://ctrgxweb/Argix08/Apps/Freight/FreightAssign/FreightAssign.application?terminal=CT">Freight Assignment</a></td><td><span class="sectiondesc">Tsort Freight Assignment application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://ctrgxweb/Argix08/Apps/AgentLineHaul/ISDExport/ISDExport.application?terminal=CT">ISD Export</a></td><td><span class="sectiondesc">ISD (Inbound Scan Data) Export application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/Argix08/TLViewer/Default.aspx?location=4">TL Viewer</a></td><td><span class="sectiondesc">TL Viewer web application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://ctrgxweb/Argix08/Apps/Freight/ZoneClosing/ZoneClosing.application?terminal=CT">Zone Closing</a></td><td><span class="sectiondesc">Tsort Zone Closing application</span></td></tr>
                    </table>
                </div>
                 <div class="roadshowcontent">
                    <div class="sectiontitle">Roadshow Links</div>
                    <ul>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Daily+Customer+Pickup&rs:Command=Render&RouteClass=4">Daily Customer Pickup Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Daily+Floor+Loadout&rs:Command=Render&RouteClass=4">Daily Floor Loadout Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Driver+Return+Times&rs:Command=Render&RouteClass=4">Driver Return Times Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Load+Percent+Summary&rs:Command=Render&RouteClass=4">Load Percent Summary Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Auto&rs:Command=Render&RouteClass=4">Routing (Auto) Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Auto+Summary&rs:Command=Render">Routing Summary (Auto) Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Edit&rs:Command=Render&RouteClass=4">Routing (Edit) Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Edit+Summary&rs:Command=Render">Routing Summary (Edit) Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Edit+Final&rs:Command=Render&RouteClass=4">Roadshow Routes Edit Final Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Auto+vs+Edit&rs:Command=Render">Routing Auto vs Edit Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmweb/argix08/apps/terminals/rsreports/rsreports.application?terminal=4">RSReports Application</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Limited&rs:Command=Render&RouteClass=4">The Limited Report</a></li>
                    </ul>
                </div>
            </asp:View>
            <asp:View ID="vwWilmingtom" runat="server">
                <div class="subtitle">Wilmingtom</div>
                <div class="terminalcontent">
                    <div class="sectiontitle">Local Applications</div>
                    <table>
                        <thead><tr><th class="urlheader">URL</th><th class="descheader">Description</th></tr></thead>
                        <tr><td><a class="sectionurl" target="_blank" href="http://margxweb/argix08/apps/agentlinehaul/BearwareRouteImport/BearwareRouteImport.application?terminal=MA">Bearware Roadshow Import</a></td><td><span class="sectiondesc">Creates a Bearware import file from a Roadshow route file.</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/argix10/crm/default.aspx?agentNumber=0044">CRM</a></td><td><span class="sectiondesc">Issue management for local terminals</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/argix10/apps/freight/dispatch/dispatch.application?terminal=0044">Dispatch</a></td><td><span class="sectiondesc">Dispatch application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://margxweb/Argix08/Apps/Freight/FreightAssign/FreightAssign.application?terminal=MA">Freight Assignment</a></td><td><span class="sectiondesc">Tsort Freight Assignment application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://margxweb/Argix08/Apps/AgentLineHaul/ISDExport/ISDExport.application?terminal=MA">ISD Export</a></td><td><span class="sectiondesc">ISD (Inbound Scan Data) Export application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://rgxvmweb/Argix08/TLViewer/Default.aspx?location=3">TL Viewer</a></td><td><span class="sectiondesc">TL Viewer web application</span></td></tr>
                        <tr><td><a class="sectionurl" target="_blank" href="http://margxweb/Argix08/Apps/Freight/ZoneClosing/ZoneClosing.application?terminal=MA">Zone Closing</a></td><td><span class="sectiondesc">Tsort Zone Closing application</span></td></tr>
                    </table>
                </div>
                 <div class="roadshowcontent">
                    <div class="sectiontitle">Roadshow Links</div>
                    <ul>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Daily+Customer+Pickup&rs:Command=Render&RouteClass=3">Daily Customer Pickup Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Daily+Floor+Loadout&rs:Command=Render&RouteClass=3">Daily Floor Loadout Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Driver+Return+Times&rs:Command=Render&RouteClass=3">Driver Return Times Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Load+Percent+Summary&rs:Command=Render&RouteClass=3">Load Percent Summary Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Auto&rs:Command=Render&RouteClass=3">Routing (Auto) Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Auto+Summary&rs:Command=Render">Routing Summary (Auto) Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Edit&rs:Command=Render&RouteClass=3">Routing (Edit) Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Edit+Summary&rs:Command=Render">Routing Summary (Edit) Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Edit+Final&rs:Command=Render&RouteClass=3">Roadshow Routes Edit Final Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Routes+Auto+vs+Edit&rs:Command=Render">Routing Auto vs Edit Report</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmweb/argix08/apps/terminals/rsreports/rsreports.application?terminal=3">RSReports Application</a></li>
                        <li><a class="sectionurl" target="_blank" href="http://rgxvmsqlrpt08/ReportServer/Pages/ReportViewer.aspx?%2fTerminals%2fRoadshow+Limited&rs:Command=Render&RouteClass=3">The Limited Report</a></li>
                    </ul>
                </div>
            </asp:View>
        </asp:MultiView>
    </div>
</div>            
</asp:Content>
