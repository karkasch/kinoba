using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kinoba.web.Core
{
    public class UserData
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Slug { get; set; }
        
        public override string ToString()
        {
            return string.Format("{0}:{1}", UserId, UserName);
        }
    }
}