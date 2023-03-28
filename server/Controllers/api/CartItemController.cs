using Newtonsoft.Json;
using server.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace server.Controllers
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
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<CartItem> GetCart([FromUri] PagingParameterModel pagingparametermodel)
        {
            var source = (from cart in _context.cartitem.
                            OrderBy(a => a.MealId)
                          select cart).AsQueryable();
            int count = source.Count();
            int CurrentPage = pagingparametermodel.pageNumber;
            int PageSize = pagingparametermodel.pageSize;
            int TotalCount = count;
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            var items = source.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
            var previousPage = CurrentPage > 1 ? "Yes" : "No";
            var nextPage = CurrentPage < TotalPages ? "Yes" : "No"; 
            var paginationMetadata = new
            {
                totalCount = TotalCount,
                pageSize = PageSize,
                currentPage = CurrentPage,
                totalPages = TotalPages,
                previousPage,
                nextPage
            };
            HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
            return items;
        }

        [AllowAnonymous]
        // GET api/<controller>/5
        public IHttpActionResult Get(string id, [FromUri] PagingParameterModel pagingparametermodel)
        {
            List<CartItem> source = _context.cartitem.Where(s => s.UserId == id).ToList();
            if (source == null || source.Count == 0)
            {
                return NotFound();
            }
            int count = source.Count();
            int CurrentPage = pagingparametermodel.pageNumber;
            int PageSize = pagingparametermodel.pageSize;
            int TotalCount = count;
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);
            var items = source.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
            var previousPage = CurrentPage > 1 ? "Yes" : "No";
            var nextPage = CurrentPage < TotalPages ? "Yes" : "No";
            var paginationMetadata = new
            {
                totalCount = TotalCount,
                pageSize = PageSize,
                currentPage = CurrentPage,
                totalPages = TotalPages,
                previousPage,
                nextPage
            };
            HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));
            return Ok(items);
        }

        // POST api/<controller>
        public IHttpActionResult Post(CartBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_context.meals.FirstOrDefault(a => a.Id == model.MealId) == null)
            {
                return BadRequest();
            }
            Meal meal = _context.meals.FirstOrDefault(a => a.Id == model.MealId);
            if(meal.AmountLeft < model.Amount)
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
            meal.AmountLeft -= (int)model.Amount;
            _context.Entry(meal).CurrentValues.SetValues(meal);
            _context.cartitem.Add(New_cart);
            _context.SaveChanges();
            return Ok("");
        }
        [HttpPost]
        public IHttpActionResult PostUpdate(CartDeleteBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_context.meals.FirstOrDefault(a => a.Id == model.MealId) == null)
            {
                return BadRequest();
            }
            Meal meal = _context.meals.FirstOrDefault(a => a.Id == model.MealId);
            if (meal.AmountLeft < model.Amount)
            {
                return BadRequest();
            }
            meal.AmountLeft -= (int)model.Amount;
            _context.Entry(meal).CurrentValues.SetValues(meal);
            List<CartItem> Cartitem = _context.cartitem.Where(s => s.UserId == model.UserId).ToList();
            CartItem EditCart = Cartitem.Find(a => a.MealId == model.MealId);
            EditCart.Amount = model.Amount;
            meal.AmountLeft -= (int)model.Amount;
            _context.Entry(meal).CurrentValues.SetValues(meal);
            _context.cartitem.AddOrUpdate(EditCart);
            _context.SaveChanges();
            return Ok("");
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(CartDeleteBindingModel model)
        {
            List<CartItem> Cartitem = _context.cartitem.Where(s => s.UserId == model.UserId).ToList();
            CartItem DeleteCart = Cartitem.Find(a => a.MealId == model.MealId);
            if (DeleteCart == null)
            {
                return BadRequest();
            }
            _context.cartitem.Remove(DeleteCart);
            _context.SaveChanges();
            return Ok("");
        }
    }
}