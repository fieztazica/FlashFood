using Newtonsoft.Json;
using server.Models;
using server.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace server.Controllers.api
{
    public class OrderItemsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public OrderItemsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var Orderitem = _context.OrderItems.ToList();
            List<OrderItemViewModel> items = new List<OrderItemViewModel>();
            foreach(var o in Orderitem)
            {
                o.Meal = _context.Meals.FirstOrDefault(m => m.Id == o.MealId);
                items.Add(OrderItemViewModel.FromOrderItem(o));
            }
            if (Orderitem == null || Orderitem.Count == 0)
            {
                return NotFound();
            }
            return Ok(items);
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var Orderitem = _context.OrderItems.Where(s => s.OrderId == id).ToList();
            List<OrderItemViewModel> items = new List<OrderItemViewModel>();
            foreach (var o in Orderitem)
            {
                o.Meal = _context.Meals.FirstOrDefault(m => m.Id == o.MealId);
                items.Add(OrderItemViewModel.FromOrderItem(o));
            }
            if (Orderitem == null || Orderitem.Count == 0)
            {
                return NotFound();
            }
            return Ok(Orderitem);
        }
        /*
        // POST api/<controller>
        public IHttpActionResult Post(int Orderid)
        {
            var Order = _context.Orders.FirstOrDefault(a => a.Id == Orderid);
            var CartItem = _context.Cartitems.Where(a => a.UserId == Order.UserId).ToList();
            foreach(var item in CartItem)
            {
                OrderItem orderItem = new OrderItem()
                {
                    MealId = item.MealId,
                    OrderId = Orderid,
                    Amount = item.Amount,
                };
                _context.OrderItems.Add(orderItem);
                _context.SaveChanges();
            }
            return Ok();
        }
        */
        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id,int mealId)
        {
            var orderItem = _context.OrderItems.FirstOrDefault(a => a.OrderId == id && a.MealId == mealId);
            if (orderItem == null)
            {
                return BadRequest("not Found");
            }
            _context.OrderItems.Remove(orderItem);
            _context.SaveChanges();
            return Ok();
        }
    }
}