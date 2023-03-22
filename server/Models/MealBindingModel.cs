using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace server.Models
{
    public class MealBindingModel
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Price")]
        public int Price { get; set; }
        [Required]
        [Display(Name = "AmountLeft")]
        public int AmountLeft { get; set; }
    }

    public class MealEditBindingModel
    {
        [Required]
        [Display(Name = "Price")]
        public int Price { get; set; }
        [Required]
        [Display(Name = "AmountLeft")]
        public int AmountLeft { get; set; }
    }
}