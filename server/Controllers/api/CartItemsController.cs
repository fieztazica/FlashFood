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
        public IHttpActionResult GetMine()
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
        [HttpPost]
        // POST api/<controller>
        public IHttpActionResult Create(CreateCartItemBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var UserId = User.Identity.GetUserId();
            var Meal = _context.Meals.FirstOrDefault(a => a.Id == model.MealId);
            List<CartItemViewModel> lstcart = new List<CartItemViewModel>();
            if (Meal == null && Meal.AmountLeft < model.Amount)
            {
                return BadRequest("The meal is not existed");
            }
            var cartItem = _context.CartItems.FirstOrDefault(a => a.MealId == model.MealId && a.UserId == UserId);
            if (cartItem != null)
            {
                var CartUpdate = _context.CartItems.Where(a => a.UserId == UserId).ToList();
                cartItem.Amount += model.Amount;
                _context.CartItems.AddOrUpdate(cartItem);
                //Update Meal
                Meal.AmountLeft -= model.Amount;
                _context.Meals.AddOrUpdate(Meal);
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
                UserId = UserId,
                Amount = model.Amount,
                Meal = _context.Meals.FirstOrDefault(a => a.Id == model.MealId),
            };
            _context.CartItems.Add(New_cart);
            //Update Meal
            Meal.AmountLeft -= model.Amount;
            _context.Meals.AddOrUpdate(Meal);
            _context.SaveChanges();

            var CartNow = _context.CartItems.Where(a => a.UserId == UserId).ToList();
            foreach (var c in CartNow)
            {
                c.Meal = _context.Meals.FirstOrDefault(m => m.Id == c.MealId);
                lstcart.Add(CartItemViewModel.FromCartItem(c));
            }
            return Ok(lstcart);
        }
        [HttpPost]
        public IHttpActionResult Update(DeleteCartItemBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //check Meal ton tai khong
            var Meal = _context.Meals.FirstOrDefault(a => a.Id == model.MealId);
            if (Meal == null && Meal.AmountLeft < model.Amount)
            {
                return BadRequest("The meal is not existed");
            }
            var UserId = User.Identity.GetUserId();
            List<CartItem> Cartitem = _context.CartItems.Where(s => s.UserId == UserId).ToList();
            CartItem EditCart = Cartitem.Find(a => a.MealId == model.MealId);

            List<CartItemViewModel> lstcart = new List<CartItemViewModel>();
            //Check neu Amount <0 thi xoa CartItem
            if (model.Amount <= 0)
            {
                _context.CartItems.Remove(EditCart);
                _context.SaveChanges();

                var CartDelete = _context.CartItems.Where(a => a.UserId == UserId).ToList();
                foreach (var c in CartDelete)
                {
                    c.Meal = _context.Meals.FirstOrDefault(m => m.Id == c.MealId);
                    lstcart.Add(CartItemViewModel.FromCartItem(c));
                }
                return Ok(lstcart);
            }
            //xu ly meal
            if(model.Amount < EditCart.Amount)
            {
                Meal.AmountLeft += (EditCart.Amount - model.Amount);
                _context.Meals.AddOrUpdate(Meal);
            }
            else
            {
                Meal.AmountLeft += (model.Amount - EditCart.Amount);
                _context.Meals.AddOrUpdate(Meal);
            }
            EditCart.Amount = model.Amount;
            _context.CartItems.AddOrUpdate(EditCart);
            _context.SaveChanges();

            var CartNow = _context.CartItems.Where(a => a.UserId == UserId).ToList();
            foreach (var c in CartNow)
            {
                c.Meal = _context.Meals.FirstOrDefault(m => m.Id == c.MealId);
                lstcart.Add(CartItemViewModel.FromCartItem(c));
            }
            return Ok(lstcart);
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int mealId)
        {
            var UserId = User.Identity.GetUserId();
            List<CartItem> Cartitem = _context.CartItems.Where(s => s.UserId == UserId).ToList();
            CartItem DeleteCart = Cartitem.Find(a => a.MealId == mealId);
            if (DeleteCart == null)
            {
                return BadRequest();
            }
            _context.CartItems.Remove(DeleteCart);
            _context.SaveChanges();

            List<CartItemViewModel> lstcart = new List<CartItemViewModel>();
            var CartNow = _context.CartItems.Where(a => a.UserId == UserId).ToList();
            foreach (var c in CartNow)
            {
                c.Meal = _context.Meals.FirstOrDefault(m => m.Id == c.MealId);
                lstcart.Add(CartItemViewModel.FromCartItem(c));
            }
            return Ok(lstcart);
        }
    }
}