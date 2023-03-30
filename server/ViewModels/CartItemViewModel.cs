using server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace server.ViewModels
{
    public class CartItemViewModel
    {
        public int MealId { get; set; }
        public string UserId { get; set; }
        public double Amount { get; set; }
        public string MealName { get; set; }
        public string MealImageURL { get; set; }

        public static CartItemViewModel FromCartItem(CartItem cartItem)
        {
            return new CartItemViewModel
            {
                MealId = cartItem.MealId,
                UserId = cartItem.UserId,
                Amount = cartItem.Amount,
                MealName = cartItem.Meal.Name,
                MealImageURL = cartItem.Meal.ImageURL
            };
        }

    }

}