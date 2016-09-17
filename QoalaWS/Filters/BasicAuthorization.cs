using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace QoalaWS.Filters
{
    public class BasicAuthorization : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            this.Logger().Debug(
                new
                {
                    StatusCode = actionExecutedContext.Response.StatusCode,
                    ReasonPhrase = actionExecutedContext.Response.ReasonPhrase,
                    IsSuccessStatusCode = actionExecutedContext.Response.IsSuccessStatusCode
                });
        }
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            this.Logger().Debug(
                new
                {
                    Method = actionContext.Request.Method,
                    RequestUri = actionContext.Request.RequestUri.ToString(),
                    Accept = actionContext.Request.Headers.Accept
                });
            if (actionContext.Request.RequestUri.DnsSafeHost == "localhost")
            {
                return;
            }

            var request = HttpContext.Current.Request;
            var authHeader = request.Headers["Authorization"];
            if (authHeader == null)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            } else {
                var authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);

                // RFC 2617 sec 1.2, "scheme" name is case-insensitive
                if (authHeaderVal.Scheme.Equals("Token",
                        StringComparison.OrdinalIgnoreCase) &&
                    authHeaderVal.Parameter != null)
                {
                    //Need check if user has this control access
                    DAO.ACCESSCONTROL ac = DAO.ACCESSCONTROL.find(new DAO.QoalaEntities(), authHeaderVal.Parameter);
                    if (ac == null)
                        actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}