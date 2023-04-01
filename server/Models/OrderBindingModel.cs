using server.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace server.Models
{
    public class OrderBindingModel
    {
        [Required]
        [Display(Name = "PaidAt")]
        public DateTime PaidAt { get; set; }
        [Required]
        [Display(Name = "Paid")]
        public double Paid { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name ="List_Cart")]
        public List<CartItemViewModel> Carts { get; set; }
    }
}