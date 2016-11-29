using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Argix.Models {
    //
    public class ContactModel {
        [Required(ErrorMessage=" (required)")]
        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Company")]
        public string Company { get; set; }

        [DisplayName("Title")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = " (required)")]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email")]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [DisplayName("Phone")]
        public string Phone { get; set; }

        [DisplayName("Address")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [DisplayName("Send Brochure")]
        public bool SendBrochure { get; set; }

        [DisplayName("Free Logistics Assessment")]
        public bool FreeAssessment { get; set; }

        [DisplayName("Schedule a Tour")]
        public bool ScheduleTour { get; set; }
        
        [DisplayName("Contact Me")]
        public bool ContactMe { get; set; }

        [DisplayName("Additional Requests")]
        [DataType(DataType.MultilineText)]
        public string AdditionalRequests { get; set; }
    }
}
