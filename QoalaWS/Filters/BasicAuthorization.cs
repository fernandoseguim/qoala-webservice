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
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
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
                    DAO.AccessControl ac = DAO.AccessControl.find(new DAO.QoalaEntities(), authHeaderVal.Parameter);
                    if (ac == null)
                        actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}