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

        public string Status { get; set; }

        List<OrderItemViewModel> Items { get; set; }

        public static OrderViewModel FromOrder(Order order)
        {
            return new OrderViewModel
            {
                Id = order.Id,
                OrderAt = order.OrderAt,
                UserName = order.User.UserName,
                PaidAt = (DateTime)order.PaidAt,
                Paid = (double)order.Paid,
                Change = (double)order.Change,
                SellerId = order.SellerId,
                TotalMoney = order.Total_money,
                Status = order.Status,
            };
        }
    }
    public class OrderAllItemViewModel
    {
        public int Id { get; set; }
        public DateTime OrderAt { get; set; }
        public DateTime PaidAt { get; set; }
        public double TotalMoney { get; set; }
        public double Paid { get; set; }
        public double Change { get; set; }
        public string SellerId { get; set; }
        public string UserName { get; set; }

        public string Status { get; set; }

        public List<OrderItemViewModel> ItemOrder { get; set; }

        public static OrderAllItemViewModel FromOrder(Order order)
        {
            return new OrderAllItemViewModel
            {
                Id = order.Id,
                OrderAt = order.OrderAt,
                UserName = order.User.UserName,
                PaidAt = (DateTime)order.PaidAt,
                Paid = (double)order.Paid,
                Change = (double)order.Change,
                SellerId = order.SellerId,
                TotalMoney = order.Total_money,
                Status = order.Status,
            };
        }
    }
}