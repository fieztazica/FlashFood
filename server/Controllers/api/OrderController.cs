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
        public async Task<IHttpActionResult> Post(OrderBindingModel oder)
        {
            var Total_money = _context.cartitem.Where(a => a.UserId == oder.UserId).ToList();
            double money = 0;
            foreach (var t in Total_money)
            {
                money += t.Money();
            }
            var Oder = new Order()
            {
                UserId = oder.UserId,
                SellerId = oder.SellerId,
                Paid = oder.Paid,
                PaidAt = oder.PaidAt,
                Change = oder.Change,
                Total_money = money,
            };
            _context.Order.Add(Oder);
            _context.SaveChanges();
            string Href = "http://localhost:/api/OderItem/Post";
            Href += Oder.Id.ToString();
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(Href);
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    // process the response body
                }
                else
                {
                    // handle the error
                }
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