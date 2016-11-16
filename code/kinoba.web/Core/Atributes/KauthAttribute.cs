using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace kinoba.web.Core.Atributes
{
    public class KauthAttribute : ActionFilterAttribute, IActionFilter
    {
        private bool _isAjax = false;

        public KauthAttribute()
        {

        }

        public KauthAttribute(bool isAjax = false)
        {

        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!Kinoba.Authenticated)
            {
                if (_isAjax)
                    filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.Unauthorized, "Not authorazed");
                else
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "site" }, { "action", "login" } });
            }
        }
    }
}