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
    [Authorize]
    public class OrderItemsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public OrderItemsController()
        {
            _context = new ApplicationDbContext();
        }
        [Authorize(Roles = "Admin, Manager")]
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

        // DELETE api/<controller>/5
        [Authorize(Roles = "Admin, Manager")]
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