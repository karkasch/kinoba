using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kinoba.business
{
    public partial class Specialist
    {
        [NotMapped]
        public List<int> ProfessionIds { get; set; }
        [NotMapped]
        public List<int> SchoolIds { get; set; }
        [NotMapped]
        public int Age { get; set; }
        [NotMapped]
        public string EducationStr { get; set; }
        [NotMapped]
        public string ProfessionStr { get; set; }
        [NotMapped]
        public string HairColorStr { get; set; }
        [NotMapped]
        public string HairLengthStr { get; set; }
        [NotMapped]
        public string EyeColorStr { get; set; }
    }
}
