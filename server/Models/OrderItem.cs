using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace server.Models
{
    public class OrderItem
    {
        public int OrderId { get; set; }
        public int MealId { get; set; }
        [Required]
        public int Amount { get; set; }

        public Order Order { get; set; }
        public Meal Meal { get; set; }
    }
}