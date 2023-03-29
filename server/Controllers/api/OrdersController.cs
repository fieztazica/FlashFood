using server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace server.Controllers.api
{
    public class OrdersController : ApiController
    {
        private readonly ApplicationDbContext _context;


        public OrdersController()
        {
            _context = new ApplicationDbContext();
        }
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var Order = _context.Orders.ToList();
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
            var Order_User = _context.Orders.Where(a => a.UserId == Userid).ToList();
            if (Userid == null)
            {
                return NotFound();
            }
            return Ok(Order_User);
        }
        [HttpGet]
        public IHttpActionResult GetBySeller(string Sellerid)
        {
            var Order_Seller = _context.Orders.Where(a => a.SellerId == Sellerid).ToList();
            if (Sellerid == null)
            {
                return NotFound();
            }
            return Ok(Order_Seller);
        }
        // POST api/<controller>
        public IHttpActionResult Post(OrderBindingModel o)
        {
            var Total_money = _context.Cartitems.Where(a => a.UserId == o.UserId).ToList();
            double money = 0;
            foreach (var t in Total_money)
            {
                money += t.Money();
            }
            var Change = o.Paid - money;
            Order order = new Order()
            {
                UserId = o.UserId,
                SellerId = o.SellerId,
                PaidAt = o.PaidAt,
                Change = Change,
                Total_money = money,

            };
            _context.Orders.Add(order);
            _context.SaveChanges();
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