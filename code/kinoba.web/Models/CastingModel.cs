using kinoba.business;
using kinoba.business.Domain;
using kinoba.business.Managers;
using kinoba.web.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kinoba.web.Models
{
    public class CastingModel
    {
        public Specialist Specialist { get; set; }
        public List<Profession> Professions { get; set; }
        public List<School> Schools { get; set; }
        public List<City> Cities { get; set; }
        public List<ListItem> EyeColors { get; set; }
        public List<ListItem> HairTypes { get; set; }

        public string ProfessionsView
        {
            get
            {
                return Kinoba.Serialize(Professions.Select(a => new { a.Id, a.Name }));
            }
        }

        public string SchoolsView
        {
            get
            {
                return Kinoba.Serialize(Schools.Select(a => new { a.Id, a.Name }));
            }
        }

        public string CitiesView
        {
            get
            {
                return Kinoba.Serialize(Cities.Select(a => new { a.Id, a.Name }));
            }
        }

        public string EyeColorView
        {
            get
            {
                return Kinoba.Serialize(EyeColors.Select(a => new { Id = a.Id,  a.Name }));
            }
        }

        public string HairTypeView
        {
            get
            {
                return Kinoba.Serialize(HairTypes.Select(a => new { Id = a.Id, a.Name }));
            }
        }

        public void LoadLists()
        {
            Professions = ListsManager.Professions;
            Schools = ListsManager.Schools;
            Cities = ListsManager.Cities;
            EyeColors = ListsManager.EyeColors;
            HairTypes = ListsManager.HairTypes;
        }

        //public string SpecialistSlug { get; set; }
    }
}