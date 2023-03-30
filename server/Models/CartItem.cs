using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace server.Models
{
    public class CartItem
    {
        public int MealId { get; set; }
        public string UserId { get; set; }
        [Required]
        public double Amount { get; set; }

        public Meal Meal { get; set; }
        public ApplicationUser User { get; set; }

    }
}