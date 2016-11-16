using kinoba.business.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kinoba.business.Managers
{
    public class ListsManager
    {
        static ListsManager()
        {
            var listsManager = new ListsManager();

            Cities = listsManager.LoadCities();
            Schools = listsManager.LoadSchools();
            Professions = listsManager.LoadProfessions();
            EyeColors = listsManager.LoadEyeColors();
            HairTypes = listsManager.LoadHairTypes();
            HairColors = listsManager.LoadHairColors();
        }

        public static List<City> Cities { get; set; }
        public static List<School> Schools { get; set; }
        public static List<Profession> Professions { get; set; }
        public static List<ListItem> EyeColors { get; set; }
        public static List<ListItem> HairTypes { get; set; }
        public static List<ListItem> HairColors { get; set; }

        public List<City> LoadCities()
        {
            using (var db = new kinobaEntities())
            {
                return db.Cities.OrderBy(a => a.Name).ToList();
            }
        }

        public List<School> LoadSchools()
        {
            using (var db = new kinobaEntities())
            {
                return db.Schools.OrderBy(a => a.Name).ToList();
            }
        }

        public List<Profession> LoadProfessions()
        {
            using (var db = new kinobaEntities())
            {
                return db.Professions.OrderBy(a => a.Name).ToList();
            }
        }

        public List<ListItem> LoadEyeColors()
        {
            return new List<ListItem>()
            {
                { new EyeColor() { Id = 1, Name = "Другое" } },
                { new EyeColor() { Id = 2, Name = "Карий" } },
                { new EyeColor() { Id = 3, Name = "Голубой" } },
                { new EyeColor() { Id = 4, Name = "Зеленый" } },
            };
        }

        public List<ListItem> LoadHairTypes()
        {
            return new List<ListItem>()
            {
                { new HairType() { Id = 1, Name = "Другое" } },
                { new HairType() { Id = 2, Name = "Короткие" } },
                { new HairType() { Id = 3, Name = "Средние" } },
                { new HairType() { Id = 4, Name = "Длинные" } },
            };
        }

        public List<ListItem> LoadHairColors()
        {
            return new List<ListItem>()
            {
                { new HairType() { Id = 1, Name = "Другое" } },
                { new HairType() { Id = 2, Name = "Шатен" } },
                { new HairType() { Id = 3, Name = "Брюнет" } },
                { new HairType() { Id = 4, Name = "Блондин" } },
                { new HairType() { Id = 5, Name = "Рыжий" } },
            };
        }
    }
}
