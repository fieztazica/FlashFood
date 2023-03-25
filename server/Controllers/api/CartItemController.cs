using server.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace server.Controllers.api
{
    [Authorize]
    public class CartItemController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public CartItemController()
        {
            _context = new ApplicationDbContext();
        }
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var Cartitem = _context.cartitem.ToList();
            if (Cartitem == null || Cartitem.Count == 0)
            {
                return NotFound();
            }
            return Ok(Cartitem);
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(string id)
        {
            List<CartItem> Cartitem = _context.cartitem.Where(s => s.UserId == id).ToList();
            if (Cartitem == null || Cartitem.Count == 0)
            {
                return NotFound();
            }
            return Ok(Cartitem);
        }

        // POST api/<controller>
        public IHttpActionResult Post(CartBlidingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_context.meals.FirstOrDefault(a => a.Id == model.MealId) == null)
            {
                return BadRequest();
            }
            var New_cart = new CartItem()
            {
                MealId = model.MealId,
                UserId = model.UserId,
                Amount = model.Amount,
                //Meal = _context.meals.FirstOrDefault(a => a.Id == model.MealId),
                //User = _context.Users.FirstOrDefault(a => a.Id == model.UserId)
            };
            _context.cartitem.Add(New_cart);
            _context.SaveChanges();
            return Ok("Saved");
        }
        [HttpPost]
        public IHttpActionResult PostUpdate(CartDeleteBlidingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_context.meals.FirstOrDefault(a => a.Id == model.MealId) == null)
            {
                return BadRequest();
            }
            List<CartItem> Cartitem = _context.cartitem.Where(s => s.UserId == model.UserId).ToList();
            CartItem EditCart = Cartitem.Find(a => a.MealId == model.MealId);
            EditCart.Amount = model.Amount;
            _context.cartitem.AddOrUpdate(EditCart);
            _context.SaveChanges();
            return Ok("Saved");
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(CartDeleteBlidingModel model)
        {
            List<CartItem> Cartitem = _context.cartitem.Where(s => s.UserId == model.UserId).ToList();
            CartItem DeleteCart = Cartitem.Find(a => a.MealId == model.MealId);
            if (DeleteCart == null)
            {
                return BadRequest();
            }
            _context.cartitem.Remove(DeleteCart);
            _context.SaveChanges();
            return Ok("Saved");
        }
    }
}