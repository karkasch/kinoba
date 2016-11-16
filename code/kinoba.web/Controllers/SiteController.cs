using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace kinoba.web.Controllers
{
    public class SiteController : Controller
    {
        // GET: Site
        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("", "");
        }
    }
}
