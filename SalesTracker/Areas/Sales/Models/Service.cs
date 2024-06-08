using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SalesTracker.Areas.Sales.Models
{
    public class Service
    {
        [Key]
        public string Service_ID { get; set; }
        [Required]
        public string Service_Type { get; set; }
        [Required]
        public string Rate { get; set; }
        [Required]
        public string CusType { get; set; }
        [Required]
        public string Quantity { get; set; }
        [Required]
        public string Amount { get; set; }
    }
    
}