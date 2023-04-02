using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using server.Models;
using server.ViewModels;

namespace server.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext db;

        public OrdersController()
        {
            db = new ApplicationDbContext();
        }

        // GET: Orders
        public ActionResult Index(string searchString)
        {
            var orders = db.Orders.OrderByDescending(x => x.OrderAt).Where(x => DbFunctions.TruncateTime(x.OrderAt) == DbFunctions.TruncateTime(DateTime.Now)).Select(x => x);
            if (!String.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(s => s.Status.Contains(searchString));
            }
            return View(orders.ToList());
        }

        public ActionResult List(string searchString)
        {
            var orders = db.Orders.OrderByDescending(x => x.OrderAt).Select(x => x);
            if (!String.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(s => s.Status.Contains(searchString));
            }
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }

            var orderItemModels = db.OrderItems.Where(a => a.OrderId == order.Id).ToList();
            List<OrderItemViewModel> orderItems = new List<OrderItemViewModel>();
            foreach (var oi in orderItemModels)
            {
                oi.Meal = db.Meals.FirstOrDefault(m => m.Id == oi.MealId);
                orderItems.Add(OrderItemViewModel.FromOrderItem(oi));
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", order.UserId);
            ViewBag.OrderItems = orderItems;
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,OrderAt,Total_money,Paid,Change,SellerId,UserId,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                if (order.Paid != null)
                    order.PaidAt = DateTime.Now;
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", order.UserId);
            return View(order);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,OrderAt,PaidAt,Total_money,Paid,Change,SellerId,UserId,Status")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", order.UserId);
            return View(order);
        }

        // GET: Orders/Edit/5


        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
