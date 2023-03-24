using server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace server.Controllers
{
    public class CartItemController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public CartItemController()
        {
            _context = new ApplicationDbContext();
        }
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var Cartitem = _context.cartitem.ToList();
            if (Cartitem == null || Cartitem.Count == 0)
            {
                return NotFound();
            }
            return (IHttpActionResult)Ok(Cartitem);
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(string id)
        {
            var Cartitem = _context.cartitem.Where(s => s.UserId == id).ToList();
            if (Cartitem == null || Cartitem.Count == 0)
            {
                return NotFound();
            }
            return (IHttpActionResult)Ok(Cartitem);
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