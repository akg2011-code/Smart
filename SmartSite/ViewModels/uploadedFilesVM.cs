using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmartSite.ViewModels
{
    public class uploadedFilesVM
    {
        [Required]
        public HttpPostedFileBase file { get; set; }
    }
}