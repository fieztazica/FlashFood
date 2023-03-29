using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace server.Models
{
    public class OrderBidingModel
    {
        [Required]
        [Display(Name = "UserId")]
        public string UserId { get; set; }
        [Required]
        [Display(Name = "SellerId")]
        public string SellerId { get; set; }
        [Required]
        [Display(Name = "PaidAt")]
        public DateTime PaidAt { get; set; }
        [Required]
        [Display(Name = "Paid")]
        public double Paid { get; set; }
        [Required]
        [Display(Name = "Change")]
        public double Change { get; set; }
        [Required]
        [Display(Name = "Status")]
        public string Status { get; set; }
    }
}