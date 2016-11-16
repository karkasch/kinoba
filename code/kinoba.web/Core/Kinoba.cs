using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace kinoba.web.Core
{
    public class Kinoba
    {
        private static JsonSerializerSettings _jsonSerializerSettings;

        static Kinoba()
        {
            _jsonSerializerSettings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        internal static void Authenticate(UserData userData)
        {
            //FormsAuthentication.SetAuthCookie(userId.ToString(), false);

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1,
                userData.UserId.ToString(),
                DateTime.UtcNow,
                DateTime.UtcNow.AddDays(30), true, userData.ToString());
            string encrypted = FormsAuthentication.Encrypt(ticket);
            HttpCookie cook = new HttpCookie("UrlAuthrz", encrypted);
            HttpContext.Current.Response.Cookies.Add(cook);

            FormsAuthentication.SetAuthCookie(userData.UserId.ToString(), true);


            HttpContext.Current.Session["userName"] = userData.UserName;
        }

        public static bool Authenticated
        {
            get
            {
                return HttpContext.Current.User.Identity.IsAuthenticated;
            }
        }

        public static int UserId
        {
            get
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                    return int.Parse(HttpContext.Current.User.Identity.Name);

                return -1;
            }
        }

        public static string Serialize(object value)
        {
            return JsonConvert.SerializeObject(value, _jsonSerializerSettings);
        }
    }
}