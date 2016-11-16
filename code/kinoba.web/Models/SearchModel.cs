using kinoba.business.Domain;
using kinoba.business.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kinoba.web.Models
{
    public class SearchModel
    {
        public void LoadLists()
        {
            var listsManager = new ListsManager();

            Professions = listsManager.LoadProfessions();
            Schools = listsManager.LoadSchools();
            Cities = listsManager.LoadCities();
            EyeColors = ListsManager.EyeColors;
            HairTypes = listsManager.LoadHairTypes();
        }

        public List<business.Profession> Professions { get; set; }

        public List<business.School> Schools { get; set; }

        public List<business.City> Cities { get; set; }

        public List<business.Domain.ListItem> EyeColors { get; set; }

        public List<business.Domain.ListItem> HairTypes { get; set; }
    }
}