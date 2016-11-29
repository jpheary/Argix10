using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace Argix.Models {
    //
    public class LoginModel {
        [Required(ErrorMessage = " (required)")]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = " (required)")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    public class RegisterModel {
        [Required(ErrorMessage = " (required)")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = " (required)")]
        [Display(Name = "Company")]
        public string Company { get; set; }

        [Required(ErrorMessage = " (required)")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email address")]
        public string Email { get; set; }

        [Required(ErrorMessage = " (required)")]
        [Display(Name = "UserID")]
        public string UserID { get; set; }

        [Required(ErrorMessage = " (required)")]
        [StringLength(100,ErrorMessage = "The {0} must be at least {2} characters long.",MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = " (required)")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password",ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class RecoverPasswordModel {
        [Required(ErrorMessage = " (required)")]
        [Display(Name = "UserID")]
        public string UserID { get; set; }
    }

    public class ChangePasswordModel {
        [Required(ErrorMessage = " (required)")]
        [Display(Name = "User ID")]
        public string UserID { get; set; }

        [Required(ErrorMessage = " (required)")]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = " (required)")]
        [StringLength(100,ErrorMessage = "The {0} must be at least {2} characters long.",MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword",ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
