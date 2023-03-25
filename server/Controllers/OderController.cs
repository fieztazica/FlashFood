using server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace server.Controllers
{
    public class OderController : ApiController
    {
        private readonly ApplicationDbContext _context;


        public OderController()
        {
            _context = new ApplicationDbContext();
        }
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var Oder = _context.Oder.ToList();
            if (Oder == null || Oder.Count == 0)
            {
                return NotFound();
            }
            return (IHttpActionResult)Ok(Oder);
        }
        //Get by UserID
        // GET api/<controller>/5
        [HttpGet]
        public IHttpActionResult GetByUser(string Userid)
        {
            var Oder_User = _context.Oder.Where(a => a.UserId == Userid).ToList();
            if (Userid == null)
            {
                return NotFound();
            }
            return (IHttpActionResult) Ok(Oder_User);
        }
        [HttpGet]
        public IHttpActionResult GetBySeller(string Sellerid)
        {
            var Oder_Seller = _context.Oder.Where(a => a.SellerId == Sellerid).ToList();
            if (Sellerid == null)
            {
                return NotFound();
            }
            return (IHttpActionResult)Ok(Oder_Seller);
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