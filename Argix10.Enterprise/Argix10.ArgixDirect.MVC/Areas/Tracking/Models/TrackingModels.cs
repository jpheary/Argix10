using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Argix.Areas.Tracking.Models {

    public class TrackByCartonModel {
        [Required(ErrorMessage=" (required)")]
        [DisplayName("Track By")]
        public string TrackBy { get; set; }

        [Required(ErrorMessage=" (required)")]
        [DataType(DataType.MultilineText)]
        [DisplayName("Items")]
        public string Items { get; set; }
    }
}
