using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.Profile;
using System.Collections.Generic;
using System.Web.Configuration;

public class MembershipServices {
    //Members
    private string mUsername="";
    private const string CLIENTID_ARGIX = "000";

    public const string GUESTROLE = "guest";
    public const string ADMINROLE = "administrator";
    public const string SALESROLE = "salesrep";
    public const string CLIENTROLE = "client";

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
    public ProfileCommon MemberProfile { get { return new ProfileCommon().GetProfile(this.mUsername); } }
    public bool IsArgix { get { return (GetMemberProfile(this.mUsername).ClientID == CLIENTID_ARGIX); } }
    public bool IsAdmin { get { return Roles.IsUserInRole(this.mUsername,ADMINROLE); } }
    public bool IsSalesRep { get { return Roles.IsUserInRole(this.mUsername,SALESROLE); } }
    public bool IsClient { get { return Roles.IsUserInRole(this.mUsername,CLIENTROLE); } }

    public MembersDataset GetMembers() {
        //Load a list of Member selections
        MembersDataset ds = new MembersDataset();
        MembershipUserCollection members = Membership.GetAllUsers();
        foreach(MembershipUser member in members) {
            MembersDataset.MembershipTableRow row = ds.MembershipTable.NewMembershipTableRow();
            row.UserName = member.UserName;
            row.Password = "";
            row.Email = member.Email;
            row.IsApproved = member.IsApproved;
            row.IsLockedOut = member.IsLockedOut;
            row.CreateDate = member.CreationDate;
            row.LastLoginDate = member.LastLoginDate;
            row.Comment = member.Comment;
            row.IsOnline = member.IsOnline;
            row.LastActivityDate = member.LastActivityDate;
            row.ClientID = GetMemberProfile(member.UserName).ClientID;
            ds.MembershipTable.AddMembershipTableRow(row);
        }
        ds.AcceptChanges();
        return ds;
    }
    public MembershipUser GetMember(string username) { return Membership.GetUser(username); }
    public ProfileCommon GetMemberProfile(string username) { return new ProfileCommon().GetProfile(username); }
    public void ChangeMember(string userID,string email,string clientID) {
        MembershipUser user = Membership.GetUser(userID);
        user.Email = email;
        Membership.UpdateUser(user);

        ProfileCommon profile = new ProfileCommon().GetProfile(userID);
        profile.ClientID = clientID;
        profile.Save();
    }
    public bool RemoveMember(string userID) {
        return Membership.DeleteUser(userID,true);
    }

}

