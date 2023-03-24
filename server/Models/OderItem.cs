using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace server.Models
{
    public class OderItem
    {
        public int OderId { get; set; }
        public int MealId { get; set; }
        [Required]
        public double Amount { get; set; }

        public Oder Oder { get; set; }
        public Meal Meal { get; set; }
    }
}