using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QoalaWS.DAO;

namespace QoalaWS.Controllers
{
    public class AccountsController : ApiController
    {
        private QoalaEntities db = new QoalaEntities();

        [HttpPost]
        public IHttpActionResult Register(USER user)
        {
            try
            {
                user.Add(db);
                db.SaveChanges();

                ACCESSCONTROL ac = new ACCESSCONTROL { USER = user };

                ac.Add(db);
                db.SaveChanges();

                return Created("", new { Token = ac.TOKEN });
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPost]
        public IHttpActionResult Login(USER user)
        {
            ACCESSCONTROL ac = user.doLogin(db);
            if (ac == null)
                return BadRequest("User or password is invalid!");

            return Created("", new { Token = ac.TOKEN });
        }

        [HttpPost]
        public IHttpActionResult Logout(ACCESSCONTROL control)
        {
            ACCESSCONTROL ac = ACCESSCONTROL.find(db, control.TOKEN);
            if (ac == null)
                return NotFound();

            ac.Delete(db);

            return StatusCode(HttpStatusCode.OK);
        }
    }
}
