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
        public IHttpActionResult Post(int id)
        {
            
            return Ok("Saved");
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