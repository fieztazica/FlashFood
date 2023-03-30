using server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace server.ViewModels
{
    public class MealViewModel
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public string Name { get; set; }
        public int AmountLeft { get; set; }
        public string ImageURL { get; set; }
        public string Type { get; set; }

        public static MealViewModel FromMeal(Meal meal)
        {
            return new MealViewModel
            {
                Id = meal.Id,
                Price = meal.Price,
                Name = meal.Name,
                AmountLeft = meal.AmountLeft,
                ImageURL = meal.ImageURL,
                Type = meal.Type
            };
        }

    }

}