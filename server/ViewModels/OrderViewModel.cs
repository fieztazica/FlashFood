using server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace server.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public DateTime OrderAt { get; set; }
        public DateTime PaidAt { get; set; }
        public double TotalMoney { get; set; }
        public double Paid { get; set; }
        public double Change { get; set; }
        public string SellerId { get; set; }
        public string UserName { get; set; }

        public static OrderViewModel FromOrder(Order order)
        {
            return new OrderViewModel
            {
                Id = order.Id,
                OrderAt = order.OrderAt,
                UserName = order.User.UserName,
                PaidAt = order.PaidAt,
                Paid = order.Paid,
                Change = order.Change,
                SellerId = order.SellerId,
                TotalMoney = order.Total_money              
            };
        }
    }
}