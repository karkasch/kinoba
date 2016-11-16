using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kinoba.business
{
    public partial class SpecialistPhoto
    {
        [NotMapped]
        public string Url { get; set; }
        
    }
}
