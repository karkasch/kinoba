using kinoba.web.Core.Atributes;
using kinoba.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kinoba.web.Controllers
{
    public class SearchController : Controller
    {
        [Kauth]
        public ActionResult Index()
        {
            var model = new SearchModel();
            model.LoadLists();
            return View(model);
        }
    }
}