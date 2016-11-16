using kinoba.web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace kinoba.web.api.Atributes
{
    public class KauthAttribute : ActionFilterAttribute, IActionFilter
    {
        public KauthAttribute()
        {

        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!Kinoba.Authenticated)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized)
                {
                    Content = new StringContent("Нет авторизации")
                };

                return;
            }
        }
    }
}