using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Argix.Areas.Mobile.Models {
    public class ContactModel {
        [Required(ErrorMessage=" (required)")]
        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Title")]
        public string Title { get; set; }

        [DisplayName("Company")]
        public string Company { get; set; }

        [DisplayName("Address")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Required(ErrorMessage=" (required)")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [DisplayName("Phone")]
        public string Phone { get; set; }

        [DataType(DataType.PhoneNumber)]
        [DisplayName("Fax")]
        public string Fax { get; set; }

        [DisplayName("Send Brochure")]
        public bool Brochure { get; set; }

        [DisplayName("Provide Free Logistics Assessment")]
        public bool Assessment { get; set; }

        [DisplayName("Arrange Retail Sort Center Tour")]
        public bool Tour { get; set; }

        [DisplayName("Call Me")]
        public bool CallMe { get; set; }

        [DisplayName("Email Me")]
        public bool EmailMe { get; set; }

    }
}
