using Microsoft.AspNet.Identity;
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
    public class MealsController : ApiController
    {

        private readonly ApplicationDbContext _context;


        public MealsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET api/<controller>
        [AllowAnonymous]
        public IHttpActionResult Get()
        {
            var meals = _context.Meals.ToList();
            if(meals == null || meals.Count == 0)
            {
                return NotFound();
            }
            return Ok(meals);
        }


        // GET api/<controller>/5
        [AllowAnonymous]
        public IHttpActionResult Get(int id)
        {
            var meals_Id = _context.Meals.FirstOrDefault(a => a.Id == id);
            if (meals_Id == null)
            {
                return NotFound();
            }
            return Ok(meals_Id);
        }

        // POST api/<controller>
        [Authorize(Roles = "Admin, Manager")]
        public IHttpActionResult Post(MealBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(_context.Meals.FirstOrDefault(a => a.Id == model.Id) != null)
            {
                return BadRequest();
            }
            var New_meal = new Meal() {
                AmountLeft = model.AmountLeft,
                Price = model.Price,
                Name = model.Name,
                ImageURL = model.ImageURL,
                Type = model.Type,
            };
            _context.Meals.Add(New_meal);
            _context.SaveChanges();
            return Ok("Saved");
        }
        // POST api/<controller>/id
        [Authorize(Roles = "Admin, Manager")]
        public IHttpActionResult Post(int id, MealEditBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var EditMeal = _context.Meals.FirstOrDefault(a => a.Id == id);
            if ( EditMeal == null)
            {
                return BadRequest();
            }
            EditMeal.AmountLeft = model.AmountLeft;
            EditMeal.Price = model.Price;
            EditMeal.Name = model.Name;
            EditMeal.ImageURL = model.ImageURL;
            EditMeal.Type = model.Type;
            _context.Meals.AddOrUpdate(EditMeal);
            _context.SaveChanges();
            return Ok("Saved");
        }

        // PUT api/<controller>

        // DELETE api/<controller>/5
        [Authorize(Roles = "Admin, Manager")]
        public IHttpActionResult Delete(int id)
        {
            var DeleteMeal = _context.Meals.FirstOrDefault(a => a.Id == id);
            if (DeleteMeal == null)
            {
                return BadRequest();
            }
            _context.Meals.Remove(DeleteMeal);
            _context.SaveChanges();
            return Ok("Saved");
        }
    }
}