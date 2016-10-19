using System;
using System.Net;
using System.Web.Http;
using QoalaWS.DAO;
using QoalaWS.Filters;
using Newtonsoft.Json.Linq;
using System.Data.Entity;

namespace QoalaWS.Controllers
{
    public class UsersController : ApiController
    {

        [Route("users/{id}")]
        [BasicAuthorization(Permission = Permission.Admin, PassForSameUserFromToken = true)]
        public IHttpActionResult Get(decimal id)
        {
            using (QoalaEntities db = new QoalaEntities())
            {
                User user = DAO.User.findById(db, id);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user.Serializer());
            }
        }

        [HttpPost]
        [Route("users/{id}/plans/{plan_id}/{qnt}")]
        public IHttpActionResult AddPlansUser(int id, int plan_id, int qnt)
        {
            QoalaEntities db = new QoalaEntities();

            User u = DAO.User.findById(db, id);
            if(u == null)
                return NotFound();
            Plan plan = Plan.Find(plan_id);
            if (plan == null)
                return NotFound();
            if (plan.LEFT < qnt)
                return BadRequest();
            u.ID_PLAN = plan_id;
            db.Entry(u).State = EntityState.Modified;
            db.SaveChanges();

            plan.LEFT = plan.LEFT - qnt;
            db.Entry(plan).State = EntityState.Modified;
            db.SaveChanges();
            //u.AddPlan(qnt);


            return Ok(u.Serializer());
        }

        [HttpPut]
        [Route("users/{id}")]
        [BasicAuthorization(Permission = Permission.Admin, PassForSameUserFromToken = true)]
        public IHttpActionResult Update(decimal id, User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            using (QoalaEntities db = new QoalaEntities())
            {
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
        }

        [HttpDelete]
        [Route("users/{id}")]
        [BasicAuthorization(Permission = Permission.Admin, PassForSameUserFromToken = true)]
        public IHttpActionResult Delete(decimal id)
        {
            using (QoalaEntities db = new QoalaEntities())
            {
                User user = DAO.User.findById(db, id);
                if (user == null)
                    return NotFound();

                user.Delete(db);

                return StatusCode(HttpStatusCode.NoContent);
            }
        }

        [HttpGet]
        [Route("users")]
        [BasicAuthorization(Permission = Permission.Admin)]
        public IHttpActionResult GetUsers(int page = 1)
        {
            using (QoalaEntities db = new QoalaEntities())
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
}