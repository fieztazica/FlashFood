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
        [Display(Name = "UserId")]
        public string UserId { get; set; }
        [Required]
        [Display(Name = "SellerId")]
        public string SellerId { get; set; }
        [Required]
        [Display(Name = "PaidAt")]
        public DateTime PaidAt { get; set; }
      
        [Display(Name = "Paid")]
        public double Paid { get; set; }
        [Display(Name = "Total_Money")]
        public double Total_money { get; set; }

        [Display(Name ="List_Cart")]
        public List<CartItemViewModel> Carts { get; set; }
    }
}