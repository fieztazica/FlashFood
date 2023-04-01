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
        [Display(Name = "PaidAt")]
        public DateTime PaidAt { get; set; }

        [Display(Name = "Paid")]
        public double Paid { get; set; }

        [Display(Name = "TotalMoney")]
        public double Total_money { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }
        [Required]
        [Display(Name ="ListCartItem")]
        public List<CartItemViewModel> Carts { get; set; }
    }
}