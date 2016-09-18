using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using QoalaWS.DAO;
using QoalaWS.Filters;
using Newtonsoft.Json.Linq;

namespace QoalaWS.Controllers
{
    public class UsersController : ApiController
    {
        private QoalaEntities db = new QoalaEntities();

        [Route("users/{id}")]
        [BasicAuthorization]
        public IHttpActionResult Get(decimal id)
        {
            User user = DAO.User.findById(db, id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(
                new
                {
                    EMAIL = user.EMAIL,
                    NAME = user.NAME,
                    PERMISSION = user.PERMISSION,
                    CREATED_AT = user.CREATED_AT
                }
            );
        }
        
        [HttpPut]
        [Route("users/{id}")]
        [BasicAuthorization]
        public IHttpActionResult Update(decimal id, JObject obj)
        {

            User user = DAO.User.findById(db, id);

            if (user == null)
                return NotFound();

            if (!obj.HasValues)
            {
                return BadRequest("Object sent was received invalid!");
            }

            string val = null;
            val = obj.Value<string>("EMAIL");
            if (val!=null) { user.EMAIL = val; }

            val = obj.Value<String>("NAME");
            if (val != null) { user.NAME = val; }

            val = obj.Value<String>("PASSWORD");
            if (val != null) { user.PASSWORD = val; }

            val = obj.Value<String>("PERMISSION");
            if (val != null) { user.PERMISSION = byte.Parse(val); }

            user.Update(db);

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpDelete]
        [Route("users/{id}")]
        [BasicAuthorization]
        public IHttpActionResult Delete(decimal id)
        {
            User user = DAO.User.findById(db, id);
            if (user == null)
                return NotFound();

            user.Delete(db);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}