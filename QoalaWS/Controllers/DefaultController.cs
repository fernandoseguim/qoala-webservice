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
                Version = ass.GetName().Version,
                CreateTime = System.IO.File.GetCreationTimeUtc(ass.Location),
                LastWriteTime = System.IO.File.GetLastWriteTimeUtc(ass.Location),
                ExportedTypes = ass.ExportedTypes.Select(a => a.FullName),
            });
        }
    }
}
