using Microsoft.AspNet.Identity;
using server.Models;
using server.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace server.Controllers.api
{
    [Authorize]
    public class CartItemsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public CartItemsController()
        {
            _context = new ApplicationDbContext();
        }
        // GET api/<controller>
        [Authorize(Roles = "Admin, Manager")]
        public IHttpActionResult Get()
        {
            var Cartitem = _context.CartItems.ToList();
            List<CartItemViewModel> lstcart = new List<CartItemViewModel>();
            foreach (var c in Cartitem)
            {
                c.Meal = _context.Meals.FirstOrDefault(m => m.Id == c.MealId);
                lstcart.Add(CartItemViewModel.FromCartItem(c));
            }
            if (Cartitem == null || Cartitem.Count == 0)
            {
                return NotFound();
            }
            return Ok(lstcart);
        }
        [HttpGet]
        // GET api/<controller>/5
        public IHttpActionResult GetByUser()
        {
            var UserId = User.Identity.GetUserId();
            var Cartitem = _context.CartItems.Where(s => s.UserId == UserId).ToList();
            List<CartItemViewModel> lstcart = new List<CartItemViewModel>();
            foreach (var c in Cartitem)
            {
                c.Meal = _context.Meals.FirstOrDefault(m => m.Id == c.MealId);
                lstcart.Add(CartItemViewModel.FromCartItem(c));
            }
            if (Cartitem == null || Cartitem.Count == 0)
            {
                return NotFound();
            }
            return Ok(lstcart);
        }

        // POST api/<controller>
        public IHttpActionResult Post(CartBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var UserId = User.Identity.GetUserId();
            var Meal = _context.Meals.FirstOrDefault(a => a.Id == model.MealId);
            List<CartItemViewModel> lstcart = new List<CartItemViewModel>();
            if (Meal == null)
            {
                return BadRequest("The meal is not existed");
            }
            var cartItem = _context.CartItems.FirstOrDefault(a => a.MealId == model.MealId && a.UserId == UserId);
            if (cartItem != null)
            {
                var CartUpdate = _context.Cartitems.Where(a => a.UserId == model.UserId).ToList();
                cartItem.Amount += model.Amount;
                _context.CartItems.AddOrUpdate(cartItem);
                _context.SaveChanges();
                
                foreach (var c in CartUpdate)
                {
                    c.Meal = _context.Meals.FirstOrDefault(m => m.Id == c.MealId);
                    lstcart.Add(CartItemViewModel.FromCartItem(c));
                }
                return Ok(lstcart);
            }
            var New_cart = new CartItem()
            {
                MealId = model.MealId,
                UserId = model.UserId,
                Amount = model.Amount,
                Meal = _context.Meals.FirstOrDefault(a => a.Id == model.MealId),
                //User = _context.Users.FirstOrDefault(a => a.Id == model.UserId)
            };
            _context.CartItems.Add(New_cart);
            _context.SaveChanges();
            var CartNow = _context.Cartitems.Where(a => a.UserId == model.UserId).ToList();
            foreach (var c in CartNow)
            {
                c.Meal = _context.Meals.FirstOrDefault(m => m.Id == c.MealId);
                lstcart.Add(CartItemViewModel.FromCartItem(c));
            }
            return Ok(lstcart);
        }
        [HttpPost]
        public IHttpActionResult PostUpdate(CartDeleteBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_context.Meals.FirstOrDefault(a => a.Id == model.MealId) == null)
            {
                return BadRequest("The meal is not existed");
            }
            List<CartItem> Cartitem = _context.CartItems.Where(s => s.UserId == model.UserId).ToList();
            CartItem EditCart = Cartitem.Find(a => a.MealId == model.MealId);
            EditCart.Amount = model.Amount;
            _context.CartItems.AddOrUpdate(EditCart);
            _context.SaveChanges();
            List<CartItemViewModel> lstcart = new List<CartItemViewModel>();
            var CartNow = _context.Cartitems.Where(a => a.UserId == model.UserId).ToList();
            foreach (var c in CartNow)
            {
                c.Meal = _context.Meals.FirstOrDefault(m => m.Id == c.MealId);
                lstcart.Add(CartItemViewModel.FromCartItem(c));
            }
            return Ok(lstcart);
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(CartDeleteBindingModel model)
        {
            List<CartItem> Cartitem = _context.CartItems.Where(s => s.UserId == model.UserId).ToList();
            CartItem DeleteCart = Cartitem.Find(a => a.MealId == model.MealId);
            if (DeleteCart == null)
            {
                return BadRequest();
            }
            _context.CartItems.Remove(DeleteCart);
            _context.SaveChanges();
            List<CartItemViewModel> lstcart = new List<CartItemViewModel>();
            var CartNow = _context.Cartitems.Where(a => a.UserId == model.UserId).ToList();
            foreach (var c in CartNow)
            {
                c.Meal = _context.Meals.FirstOrDefault(m => m.Id == c.MealId);
                lstcart.Add(CartItemViewModel.FromCartItem(c));
            }
            return Ok(lstcart);
        }
    }
}