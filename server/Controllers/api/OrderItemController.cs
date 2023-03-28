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

namespace server.Controllers.api
{
    public class OrderItemController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public OrderItemController()
        {
            _context = new ApplicationDbContext();
        }
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var Orderitem = _context.Orderitem.ToList();
            if (Orderitem == null || Orderitem.Count == 0)
            {
                return NotFound();
            }
            return Ok(Orderitem);
        }

        public IEnumerable<OrderItem> GetOderItem([FromUri] PagingParameterModel pagingparametermodel)
        {
            var source = (from cart in _context.Orderitem.
                            OrderBy(a => a.OrderId)
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

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var Orderitem = _context.Orderitem.Where(s => s.OrderId == id).ToList();
            if (Orderitem == null || Orderitem.Count == 0)
            {
                return NotFound();
            }
            return Ok(Orderitem);
        }

        // POST api/<controller>
        public IHttpActionResult Post(int Orderid)
        {
            var Order = _context.Order.FirstOrDefault(a => a.Id == Orderid);
            var CartItem = _context.cartitem.Where(a => a.UserId == Order.UserId).ToList();
            foreach(var item in CartItem)
            {
                OrderItem orderItem = new OrderItem()
                {
                    MealId = item.MealId,
                    OrderId = Orderid,
                    Amount = item.Amount,
                };
                _context.Orderitem.Add(orderItem);
                _context.SaveChanges();
            }
            return Ok();
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}