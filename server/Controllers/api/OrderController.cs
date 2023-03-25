using server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace server.Controllers.api
{
    public class OrderController : ApiController
    {
        private readonly ApplicationDbContext _context;


        public OrderController()
        {
            _context = new ApplicationDbContext();
        }
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var Order = _context.Order.ToList();
            if (Order == null || Order.Count == 0)
            {
                return NotFound();
            }
            return Ok(Order);
        }
        //Get by UserID
        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult GetByUser(string Userid)
        {
            var Order_User = _context.Order.Where(a => a.UserId == Userid).ToList();
            if (Userid == null)
            {
                return NotFound();
            }
            return Ok(Order_User);
        }
        [HttpGet]
        public IHttpActionResult GetBySeller(string Sellerid)
        {
            var Order_Seller = _context.Order.Where(a => a.SellerId == Sellerid).ToList();
            if (Sellerid == null)
            {
                return NotFound();
            }
            return Ok(Order_Seller);
        }
        // POST api/<controller>
        public void Post([FromBody] string value)
        {
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