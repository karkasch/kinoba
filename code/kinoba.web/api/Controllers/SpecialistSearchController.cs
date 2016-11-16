using kinoba.business;
using kinoba.business.Domain;
using kinoba.business.Managers;
using kinoba.web.api.Atributes;
using kinoba.web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace kinoba.web.api.Controllers
{
    public class SpecialistSearchController : ApiController
    {
        //[Kauth]
        public SearchResult<Specialist> Post([FromBody]SpecialistSearchParameters search)
        {
            var specialistManager = new SpecialistManager();
            var specialists = specialistManager.Search(search);

            // temp
            foreach (var spec in specialists.Data)
            {
                foreach (var photo in spec.SpecialistPhotos)
                {
                    if (string.IsNullOrWhiteSpace(photo.Url))
                    {
                        photo.Url = Kimages.GetPhotoUrl(spec.Id, photo.FileName);
                    }
                }

                spec.Age = GetAge(spec.BirthDate);

                if (spec.SpecialistSchoolLinks != null)
                    spec.EducationStr = GetEducation(spec.SpecialistSchoolLinks.Select(a => a.SchoolId).ToList());

                if (spec.SpecialistProfessionLinks != null)
                    spec.ProfessionStr = GetProfession(spec.SpecialistProfessionLinks.Select(a => a.ProfessionId).ToList());

                spec.HairColorStr = GetListName(ListsManager.HairColors, spec.HairColor);
                spec.HairLengthStr = GetListName(ListsManager.HairTypes, spec.HairLength);
                spec.EyeColorStr = GetListName(ListsManager.EyeColors, spec.EyeColor);
            }

            return specialists;
        }

        private int GetAge(DateTime? birthDate)
        {
            if (!birthDate.HasValue)
                return 0;

            return DateTime.UtcNow.Year - birthDate.Value.Year;
        }

        private string GetEducation(List<int> schoolIds)
        {
            if (schoolIds == null)
                return "";

            var result = new StringBuilder();

            foreach (var id in schoolIds)
            {
                var school = ListsManager.Schools.FirstOrDefault(a => a.Id == id);
                if (school != null)
                {
                    if (result.Length > 0)
                        result.Append(", ");

                    result.Append(school.Name);
                }
            }

            return result.ToString();
        }

        private string GetProfession(List<int> professionIds)
        {
            if (professionIds == null)
                return "";

            var result = new StringBuilder();

            foreach (var id in professionIds)
            {
                var prof = ListsManager.Professions.FirstOrDefault(a => a.Id == id);
                if (prof != null)
                {
                    if (result.Length > 0)
                        result.Append(", ");

                    result.Append(prof.Name);
                }
            }

            return result.ToString();
        }

        private string GetListName(List<ListItem> items, int? id)
        {
            if (!id.HasValue)
                return "";

            var result = new StringBuilder();

            var res = items.FirstOrDefault(a => a.Id == id.Value);
            if (res != null)
            {
                return res.Name;
            }

            return "";
        }
    }
}
