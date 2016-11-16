using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using kinoba.business;
using kinoba.web.Core;
using System.Web.Security;
using kinoba.web.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using kinoba.business.Managers;

namespace kinoba.web.Controllers
{
    public class AuthController : Controller
    {
        // facebook
        #region Facebook

        public ActionResult fbLogin()
        {
            // login user on facebook
            var resUrl = Url.Action("fbloginres", "auth", null, Request.Url.Scheme); // "http://localhost:49625/auth/fbLoginRes";
            var url = string.Format("https://www.facebook.com/dialog/oauth?client_id={0}&response_type=code&redirect_uri={1}", Kconfig.FacebookAppId, resUrl);
            Response.Redirect(url);

            return null;
        }

        public ActionResult fbLoginRes()
        {
            var code = Request.QueryString["code"];

            if (string.IsNullOrWhiteSpace(code))
            {
                return RedirectToAction("cancel");
            }

            try
            {
                // get access token
                var resUrl = Url.Action("fbloginres", "auth", null, Request.Url.Scheme); // "http://localhost:49625/auth/fbLoginRes";
                var url = string.Format("oauth/access_token?client_id={0}&redirect_uri={1}&client_secret={2}&code={3}&scopes=public_profile,email",
                    Kconfig.FacebookAppId, resUrl, Kconfig.FacebookAppSecret, code);

                //var responseData = getResponseText("https://graph.facebook.com/", url).Result;
                var responseData = GetResponseString("https://graph.facebook.com/", url);

                var items = responseData.Split(new[] { '&' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                var accessToken = items.First(a => a.StartsWith("access_token")).Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries)[1];


                // get user info
                url = string.Format("me?access_token={0}", accessToken);

                //responseData = getResponseText("https://graph.facebook.com/", url).Result;
                //var fbUser = GetObject<FacebookUser>(responseData);

                var fbUser = GetResponse<FacebookUser>("https://graph.facebook.com/", url);

                // login register
                var manager = new UserManager();
                var user = manager.LoginRegisterFromSocial("fb", fbUser.id, fbUser.email, fbUser.first_name, fbUser.last_name);

                if (user.Id > 0)
                {
                    Session.Clear();
                    FormsAuthentication.SignOut();

                    Kinoba.Authenticate(new UserData() { UserId = user.Id, UserName = fbUser.name });

                    Session["userName"] = fbUser.name;

                    return RedirectToAction("", "");
                }

            }
            catch (Exception ex)
            {
                Klog.Err(ex.ToString());
            }

            return RedirectToAction("cancel");
        }

        public ActionResult cancel()
        {
            var errorModel = new KinobaBaseModel();
            ViewBag.Status = "Не получилось авторизоваться. Попробуйте еще раз<br />";
            return View("cancel", errorModel);
        }

        #endregion

        #region VK

        public ActionResult vkLogin()
        {
            // login user on facebook
            var resUrl = Url.Action("vkloginres", "auth", null, Request.Url.Scheme); // "http://localhost:49625/auth/fbLoginRes";
            var url = string.Format("http://oauth.vk.com/authorize?client_id={0}&redirect_uri={1}&response_type=code", Kconfig.VkontakteAppId, resUrl);
            Response.Redirect(url);

            return null;
        }

        public ActionResult vkLoginRes()
        {
            var code = Request.QueryString["code"];

            if (string.IsNullOrWhiteSpace(code))
            {
                return RedirectToAction("cancel");
            }

            try
            {
                // get access token
                var resUrl = Url.Action("vkloginres", "auth", null, Request.Url.Scheme); // "http://localhost:49625/auth/fbLoginRes";
                var url = string.Format("access_token?client_id={0}&client_secret={1}&code={2}&redirect_uri={3}",
                    Kconfig.VkontakteAppId, Kconfig.VkontakteAppSecret, code, resUrl);

                // {"access_token":"79db23248be4d774c229f0f8d38eeb93b125403b7ced1f2fbadb478b2a749ea6db99739ed0bd4e9732bad","expires_in":86393,"user_id":2590656}
                var vkToken = GetResponse<VkontakteToken>("https://oauth.vk.com/", url);

                if (string.IsNullOrWhiteSpace(vkToken.user_id))
                {
                    Klog.Err("vkToken.user_id empty");
                    return RedirectToAction("cancel");
                }

                // get user data
                url = string.Format("method/users.get?uids={0}&fields=uid,email,first_name,last_name,nickname,city,country&access_token={1}",
                    vkToken.user_id, vkToken.access_token);
                //responseData = getResponseText(url);
                //"{\"response\":[{\"uid\":2590656,\"first_name\":\"Аркадий\",\"last_name\":\"Турченко\",\"nickname\":\"\",\"city\":\"2220\",\"country\":\"139\"}]}"
                var vkUserResponse = GetResponse<VkontakeUserResponse>("https://api.vk.com/", url);  // GetObject<VkontakeUserResponse>(responseData);
                var vkUser = vkUserResponse.response[0];

                // login register
                var manager = new UserManager();
                var user = manager.LoginRegisterFromSocial("vk", vkUser.uid, null, vkUser.first_name, vkUser.last_name);

                if (user.Id > 0)
                {
                    Session.Clear();
                    FormsAuthentication.SignOut();

                    var userName = (vkUser.first_name + " " + vkUser.last_name).Trim();
                    Kinoba.Authenticate(new UserData() { UserId = user.Id, UserName = userName });

                    Session["userName"] = vkUser.first_name + " " + vkUser.last_name;

                    return RedirectToAction("", "");
                }

            }
            catch (Exception ex)
            {
                Klog.Err(ex.ToString());
                throw;
            }


            return RedirectToAction("cancel");
        }

        #endregion

        private async Task<string> getResponseText(string baseAddress, string url)
        {
            var responseData = "";

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseAddress);
                using (var responseMessage = await client.GetAsync(url))
                {
                    responseData = await responseMessage.Content.ReadAsStringAsync();
                }
            }

            //var responseData = "";
            //var request = WebRequest.Create(url);
            //using (var response = request.GetResponse())
            //using (var streamReader = new StreamReader(response.GetResponseStream()))
            //{
            //    responseData = streamReader.ReadToEnd();
            //}

            return responseData;
        }

        private T GetResponse<T>(string baseAddress, string url) where T : class
        {
            url = baseAddress + url;

            var responseData = "";
            var request = WebRequest.Create(url);
            using (var response = request.GetResponse())
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                responseData = streamReader.ReadToEnd();
            }

            if (typeof(T).Name == "String")
                return responseData as T;
            
            return GetObject<T>(responseData);


            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri(baseAddress);
            //    using (var responseMessage = await client.GetStreamAsync(url))
            //    {
                    
            //        return responseMessage.rContent.ReadAsAsync<T>();
            //    }
            //}

            //return Task.FromResult<T>(null);
        }

        private T GetObject<T>(string json)
        {
            var ser = new System.Web.Script.Serialization.JavaScriptSerializer();
            var objClass = ser.Deserialize<T>(json);
            return objClass;
        }

        private string GetResponseString(string baseAddress, string url)
        {
            url = baseAddress + url;

            var responseData = "";
            var request = WebRequest.Create(url);
            using (var response = request.GetResponse())
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                responseData = streamReader.ReadToEnd();
            }

            return responseData;
        }

    }

    /* {"id":"10206106497397247",
     * "first_name":"Arkady",
     * "gender":"male",
     * "last_name":"Turchenko",
     * "link":"https:\/\/www.facebook.com\/app_scoped_user_id\/10206106497397247\/",
     * "locale":"en_US",
     * "name":"Arkady Turchenko",
     * "timezone":1,
     * "updated_time":"2014-07-30T05:11:15+0000",
     * "verified":true}
     * 
     {"id":"10206106497397247",
     * "email":"nutzer12\u0040googlemail.com",
     * "first_name":"Arkady",
     * "gender":"male",
     * "last_name":"Turchenko",
     * "link":"https:\/\/www.facebook.com\/app_scoped_user_id\/10206106497397247\/",
     * "locale":"en_US",
     * "name":"Arkady Turchenko",
     * "timezone":1,"updated_time":"2014-07-30T05:11:15+0000","verified":true}
     */
    public class FacebookUser
    {
        public string id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string link { get; set; }
    }

    public class VkontakteToken
    {
        public string access_token { get; set; }
        public string user_id { get; set; }
    }

    public class VkontakeUserResponse
    {
        public VkontakteUser[] response { get; set; }
    }

    public class VkontakteUser
    {
        public string uid { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
    }
}