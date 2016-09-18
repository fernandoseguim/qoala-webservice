using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace QoalaWS.Controllers
{
    public class DefaultController : ApiController
    {
        [HttpGet]
        [HttpPost]
        public IHttpActionResult Index()
        {
            var ass = System.Reflection.Assembly.GetExecutingAssembly();

            return Ok(new
            {
                version = ass.GetName().Version
            });
        }
    }
}
