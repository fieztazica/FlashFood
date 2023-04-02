using Antlr.Runtime.Misc;
using Microsoft.AspNet.Identity;
using server.Models;
using server.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.UI;

namespace server.Controllers.api
{
    [Authorize]
    public class OrdersController : ApiController
    {
        private readonly ApplicationDbContext _context;


        public OrdersController()
        {
            _context = new ApplicationDbContext();
        }
        [Authorize(Roles = "Admin, Manager")]
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var Order = _context.Orders.ToList();
            if (Order == null || Order.Count == 0)
            {
                return NotFound();
            }
            List<OrderViewModel> orders = new List<OrderViewModel>();
            foreach (var order in Order)
            {
                order.User = _context.Users.FirstOrDefault(a => a.Id == order.UserId);
                orders.Add(OrderViewModel.FromOrder(order));
            }

            return Ok(orders);
        }

        public IHttpActionResult Get(int id)
        {
            var Order = _context.Orders.FirstOrDefault(a => a.Id == id);
            if (Order == null)
            {
                return NotFound();
            }

            var allCart = _context.OrderItems.Where(a => a.OrderId == id).ToList();
            var orderAll = new OrderViewModel
            {
                Id = id,
                OrderAt = Order.OrderAt,
                UserName = _context.Users.FirstOrDefault(a => a.Id == Order.UserId).UserName,
                PaidAt = Order.PaidAt,
                Paid = Order.Paid,
                Change = Order.Change,
                SellerId = Order.SellerId,
                Status = Order.Status,
                TotalMoney = Order.Total_money
            };
            List<OrderItemViewModel> items = new List<OrderItemViewModel>();
            foreach (var a in allCart)
            {
                a.Meal = _context.Meals.FirstOrDefault(m => m.Id == a.MealId);
                items.Add(OrderItemViewModel.FromOrderItem(a));
            }
            orderAll.Items = items;
            return Ok(orderAll);
        }
        //Get by UserID
        //GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult GetMine()
        {
            string userId = User.Identity.GetUserId();
            var Order_User = _context.Orders.Where(a => a.UserId == userId).ToList();
            if (userId == null)
            {
                return NotFound();
            }
            List<OrderViewModel> orders = new List<OrderViewModel>();
            foreach (var order in Order_User)
            {
                order.User = _context.Users.FirstOrDefault(a => a.Id == order.UserId);
                orders.Add(OrderViewModel.FromOrder(order));
            }
            return Ok(orders);
        }
        [Authorize(Roles = "Admin, Manager")]
        [HttpGet]
        public IHttpActionResult GetBySeller(string sellerId)
        {
            var Order_Seller = _context.Orders.Where(a => a.SellerId == sellerId).ToList();
            if (sellerId == null)
            {
                return NotFound();
            }
            List<OrderViewModel> orders = new List<OrderViewModel>();
            foreach (var order in Order_Seller)
            {
                order.User = _context.Users.FirstOrDefault(a => a.Id == order.UserId);
                orders.Add(OrderViewModel.FromOrder(order));
            }
            return Ok(orders);
        }
        //Create Order and OrderItem
        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult Create(OrderBindingModel o)
        {
            var UserId = o.Carts.FirstOrDefault().UserId;
            double money = 0;
            foreach (var t in o.Carts)
            {
                var meal = _context.Meals.FirstOrDefault(a => a.Id == t.MealId);
                money += t.Amount * meal.Price;
            }
           
            Order order = new Order()
            {
                UserId = UserId,
                SellerId = User.Identity.GetUserId(),
                Total_money = money,
            };
            _context.Orders.Add(order);
            _context.SaveChanges();
            foreach (var item in o.Carts)
            {
                OrderItem orderItem = new OrderItem()
                {
                    MealId = item.MealId,
                    OrderId = order.Id,
                    Amount = item.Amount,
                    Meal = _context.Meals.FirstOrDefault(a => a.Id == item.MealId)
                };
                var meal = _context.Meals.FirstOrDefault(a => a.Id == item.MealId);
                _context.OrderItems.Add(orderItem);
                var CartItem = _context.CartItems.FirstOrDefault(a => a.MealId == item.MealId && a.UserId == UserId);
                _context.CartItems.Remove(CartItem);
                _context.SaveChanges();
            }
            return Ok();
        }
        [Authorize(Roles = "Admin, Manager")]
        [HttpPut]
        // PUT api/<controller>/5
        public IHttpActionResult Update(int id, OrderBindingModel orderBindingModel)
        {
            var Order = _context.Orders.FirstOrDefault(a => a.Id == id);
            if (Order == null)
            {
                return NotFound();
            }
            Order.Paid = orderBindingModel.Paid;
            Order.Total_money = orderBindingModel.Total_money;
            Order.Change = Order.Paid - Order.Total_money;
            Order.Status = orderBindingModel.Status;
            _context.Orders.AddOrUpdate(Order);
            _context.SaveChanges();
            return Ok();
        }
        [Authorize(Roles = "Admin, Manager")]
        // DELETE api/<controller>/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var Orders = _context.Orders.FirstOrDefault(a => a.Id == id);
            if (Orders == null)
            {
                return NotFound();
            }
            var Orderitems = _context.OrderItems.Where(a => a.OrderId == id);
            //delete all OrderItem in this order 
            foreach (var item in Orderitems)
            {
                _context.OrderItems.Remove(item);
            }
            //delete this order
            _context.Orders.Remove(Orders);
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult UpdateStatus(int id, string status)
        {
            var Order = _context.Orders.FirstOrDefault(o => o.Id == id);
            if (Order == null)
            {
                return NotFound();
            }
            Order.Status = status;
            _context.Orders.AddOrUpdate(Order);
            _context.SaveChanges();
            return Ok();
        }
    }
}