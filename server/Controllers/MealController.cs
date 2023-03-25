using Microsoft.AspNet.Identity;
using server.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace server.Controllers
{
    [Authorize]
    public class MealController : ApiController
    {

        private readonly ApplicationDbContext _context;


        public MealController()
        {
            _context = new ApplicationDbContext();
        }
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var meals = _context.meals.ToList();
            if(meals == null || meals.Count == 0)
            {
                return NotFound();
            }
            return (IHttpActionResult)Ok(meals);
        }


        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var meals_Id = _context.meals.FirstOrDefault(a => a.Id == id);
            if (meals_Id == null)
            {
                return NotFound();
            }
            return (IHttpActionResult)Ok(meals_Id);
        }

        // POST api/<controller>
        public IHttpActionResult Post(MealBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if(_context.meals.FirstOrDefault(a => a.Id == model.Id) != null)
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
            _context.meals.Add(New_meal);
            _context.SaveChanges();
            return Ok("Has Save");
        }
        // POST api/<controller>/id
        public IHttpActionResult Post(int id, MealEditBlindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var EditMeal = _context.meals.FirstOrDefault(a => a.Id == id);
            if ( EditMeal == null)
            {
                return BadRequest();
            }
            EditMeal.AmountLeft = model.AmountLeft;
            EditMeal.Price = model.Price;
            EditMeal.Name = model.Name;
            EditMeal.ImageURL = model.ImageURL;
            EditMeal.Type = model.Type;
            _context.meals.AddOrUpdate(EditMeal);
            _context.SaveChanges();
            return Ok("Has Save");
        }
        // PUT api/<controller>

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            var DeleteMeal = _context.meals.FirstOrDefault(a => a.Id == id);
            if (DeleteMeal == null)
            {
                return BadRequest();
            }
            _context.meals.Remove(DeleteMeal);
            _context.SaveChanges();
            return Ok("Has Save");
        }
    }
}