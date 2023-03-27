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
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "ImageURL")]
        public string ImageURL { get; set; }
        [Required]
        [Display(Name = "Type")]
        public string Type { get; set; }
    }

    public class MealEditBindingModel
    {
        [Required]
        [Display(Name = "Price")]
        public int Price { get; set; }
        [Required]
        [Display(Name = "AmountLeft")]
        public int AmountLeft { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "ImageURL")]
        public string ImageURL { get; set; }
        [Required]
        [Display(Name = "Type")]
        public string Type { get; set; }
    }
}