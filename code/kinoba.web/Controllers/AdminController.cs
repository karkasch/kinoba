using kinoba.business.Managers;
using kinoba.web.Core.Atributes;
using kinoba.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kinoba.web.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [Kauth]
        public ActionResult Index()
        {
            return View(new KinobaBaseModel());
        }

        [Kauth]
        public ActionResult Users()
        {
            var model = new AdminUsersModel();
            var userManager = new UserManager();
            model.Users = userManager.LoadUsers(1, 20);

            return View(model);
        }
    }
}