using QoalaWS.DAO;
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
        public IHttpActionResult Index()
        {
            var set = new Dictionary<String, String>();
            set.Add("running", "ok");
            return Json(set);
        }

        [HttpPost]
        public IHttpActionResult Echo([FromBody]Object[] obj)
        {
            return Json(obj);
        }

        [HttpPost]
        public IHttpActionResult Echo([FromUri] String tipo)
        {
            if(tipo.Equals("USER"))
                return Json(new USER());
            return NotFound();
        }


    }
}
