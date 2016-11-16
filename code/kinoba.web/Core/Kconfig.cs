using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace kinoba.web.Core
{
    public class Kconfig
    {
        public static string FacebookAppId { get; set; }

        public static string FacebookAppSecret { get; set; }

        public static string VkontakteAppId { get; set; }

        public static string VkontakteAppSecret { get; set; }

        static Kconfig()
        {
            VkontakteAppId = ConfigurationManager.AppSettings["VkontakteAppId"];
            VkontakteAppSecret = ConfigurationManager.AppSettings["VkontakteAppSecret"];

            FacebookAppId = ConfigurationManager.AppSettings["FacebookAppId"];
            FacebookAppSecret = ConfigurationManager.AppSettings["FacebookAppSecret"];
        }
    }
}