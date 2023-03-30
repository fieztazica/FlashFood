﻿using Antlr.Runtime.Misc;
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

namespace server.Controllers.api
{
    public class OrdersController : ApiController
    {
        private readonly ApplicationDbContext _context;


        public OrdersController()
        {
            _context = new ApplicationDbContext();
        }
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
            List<OrderViewModel> orders = new List<OrderViewModel>();
            foreach (var order in Order)
            {
                order.User = _context.Users.FirstOrDefault(a => a.Id == order.UserId);
                orders.Add(OrderViewModel.FromOrder(order));
            }

            return Ok(orders);
        }
        //Get by UserID
        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult GetByUser(string Userid)
        {
            var Order_User = _context.Orders.Where(a => a.UserId == Userid).ToList();
            if (Userid == null)
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
        [HttpGet]
        public IHttpActionResult GetBySeller(string Sellerid)
        {
            var Order_Seller = _context.Orders.Where(a => a.SellerId == Sellerid).ToList();
            if (Sellerid == null)
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
        public IHttpActionResult Post(OrderBindingModel o)
        {

            var Cart = _context.Cartitems.Where(a => a.UserId == o.UserId).ToList();
            double money = 0;
            foreach (var t in Cart)
            {
                var meal = _context.Meals.FirstOrDefault(a => a.Id == t.MealId);
                money += t.Amount * meal.Price;
            }
            double Change;
            if (o.Paid >= money)
            {
                 Change = o.Paid - money;
            }
            else
            {
                return BadRequest("Ko Du Tien");
            }
            Order order = new Order()
            {
                UserId = o.UserId,
                SellerId = o.SellerId,
                PaidAt = o.PaidAt,
                Change = Change,
                Total_money = money,
                Paid = o.Paid,
            };
            _context.Orders.Add(order);
            _context.SaveChanges();

            //return RedirectToRoute("OrderItemsPost", new { orderId = order.Id });
            foreach (var item in Cart)
            {
                OrderItem orderItem = new OrderItem()
                {
                    MealId = item.MealId,
                    OrderId = order.Id,
                    Amount = item.Amount,
                    Meal = _context.Meals.FirstOrDefault(a => a.Id == item.MealId)
                };
                _context.OrderItems.Add(orderItem);
                _context.SaveChanges();
            }
            return Ok();
        }
        
        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, OrderBindingModel orderBindingModel)
        {
            var Order = _context.Orders.FirstOrDefault(a => a.Id == id);
            if(Order == null)
            {
                return BadRequest("not Found");
            }
            Order.Paid = orderBindingModel.Paid;
            Order.Total_money = orderBindingModel.Total_money;
            Order.Change = Order.Paid - Order.Total_money;
            _context.Orders.AddOrUpdate(Order);
            _context.SaveChanges();
            return Ok();
        }
        

        // DELETE api/<controller>/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var Orders = _context.Orders.FirstOrDefault(a => a.Id == id);
            if(Orders == null)
            {
                return BadRequest("Not Found Order");
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
    }
}