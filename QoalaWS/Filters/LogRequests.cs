using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace QoalaWS.Filters
{
    internal class LogRequests : ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            this.Logger().Debug(new
            {
                StatusCode = actionExecutedContext.Response.StatusCode,
                ReasonPhrase = actionExecutedContext.Response.ReasonPhrase,
                IsSuccessStatusCode = actionExecutedContext.Response.IsSuccessStatusCode,
                actionContext = actionExecutedContext
            });
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            this.Logger().Debug(new
            {
                StatusCode = actionContext.Response.StatusCode,
                ReasonPhrase = actionContext.Response.ReasonPhrase,
                IsSuccessStatusCode = actionContext.Response.IsSuccessStatusCode,
                actionContext = actionContext
            });
        }
    }
}