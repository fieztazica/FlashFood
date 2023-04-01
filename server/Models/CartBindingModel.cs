using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace server.Models
{
    public class CartBindingModel
    {
        [Required]
        [Display(Name = "MealId")]
        public int MealId { get; set; }
        [Required]
        [Display(Name = "Amount")]
        public double Amount { get; set; }
    }

    public class DeleteCartItemBindingModel
    {
        [Required]
        [Display(Name = "MealId")]
        public int MealId { get; set; }
    }

}