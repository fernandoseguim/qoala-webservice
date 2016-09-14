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

namespace QoalaWS.Controllers
{
    public class UsersController : ApiController
    {
        private QoalaEntities db = new QoalaEntities();

        [Route("users/{id}")]
        public IHttpActionResult Get(decimal id)
        {
            USER user = USER.findById(db, id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(
                new {
                    EMAIL = user.EMAIL,
                    NAME = user.NAME,
                    PERMISSION = user.PERMISSION,
                    CREATED_AT = user.CREATED_AT
                }
            );
       }


        //That procedure to update user is broken
        [HttpPut]
        [Route("users/{id}")]
        public IHttpActionResult Update(decimal id, USER user)
        {

            USER u = USER.findById(db, id);

            if (u == null)
                return NotFound();

            if (user.NAME == null)
                user.NAME = u.NAME;
            if (user.PASSWORD == null)
                user.PASSWORD = u.PASSWORD;
            if (user.EMAIL == null)
                user.EMAIL = u.EMAIL;
            if (!(user.PERMISSION > 0))
                user.PERMISSION = u.PERMISSION;
            
            user.ID_USER = id;
            user.Update(db);

            return StatusCode(HttpStatusCode.NoContent);
        }
        
        [HttpDelete]
        [Route("users/{id}")]
        [BasicAuthorization]
        public IHttpActionResult Delete(decimal id)
        {
            USER user = USER.findById(db, id);
            if (user == null)
                return NotFound();

            user.Delete(db);

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}