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
    public class OderItemController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public OderItemController()
        {
            _context = new ApplicationDbContext();
        }
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var Oderitem = _context.oderitem.ToList();
            if (Oderitem == null || Oderitem.Count == 0)
            {
                return NotFound();
            }
            return (IHttpActionResult)Ok(Oderitem);
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var Oderitem = _context.oderitem.Where(s => s.OderId == id).ToList();
            if (Oderitem == null || Oderitem.Count == 0)
            {
                return NotFound();
            }
            return (IHttpActionResult)Ok(Oderitem);
        }

        // POST api/<controller>
        public IHttpActionResult Post(int id)
        {
            
            return Ok("Has Save");
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