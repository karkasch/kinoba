using kinoba.business.Domain;
using kinoba.business.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kinoba.business.Managers
{
    public class SpecialistManager
    {
        public Specialist GetCreatedByUserId(int userId)
        {
            using (var db = new kinobaEntities())
            {
                var spec = db.Specialists.FirstOrDefault(a => a.UserId == userId);
                if (spec == null)
                {
                    spec = new Specialist()
                    {
                        UserId = userId,
                        Slug = KinoCore.UniqueCode(),
                        AddedDate = DateTime.UtcNow,
                    };
                    db.Specialists.Add(spec);

                    db.SaveChanges();

                    return spec;
                }

                return spec;
            }
        }

        public Specialist Get(int specId, bool full = false)
        {
            using (var db = new kinobaEntities())
            {
                if (full)
                    return db.Specialists
                        .Include("SpecialistProfessionLinks")
                        .Include("SpecialistSchoolLinks")
                        .Include("SpecialistPhotos")
                        .Include("SpecialistMedias")
                        .FirstOrDefault(a => a.Id == specId);
                else
                    return db.Specialists.FirstOrDefault(a => a.Id == specId);
            }
        }

        public Specialist Get(string slug, bool full = false)
        {
            using (var db = new kinobaEntities())
            {
                if (full)
                    return db.Specialists
                        .Include("SpecialistProfessionLinks")
                        .Include("SpecialistSchoolLinks")
                        .Include("SpecialistPhotos")
                        .Include("SpecialistMedias")
                        .FirstOrDefault(a => a.Slug == slug);
                else
                    return db.Specialists.FirstOrDefault(a => a.Slug == slug);
            }
        }

        public Specialist Save(Specialist spec)
        {
            // checks
            //if (spec.CityId <= 0)
            //    throw new KinobaseException("Spec CityId");

            //if (spec.Id > 0) {
            //    spec.ModifiedDate = DateTime.UtcNow;
            //}
            //else {
            //    spec.Slug = KinoCore.UniqueCode();
            //    spec.AddedDate = DateTime.UtcNow;
            //}

            

            //spec.SpecialistCities.Clear();
            //spec.SpecialistCities.Add(new SpecialistCity() {
            //    Id = spec.CityId
            //})

            try
            {
                using (var db = new kinobaEntities())
                {
                    if (string.IsNullOrWhiteSpace(spec.Slug))
                    {
                        spec.Slug = KinoCore.UniqueCode();
                        spec.AddedDate = DateTime.UtcNow;
                        db.Specialists.Add(spec);
                    }
                    else
                    {
                        spec.ModifiedDate = DateTime.UtcNow;
                        if (spec.CityId == 0)
                            spec.CityId = null;

                        var orig = db.Specialists.First(a => a.Slug == spec.Slug);
                        spec.Id = orig.Id;
                        spec.UserId = orig.UserId;
                        spec.AddedDate = orig.AddedDate;
                        db.Entry(orig).CurrentValues.SetValues(spec);
                    }

                    db.SaveChanges();


                    var plinks = db.SpecialistProfessionLinks.Where(a => a.SpecialistId == spec.Id);
                    db.SpecialistProfessionLinks.RemoveRange(plinks);
                    spec.ProfessionIds.ForEach(id =>
                    {
                        db.SpecialistProfessionLinks.Add(new SpecialistProfessionLink() { ProfessionId = id, SpecialistId = spec.Id });
                    });

                    var schoolLinks = db.SpecialistSchoolLinks.Where(a => a.SpecialistId == spec.Id);
                    db.SpecialistSchoolLinks.RemoveRange(schoolLinks);
                    spec.SchoolIds.ForEach(id =>
                    {
                        db.SpecialistSchoolLinks.Add(new SpecialistSchoolLink() { SchoolId = id, SpecialistId = spec.Id });
                    });


                    // update photos

                    List<SpecialistPhoto> currentPhotos;

                    if (spec.SpecialistPhotos != null && spec.SpecialistPhotos.Any()) {
                        // TODO
                        var newPhotoIds = spec.SpecialistPhotos.Select(a => a.Id);
                        currentPhotos = db.SpecialistPhotos.Where(a => a.SpecialistId == spec.Id && !newPhotoIds.Contains(a.Id)).ToList();
                    }
                    else
                    {
                        currentPhotos = db.SpecialistPhotos.Where(a => a.SpecialistId == spec.Id).ToList();
                    }
                    
                    if (currentPhotos.Any())
                    {
                        foreach (var photo in currentPhotos)
                        {
                            var filePath = Kimages.GetPhotoPath(spec.Id, photo.FileName);
                            // TODO: delete a file
                            if (File.Exists(filePath))
                                File.Delete(filePath);
                        }
                    }
                    db.SpecialistPhotos.RemoveRange(currentPhotos);
                    //if (spec.SpecialistPhotos != null)
                    //{
                    //    foreach (var photo in spec.SpecialistPhotos)
                    //    {
                    //        db.SpecialistPhotos.Add(photo);
                    //    }
                    //}


                    // update videos

                    var mediaLinks = db.SpecialistMedias.Where(a => a.SpecialistId == spec.Id);
                    db.SpecialistMedias.RemoveRange(mediaLinks);
                    foreach (var media in spec.SpecialistMedias)
                    {
                        media.SpecialistId = spec.Id;
                        db.SpecialistMedias.Add(media);
                    }

                    db.SaveChanges();

                    // clear for serialization
                    //spec.SpecialistCities = null;
                    spec.SpecialistProfessionLinks = null;
                    spec.SpecialistSchoolLinks = null;
                    foreach (var media in spec.SpecialistMedias)
                    {
                        media.Specialist = null;
                    }

                    return spec;
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var err in ex.EntityValidationErrors)
                {
                    var x = err.ToString();
                    
                }
                return null;
            }
            catch (DbUpdateException ex)
            {
                //foreach (var err in ex.EntityValidationErrors)
                //{
                //    var x = err.ToString();

                //}
                return null;
            }
           
        }

        public SearchResult<Specialist> Search(Domain.SpecialistSearchParameters search)
        {
            using (var db = new kinobaEntities())
            {
                db.Configuration.ProxyCreationEnabled = false;

                var specs = db.Specialists
                        .Include("SpecialistProfessionLinks")
                        //.Include("Professions")
                        .Include("SpecialistSchoolLinks")
                        .Include("SpecialistPhotos")
                        .Include("SpecialistMedias")
                        .Include("City")
                        .Where(a => a.Id > 0);

                if (search.Sex > 0)
                {
                    specs = specs.Where(a => a.Sex == search.Sex);
                }

                if (search.CityId > 0)
                {
                    specs = specs.Where(a => a.CityId == search.CityId);
                }

                if (search.ProfessionIds != null && search.ProfessionIds.Any())
                    specs = specs.Where(a => a.SpecialistProfessionLinks.Any(b => search.ProfessionIds.Contains(b.ProfessionId)));

                if (search.AgeFrom > 0)
                {
                    var ageFrom = DateTime.UtcNow.AddYears(-search.AgeFrom);
                    specs = specs.Where(a => a.BirthDate <= ageFrom);
                }

                if (search.AgeTo > 0)
                {
                    var ageTo = DateTime.UtcNow.AddYears(-search.AgeTo);
                    specs = specs.Where(a => a.BirthDate >= ageTo);
                }

                if (!string.IsNullOrWhiteSpace(search.FirstName))
                {
                    if (search.FirstName.Contains("*"))
                    {
                        search.FirstName = search.FirstName.Replace("*", "");
                        specs = specs.Where(a => a.FirstName.Contains(search.FirstName));
                    }
                    else
                        specs = specs.Where(a => a.FirstName == search.FirstName);
                }

                if (!string.IsNullOrWhiteSpace(search.LastName))
                {
                    if (search.LastName.Contains("*"))
                    {
                        search.LastName = search.LastName.Replace("*", "");
                        specs = specs.Where(a => a.LastName.Contains(search.LastName));
                    }
                    else
                        specs = specs.Where(a => a.LastName == search.LastName);
                }

                var skip = (search.PageNum - 1) * search.PageSize;
                var take = search.PageSize;

                var total = specs.Count();
                specs = specs.OrderByDescending(a => a.ModifiedDate).ThenByDescending(a => a.AddedDate).Skip(skip).Take(take);

                var data = specs.ToList();
                
                // clear
                //foreach (var spec in specs)
                //{
                //    //spec.SpecialistCities = null;
                //    spec.SpecialistProfessionLinks = null;
                //    spec.SpecialistSchoolLinks = null;
                //    spec.User = null;
                //}

                data.ForEach(a =>
                {
                    if (a.City != null)
                        a.City.Specialists = null;
                });

                return new SearchResult<Specialist>() {
                    Total = total,
                    Data = data
                };
            }

        }
    }
}
