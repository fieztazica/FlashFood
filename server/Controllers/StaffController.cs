using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace server.Controllers
{
    [Authorize(Roles = "Admin, Manager, Employee")]
    public class StaffController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }

            return View();
        }
    }
}