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

            return Ok(user.Serializer());
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
            if (user.ADDRESS == null)
                user.ADDRESS = u.ADDRESS;
            if (user.DISTRICT == null)
                user.DISTRICT = u.DISTRICT;
            if (user.CITY == null)
                user.CITY = u.CITY;
            if (user.STATE == null)
                user.STATE = u.STATE;
            if (user.ZIPCODE == null)
                user.ZIPCODE = u.ZIPCODE;

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

        [HttpGet]
        [Route("users")]
        [BasicAuthorization(Permission = Permission.Admin)]
        public IHttpActionResult GetUsers(int page = 1)
        {
            var totalNumberPage = DAO.User.totalNumberPage(db);

            var data = new
            {
                users = DAO.User.All(db, page),
                pagination = new
                {
                    total_number_pages = totalNumberPage,
                    next_page = totalNumberPage > page,
                    current_page = page,
                    previous_page = page > 1 && page <= totalNumberPage
                }
            };
            return Ok(data);
        }
    }
}