using System;
using System.Net;
using System.Web.Http;
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
                    email = user.EMAIL,
                    name = user.NAME,
                    permission = user.PERMISSION,
                    created_at = user.CREATED_AT
                }
            );
        }
        
        [HttpPut]
        [Route("users/{id}")]
        [BasicAuthorization]
        public IHttpActionResult Update(decimal id, User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            User u = DAO.User.findById(db, id);

            if (u == null)
                return NotFound();

            user.ID_USER = id;

            if (user.EMAIL == null)
                user.EMAIL = u.EMAIL;
            if (user.NAME == null)
                user.NAME = u.NAME;
            if (user.PASSWORD == null)
                user.PASSWORD = u.PASSWORD;
            if (user.PERMISSION == 0)
                user.PERMISSION = u.PERMISSION;
            
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