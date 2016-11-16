using kinoba.business.Domain;
using kinoba.business.Managers;
using kinoba.web.Core;
using kinoba.web.Core.Atributes;
using kinoba.web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kinoba.web.Controllers
{
    [HandleError]
    public class SpecController : Controller
    {
        // GET: Spec
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //public ActionResult Create()
        //{
        //    var model = new SpecEditModel();
        //    return View(model);
        //}

        //public ActionResult Edit(int specId)
        //{
        //    var model = new SpecEditModel(specId);
        //    return View(model);
        //}

        [Kauth]
        public ActionResult Index()
        {
            var manager = new SpecialistManager();
            var user = manager.GetCreatedByUserId(Kinoba.UserId);
            
            return RedirectToAction("edit", "casting", new { id = user.Slug });
        }

        //[Kauth]
        //public ActionResult Edit(string slug)
        //{

        //    var model = new SpecEditModel(slug);
        //    return View(model);
        //}

        [ChildActionOnly]
        public ActionResult RecentAdded()
        {
            //var model = new KinobaBaseModel();

            var manager = new SpecialistManager();
            var searchParams = new SpecialistSearchParameters()
            {
                PageSize = 20,
                PageNum = 1
            };
            var result = manager.Search(searchParams);

            return View(result);
        }
    }
}