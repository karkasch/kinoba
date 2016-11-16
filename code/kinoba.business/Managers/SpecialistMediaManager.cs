using kinoba.business.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kinoba.business.Managers
{
    public class SpecialistMediaManager
    {
        public int GetImagesCount(int specialistId)
        {
            using (var db = new kinobaEntities())
            {
                var mediaType = (int)SpecialistMediaTypes.Photo;

                var result = db.SpecialistMedias
                    .Where(a => a.SpecialistId == specialistId && a.MediaType == mediaType)
                    .Count();
                return result;
            }
        }

        public SpecialistPhoto AddPhoto(int specialistId, string fileName, string url)
        {
            using (var db = new kinobaEntities())
            {
                var media = new SpecialistPhoto()
                {
                    FileName = fileName,
                    SpecialistId = specialistId,
                    Url = url
                };

                db.SpecialistPhotos.Add(media);

                db.SaveChanges();
                return media;
            }
        }
    }
}
