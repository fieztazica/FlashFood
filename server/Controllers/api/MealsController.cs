using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using server.Models;
using server.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
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

        // GET api/<controller>/5
        [AllowAnonymous]
        public IHttpActionResult Get(int id)
        {
            var meals_Id = _context.Meals.FirstOrDefault(a => a.Id == id);
            if (meals_Id == null)
            {
                return NotFound();
            }
            var viewMeals = MealViewModel.FromMeal(meals_Id);          
            return Ok(viewMeals);
        }

        // GET api/<controller>
        [AllowAnonymous]
        [HttpGet]
        public PagingResult Get([FromUri] PagingParameterModel pagingparametermodel)
        {
            // Return List of Customer  
            var source = (from meal in _context.Meals.
                            OrderBy(a => a.Id)
                          select meal).AsQueryable();
            int count = source.Count();

            // Parameter is passed from Query string if it is null then it default Value will be pageNumber:1  
            int CurrentPage = pagingparametermodel.pageNumber;

            // Parameter is passed from Query string if it is null then it default Value will be pageSize:20  
            int PageSize = pagingparametermodel.pageSize;

            // Display TotalCount to Records to User  
            int TotalCount = count;

            // Calculating Totalpage by Dividing (No of Records / Pagesize)  
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);

            // Returns List of Customer after applying Paging   
            var items = source.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

            // if CurrentPage is greater than 1 means it has previousPage  
            bool previousPage = CurrentPage > 1;

            // if TotalPages is greater than CurrentPage means it has nextPage  
            bool nextPage = CurrentPage < TotalPages;

            // Object which we are going to send in header   
            PagingResult returnData = new PagingResult
            {
                totalCount = TotalCount,
                pageSize = PageSize,
                currentPage = CurrentPage,
                totalPages = TotalPages,
                previousPage = previousPage,
                nextPage = nextPage,
                items = items,
            };

            // Returing List of Customers Collections  
            return returnData;
        }

        // POST api/<controller>
        [Authorize(Roles = "Admin, Manager")]
        public IHttpActionResult Post(MealBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_context.Meals.FirstOrDefault(a => a.Id == model.Id) != null)
            {
                return BadRequest();
            }
            var New_meal = new Meal()
            {
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
            if (EditMeal == null)
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