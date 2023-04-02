using server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace server.ViewModels
{
    public class OrderItemViewModel
    {
        public int OrderId { get; set; }
        public int MealId { get; set; }
        [Required(ErrorMessage = "Please enter the amount.")]
        [Range(0, double.MaxValue, ErrorMessage = "Please enter a positive value for the amount.")]
        public int Amount { get; set; }
        public string MealName { get; set; }
        public decimal MealPrice { get; set; }

        public static OrderItemViewModel FromOrderItem(OrderItem orderItem)
        {
            return new OrderItemViewModel
            {
                OrderId = orderItem.OrderId,
                MealId = orderItem.MealId,
                Amount = orderItem.Amount,
                MealName = orderItem.Meal.Name,
                MealPrice = orderItem.Meal.Price
            };
        }
    }
}