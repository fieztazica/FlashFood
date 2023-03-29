﻿using Microsoft.AspNet.Identity;
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
        public IHttpActionResult Get()
        {
            var Cartitem = _context.Cartitems.ToList();
            List<CartItemViewModel> lstcart = new List<CartItemViewModel>();
            foreach (var c in Cartitem)
            {
                var cartItem = _context.Cartitems.Include(ci => ci.Meal).FirstOrDefault(ci => ci.MealId == c.MealId);
                lstcart.Add(CartItemViewModel.FromCartItem(cartItem));
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
            var Cartitem = _context.Cartitems.Where(s => s.UserId == UserId).ToList();
            List<CartItemViewModel> lstcart = new List<CartItemViewModel>();
            foreach(var c in Cartitem)
            {
                var cartItem = _context.Cartitems.Include(ci => ci.Meal).FirstOrDefault(ci => ci.MealId == c.MealId);
                lstcart.Add(CartItemViewModel.FromCartItem(cartItem));
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
            if (_context.Meals.FirstOrDefault(a => a.Id == model.MealId) == null)
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
            _context.Cartitems.Add(New_cart);
            _context.SaveChanges();
            return Ok("Saved");
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
                return BadRequest();
            }
            List<CartItem> Cartitem = _context.Cartitems.Where(s => s.UserId == model.UserId).ToList();
            CartItem EditCart = Cartitem.Find(a => a.MealId == model.MealId);
            EditCart.Amount = model.Amount;
            _context.Cartitems.AddOrUpdate(EditCart);
            _context.SaveChanges();
            return Ok("Saved");
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(CartDeleteBindingModel model)
        {
            List<CartItem> Cartitem = _context.Cartitems.Where(s => s.UserId == model.UserId).ToList();
            CartItem DeleteCart = Cartitem.Find(a => a.MealId == model.MealId);
            if (DeleteCart == null)
            {
                return BadRequest();
            }
            _context.Cartitems.Remove(DeleteCart);
            _context.SaveChanges();
            return Ok("Saved");
        }
    }
}