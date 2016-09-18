using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QoalaWS.DAO;
using QoalaWS.Filters;

namespace QoalaWS.Controllers
{
    public class AccountsController : ApiController
    {
        private QoalaEntities db = new QoalaEntities();

        [HttpPost]
        public IHttpActionResult Register(User user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                user.Add(db);
                
                
                return Created("", new {
                    Token = user.createAccessControl(db).TOKEN
                });
            }
            catch (Exception e)
            {
                return BadRequest(e.ToString());
            }
        }

        [HttpPost]
        public IHttpActionResult Login(User user)
        {
            ACCESSCONTROL ac = user.doLogin(db);
            if (ac == null)
                return BadRequest("User or password is invalid!");

            return Created("", new { Token = ac.TOKEN });
        }

        //needs to check if the token on the body is the same on the headers
        [HttpPost]
        [BasicAuthorization]
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
