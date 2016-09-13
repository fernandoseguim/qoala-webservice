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

namespace QoalaWS.Controllers
{
    public class UsersController : ApiController
    {

        [Route("users/{id}")]
        public IHttpActionResult Get(decimal id)
        {
            using (QoalaEntities qe = new QoalaEntities())
            {
                USER user = USER.findById(qe, id);
                if (user == null)
                {
                    return NotFound();
                }

                return Ok(new { EMAIL = user.EMAIL, NAME = user.NAME, PERMISSION = user.PERMISSION, CREATED_AT = user.CREATED_AT });
            }
        }

        [HttpPut]
        [Route("users/{id}")]
        public IHttpActionResult Update(decimal id, USER user)
        {
            using (QoalaEntities qe = new QoalaEntities())
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (id != user.ID_USER)
                {
                    return BadRequest();
                }

                user.Update(qe);
            }


            return StatusCode(HttpStatusCode.NoContent);
        }
        
        [HttpPost]
        [Route("users/")]
        public IHttpActionResult PostUSER(USER user)
        {
            using (QoalaEntities qe = new QoalaEntities())
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                user.Add(qe);

                return CreatedAtRoute("Users", new { id = user.ID_USER }, user);
            }
        }
        
        [HttpDelete]
        public IHttpActionResult Delete(decimal id)
        {
            using (QoalaEntities qe = new QoalaEntities())
            {
                USER user = qe.USERS.Find(id);
                if (user == null)
                    return NotFound();

                user.Delete(qe);

                return Ok(user);
            }
        }
    }
}