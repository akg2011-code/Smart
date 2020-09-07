using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SmartSite.Models
{
    public class News
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required(ErrorMessage = "Must Enter News Title")]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Enter News Details")]
        public string Details { get; set; }

        public string Image { get; set; }

        public DateTime Date { get; set; }

    }
}