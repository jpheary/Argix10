using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Argix.Models;

namespace Argix.Controllers {
    //
    public class AccountController:Controller {
        //Members
        private const string USER_LOGIN_NOACCOUNT = "Your account does not exist. Make sure you have entered a correct user id. If you never registered before then register first.";
        private const string USER_LOGIN_FAILED = "Login attempt failed. Your user id or password did not match.";
        private const string USER_ACCOUNT_LOCKEDOUT = "Your account is locked out. Please contact customer support at extranet.support@argixlogistics.com to resolve the issue.";
        private const string USER_ACCOUNT_INACTIVE = "Your account is currently inactive.";
        private const string PASSWORD_MIN_LENGHT = "Password should be minimum 6 characters long.";
        private const string EMAIL_NOT_UNIQUE = "Your email address already exists.";
        private const string USERID_NOT_UNIQUE = "This login UserID already exists.";

        //Interface
        public ActionResult Login() { return View(); }
        [HttpPost]
        public ActionResult Login(LoginModel model) {
            //
            if (ModelState.IsValid) {
                //Validate login
                MembershipUserCollection users = Membership.FindUsersByName(model.UserName);
                if (users.Count == 0) { ModelState.AddModelError("",USER_LOGIN_NOACCOUNT); return View(model); }
                if (users[model.UserName].IsLockedOut) { ModelState.AddModelError("",USER_ACCOUNT_LOCKEDOUT); return View(model); }
                if (!users[model.UserName].IsApproved) { ModelState.AddModelError("",USER_ACCOUNT_INACTIVE); return View(model); }
            
                //Validate userid/password
                if (Membership.ValidateUser(model.UserName,model.Password)) {
                    //Create an authentication ticket
                    FormsAuthentication.SetAuthCookie(model.UserName,false);
                    
                    //Force user to change the password if required as per policy (i.e. password reset or expired)
                    //if (isPasswordExpired)
                    //    return RedirectToAction("ChangePassword","Acccount");
                    //else {
                    return RedirectToAction("Index","Tracking");
                    //}
                }
                else { ModelState.AddModelError("",USER_LOGIN_FAILED); return View(model); }
            }
            else
                return View(model);
        }


        public ActionResult Register() { ViewBag.Popup = false; return View(); }
        [HttpPost]
        public ActionResult Register(RegisterModel model) {
            //
            ViewBag.Popup = false;
            if(ModelState.IsValid) {
                //Create a new user
                MembershipCreateStatus status = MembershipCreateStatus.Success;
                MembershipUser user = Membership.CreateUser(model.UserID,model.Password,model.Email,null,null,false,out status);
                switch (status) {
                    case MembershipCreateStatus.Success:
                        //Add user to the guest role
                        Roles.AddUserToRole(model.UserID,"guests");

                        //Create user profile and set properties value
                        System.Web.Profile.ProfileBase profile = System.Web.Profile.ProfileBase.Create(model.UserID);
                        profile.PropertyValues["Company"].PropertyValue = model.Company.Trim();
                        profile.PropertyValues["UserFullName"].PropertyValue = model.FullName.Trim();
                        profile.PropertyValues["WebServiceUser"].PropertyValue = false;
                        profile.Save();

                        //Send notification to the administrator
                        EmailGateway svcs = new EmailGateway();
                        svcs.SendRegistrationMessage(model.FullName.Trim(),model.Email.Trim());

                        //Disply confirmation to user
                        ViewBag.Popup = true;
                        break;
                    case MembershipCreateStatus.DuplicateEmail:
                        ModelState.AddModelError("",EMAIL_NOT_UNIQUE);
                        break;
                    case MembershipCreateStatus.DuplicateUserName:
                        ModelState.AddModelError("",USERID_NOT_UNIQUE);
                        break;
                    case MembershipCreateStatus.InvalidPassword:
                        ModelState.AddModelError("",PASSWORD_MIN_LENGHT);
                        break;
                    case MembershipCreateStatus.UserRejected:
                        break;
                }
            }
            return View(model);
        }


        public ActionResult RecoverPassword() { ViewBag.Popup = false; return View(); }
        [HttpPost]
        public ActionResult RecoverPassword(RecoverPasswordModel model) {
            //
            ViewBag.Popup = false;
            if (ModelState.IsValid) {
                MembershipUser user = Membership.GetUser(model.UserID);
                string password = Membership.GeneratePassword(6,0);
                user.ChangePassword(user.GetPassword(),password);

                //Set flag that forces user to change password on next login
                System.Web.Profile.ProfileBase profile = System.Web.Profile.ProfileBase.Create(model.UserID);
                profile.PropertyValues["PasswordReset"].PropertyValue = true;
                profile.Save();

                //Send an email
                EmailGateway esvcs = new EmailGateway();
                esvcs.SendPasswordResetMessage(user.UserName,user.Email,password);
                ViewBag.Popup = true;
            }
            return View(model);
        }


        [Authorize]
        public ActionResult ChangePassword() { return View();  }
        [Authorize][HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model) {
            if(ModelState.IsValid) {

                // ChangePassword will throw an exception rather
                // than return false in certain failure scenarios.
                bool changePasswordSucceeded;
                try {
                    MembershipUser currentUser = Membership.GetUser(User.Identity.Name,true /* userIsOnline */);
                    changePasswordSucceeded = currentUser.ChangePassword(model.OldPassword,model.NewPassword);
                }
                catch(Exception) {
                    changePasswordSucceeded = false;
                }

                if(changePasswordSucceeded) {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else {
                    ModelState.AddModelError("","The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
        public ActionResult ChangePasswordSuccess() { return View(); }
    }
}
