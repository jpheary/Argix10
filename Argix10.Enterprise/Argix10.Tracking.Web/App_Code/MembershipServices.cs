using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.Profile;
using System.Collections.Generic;
using System.Web.Configuration;
using Argix.Enterprise;

//using Microsoft.Office.Interop.Excel;

//
public class MembershipServices {
    //Members
    private string mUsername="";
    
    public const string GUESTROLE = "guests";
    public const string ADMINROLE = "administrators";
    public const string FILECLAIMSROLE = "FileClaimsMember";
    public const string POSEARCHROLE = "pomembers";
    public const string REPORTSROLE = "rsmembers";
    public const string TRACKINGROLE = "members";
    public const string TRACKINGWSROLE = "wsmembers";

    //Interface
    public MembershipServices() {
        //Constructor for the current logged-in member
        if(Membership.GetUser() != null) this.mUsername = Membership.GetUser().UserName;
    }
    public MembershipServices(string username) { 
        //Constructor for any member
        if(username.Trim().Length == 0) throw new ApplicationException("Username (" + username + ") is invalid.");
        if(Membership.FindUsersByName(username).Count == 0) throw new ApplicationException("Username (" + username + ") is not a member.");
        this.mUsername = username;
    }
    public string Username { get { return this.mUsername; } }
    public MembershipUser Member { get { return Membership.GetUser(this.mUsername); } }
    public ProfileCommon MemberProfile {
        get {
            ProfileCommon profile = new ProfileCommon().GetProfile(this.mUsername);
            if(IsAdmin || IsArgix) profile.Type = "";
            return profile;
        }
    }
    public bool IsArgix {
        get {
            ProfileCommon profile = GetMemberProfile(this.mUsername);
            return (profile.ClientVendorID == TrackingGateway.ID_ARGIX);
        }
    }
    public bool IsAdmin { get { return Roles.IsUserInRole(this.mUsername,ADMINROLE); } }
    public bool IsFileClaims { get { return IsArgix || IsAdmin || Roles.IsUserInRole(this.mUsername, FILECLAIMSROLE); } }
    public bool IsPOMember { get { return Roles.IsUserInRole(this.mUsername,POSEARCHROLE); } }
    public bool IsRSMember { get { return Roles.IsUserInRole(this.mUsername,REPORTSROLE); } }
    public bool IsWebServiceRole { get { return Roles.IsUserInRole(this.mUsername,TRACKINGWSROLE); } }
    public bool IsPasswordExpired {
        get {
            MembershipUser user = GetMember(this.mUsername);
            ProfileCommon profile = GetMemberProfile(this.mUsername);
            int expireDays = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings["PasswordExpiration"]);
            TimeSpan ts = DateTime.Today.Subtract(user.LastPasswordChangedDate);
            if(ts.Days > expireDays || profile.PasswordReset)
                return true;
            else
                return false;
        }
    }
    
    public MembershipDataset GetTrackingUsers() {
        //
        ProfileCommon profileCommon = new ProfileCommon();
        ProfileCommon profile = null;
        string email = "";
        string[] users = Roles.GetUsersInRole(TRACKINGROLE);
        MembershipDataset member = new MembershipDataset();

        //Append members
        for(int i=0; i<users.Length; i++) {
            email = Membership.GetUser(users[i]).Email;
            profile = profileCommon.GetProfile(users[i]);
            if(profile.Type.Length == 0) profile.Type = "client";
            if (profile.ClientVendorID.Length == 0) profile.ClientVendorID = TrackingGateway.ID_ARGIX;
            member.MemberTable.AddMemberTableRow(profile.UserName,profile.UserFullName,email,profile.Company,profile.Type,profile.ClientVendorID,profile.WebServiceUser,profile.LastActivityDate,profile.LastUpdatedDate);
        }
        member.AcceptChanges();
        return member;
    }
    public MembersDataset GetMembers() {
        //Load a list of Member selections
        MembersDataset ds = new MembersDataset();
        MembershipUserCollection members = Membership.GetAllUsers();
        foreach(MembershipUser member in members) {
            MembersDataset.MembershipTableRow row = ds.MembershipTable.NewMembershipTableRow();
            row.Comment = member.Comment;
            row.CreateDate = member.CreationDate;
            row.Email = member.Email;
            row.IsApproved = member.IsApproved;
            row.IsLockedOut = member.IsLockedOut;
            row.IsOnline = member.IsOnline;
            row.LastActivityDate = member.LastActivityDate;
            row.LastLockoutDate = member.LastLockoutDate;
            row.LastLoginDate = member.LastLoginDate;
            row.LastPasswordChangedDate = member.LastPasswordChangedDate;
            row.PasswordQuestion = member.PasswordQuestion;
            row.UserName = member.UserName;

            ProfileCommon profile = new ProfileCommon().GetProfile(member.UserName);
            row.UserFullName = profile.UserFullName;
            row.Company = profile.Company;
            ds.MembershipTable.AddMembershipTableRow(row);
        }
        ds.AcceptChanges();
        return ds;
    }
    public MembershipUser GetMember(string username) { return Membership.GetUser(username); }
    public ProfileCommon GetMemberProfile(string username) {
        //
        ProfileCommon profile = new ProfileCommon().GetProfile(username);
        if (Roles.IsUserInRole(username,ADMINROLE) || profile.ClientVendorID == TrackingGateway.ID_ARGIX) profile.Type = "";
        return profile;
    }
    public void UpdateUser(string userID,string userFullName,string email,string company,string companyID) {
        MembershipUser user = Membership.GetUser(userID);
        user.Email = email;
        Membership.UpdateUser(user);

        ProfileCommon profile = new ProfileCommon().GetProfile(userID);
        if(profile.Type.Length == 0) profile.Type = "client";
        profile.UserFullName = userFullName;
        profile.Company = company;
        profile.ClientVendorID = companyID;
        profile.Save();
    }
    public bool DeleteUser(string userID) {
        return Membership.DeleteUser(userID,true);
    }
    public string GeneratePassword(int length) {
        //Generate a user friendly password
        string passowrd = "";

        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        Random r = new Random();
        for(int i = 0; i < length; i++) {
            int random = r.Next(0, chars.Length);
            passowrd += chars[random];
        }
        return passowrd;
    }

    public void CreateKateSpadeAccounts(string file) {
        //Open Excel file
        //Application ExcelApp = new Microsoft.Office.Interop.Excel.Application();
        //ExcelApp.Visible = false;
        //Workbook wb = ExcelApp.Workbooks.Open(file, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        //Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.Sheets["Store List Current"];
        //Range range = ws.UsedRange;
        //for(int i = 2; i <= range.Rows.Count; i++) {
        //    //Create KateSpade account
        //    string username = ((Range)ws.Cells[i, 9]).Value2.ToString();
        //    string fullnanme = ((Range)ws.Cells[i, 2]).Value2.ToString();
        //    string password = ((Range)ws.Cells[i, 10]).Value2 != null ? ((Range)ws.Cells[i, 10]).Value2.ToString() : "";
        //    string email = ((Range)ws.Cells[i, 11]).Value2.ToString();
        //    string storenumber = "3" + ((Range)ws.Cells[i, 3]).Value2.ToString();
        //    if(password.Trim().Length > 0) {
        //        MembershipCreateStatus status;
        //        MembershipUser member = Membership.CreateUser(username, password, email, null, null, true, out status);
        //        if(member == null) throw new ApplicationException("New member could not be created by the Membership system; no explanation provided (i.e. member==null).");
        //        member.Comment = "";
        //        Membership.UpdateUser(member);
        //        if(status == MembershipCreateStatus.Success) {
        //            //Update profile (add user to guest role 'cause anonymous user cannot have a profile)
        //            ProfileCommon profileCommon = new ProfileCommon();
        //            ProfileCommon profile = profileCommon.GetProfile(username);
        //            profile.Company = "KATE SPADE";
        //            profile.UserFullName = fullnanme;
        //            profile.Type = "Client";
        //            profile.ClientVendorID = "040";
        //            profile.StoreSearchType = "Argix";
        //            profile.StoreNumber = storenumber;
        //            profile.PasswordReset = profile.WebServiceUser = false;
        //            profile.Save();

        //            Roles.AddUserToRole(username, MembershipServices.TRACKINGROLE);
        //        }
        //    }
        //}

    }
}

