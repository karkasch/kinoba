
using kinoba.business;
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
    public class CastingController : Controller
    {
        [Kauth]
        public ActionResult Create()
        {
            var model = new CastingModel();
            model.Specialist = new Specialist();
            model.LoadLists();

            return View("Edit", model);
        }

        [Kauth]
        public ActionResult Edit(string id)
        {
            var specialistManager = new SpecialistManager();

            var model = new CastingModel();
            model.Specialist = specialistManager.Get(id, true);

            if (model.Specialist == null)
                throw new KinobaException("Специалист с таким ID не существует");

            model.LoadLists();

            model.Specialist.SpecialistPhotos = model.Specialist.SpecialistPhotos.Select(a => new SpecialistPhoto()
            {
                Id = a.Id,
                Url = Kimages.GetPhotoUrl(model.Specialist.Id, a.FileName)
            }).ToList();

            model.Specialist.SpecialistMedias = model.Specialist.SpecialistMedias.Select(a => new SpecialistMedia()
            {
                Id = a.Id,
                EmbedCode = a.EmbedCode,
                Link = a.Link,
                MediaType = a.MediaType
            }).ToList();

            return View(model);
        }
        
    }
}