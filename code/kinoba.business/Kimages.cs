using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace kinoba.business
{
    public class Kimages
    {
        public static string GetUploadPath(int specialistId)
        {
            var basePath = HttpContext.Current.Server.MapPath("~/images/s");
            var path = Path.Combine(basePath, GetSpecialistPath(specialistId));
            return path;
        }

        public static string GetUploadPathFile(int specialistId, string fileName)
        {
            var path = GetUploadPath(specialistId);
            path = Path.Combine(path, string.Format("{0}{1}", KinoCore.UniqueCode(), Path.GetExtension(fileName)));
            return path;
        }

        public static string GetPhotoPath(int specialistId, string fileName)
        {
            var path = GetUploadPath(specialistId);
            path = Path.Combine(path, fileName);
            return path;
        }

        public static string GetSpecialistPath(int specialistId)
        {
            var path = (specialistId / 1000).ToString("0000");
            return string.Format("{0}\\{1}", path, specialistId);
        }

        public static string GetPhotoUrl(int specialistId, string fileName)
        {
            var path = GetSpecialistPath(specialistId).Replace("\\", "/");

            return string.Format("/images/s/{0}/{1}", path, fileName);
        }
    }
}
