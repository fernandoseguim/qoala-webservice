using System.Net;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Web.Http.Controllers;

namespace QoalaWS.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ModelState.IsValid)
                return;

            actionContext.Response = actionContext.Request.CreateErrorResponse(
                HttpStatusCode.BadRequest, actionContext.ModelState
            );            
        }
    }
}