using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kinoba.business.Exceptions
{
    public class KinobaseException : ApplicationException
    {
        public KinobaseException(string message)
            : base(message)
        {

        }
    }
}
