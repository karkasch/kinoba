using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace kinoba.web.Core
{
    public class KinobaException : ApplicationException
    {
        public KinobaException(string message) : base(message)
        {

        }
    }
}