using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using kinoba.business;
using kinoba.business.Managers;

namespace kinoba.web.Models
{
    public class SpecEditModel
    {
        public SpecEditModel()
        {

        }

        public SpecEditModel(int specId)
        {
            var manager = new SpecialistManager();
            Specialist = manager.Get(specId);
        }

        public SpecEditModel(string id)
        {
            var manager = new SpecialistManager();
            Specialist = manager.Get(id);
        }

        public Specialist Specialist { get; set; }
    }
}