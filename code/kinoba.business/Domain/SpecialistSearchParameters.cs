using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kinoba.business.Domain
{
    public class SpecialistSearchParameters
    {
        public SpecialistSearchParameters()
        {
            PageNum = 1;
            PageSize = 20;
        }

        public int PageNum { get; set; }
        public int PageSize { get; set; }
        public int CityId { get; set; }
        public List<int> ProfessionIds { get; set; }
        public int AgeFrom { get; set; }
        public int AgeTo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int Sex { get; set; }
    }
}
