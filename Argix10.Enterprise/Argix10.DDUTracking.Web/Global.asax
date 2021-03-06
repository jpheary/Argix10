<%@ Application Language="C#" %>

<script runat="server">
    void Application_Start(object sender, EventArgs e) {
        // Code that runs on application startup
        //Read web.config parameters once at application startup
        Application.Add("BNTracker",ConfigurationManager.AppSettings["BNTracker"]);
        Application.Add("BNDUNS",ConfigurationManager.AppSettings["BNDUNS"].Trim());
        Application.Add("BOOKAZINEDUNS",ConfigurationManager.AppSettings["BOOKAZINEDUNS"].Trim());
    }    
    void Application_End(object sender, EventArgs e) {
        //  Code that runs on application shutdown

    }        
    void Application_Error(object sender, EventArgs e) { 
        // Code that runs when an unhandled error occurs
        Exception exc = Server.GetLastError();
        if(exc == null)
            Server.ClearError();
        else {
            Session["Error"] = exc;
            Server.Transfer("~/Error.aspx");
        }
    }

    void Session_Start(object sender, EventArgs e) {
        // Code that runs when a new session is started
        Session.Add("FromDefault",false);
        Session.Add("TrackData",null);
        Session.Add("Error",null);
    }
    void Session_End(object sender, EventArgs e) {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
        Session.Clear();
    }
       
</script>
