using kinoba.business;
using kinoba.business.Managers;
using kinoba.web.api.Atributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace kinoba.web.api.Controllers
{
    public class SpecialistController : ApiController
    {
        // GET: api/Specialist
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Specialist/5
        public string Get(int id)
        {
            return "value";
        }

        //[Kauth]
        public Specialist Post([FromBody]Specialist specialist)
        {
            var specialistManager = new SpecialistManager();
            specialistManager.Save(specialist);

            return specialist;
        }

        // PUT: api/Specialist/5
        public void Put(int id, [FromBody]Specialist specialist)
        {
        }

        // DELETE: api/Specialist/5
        public void Delete(int id)
        {
        }
    }
}
