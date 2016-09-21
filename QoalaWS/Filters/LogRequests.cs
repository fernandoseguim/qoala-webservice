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
            if (actionExecutedContext.Response != null)
            {
                this.Logger().Debug(new
                {
                    StatusCode = actionExecutedContext.Response.StatusCode,
                    ReasonPhrase = actionExecutedContext.Response.ReasonPhrase,
                    IsSuccessStatusCode = actionExecutedContext.Response.IsSuccessStatusCode,
                    Response = actionExecutedContext.Response,
                });
            }

            if (actionExecutedContext.Exception != null)
            {
                this.Logger().Error(new
                {
                    URI = actionExecutedContext.Request.RequestUri,
                    Exception = actionExecutedContext.Exception,
                    ExceptionData = actionExecutedContext.Exception.Data,
                });
            }
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            this.Logger().Debug(new
            {
                Method = actionContext.Request.Method,
                RequestUri = actionContext.Request.RequestUri,
                Headers = actionContext.Request.Headers
            });
        }
    }
}