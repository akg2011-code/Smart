using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartSite.Models
{
    public class Product
    {
        [Required]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [AllowHtml]
        public string Description { get; set; }

        public string Image { get; set; }
        public string PdfFile { get; set; }
        public string Brand { get; set; }

        [ForeignKey("ProductType")]
        public int ProductTypeID { get; set; }

        public virtual ProductType ProductType { get; set; }
    }
}