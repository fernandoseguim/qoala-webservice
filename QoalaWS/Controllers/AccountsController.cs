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

        [HttpPost]
        public IHttpActionResult Register(USER user)
        {
            try
            {
                using (QoalaEntities qe = new QoalaEntities())
                {
                    user.Add(qe);
                    qe.SaveChanges();

                    ACCESSCONTROL ac = new ACCESSCONTROL { USER = user };

                    ac.Add(qe);
                    qe.SaveChanges();

                    return Ok(new { Token = ac.TOKEN });
                }
            } catch(Exception e) {
                return BadRequest(e.ToString());
            }
        }

        [HttpPost]
        public IHttpActionResult Login(USER user)
        {
            using (QoalaEntities qe = new QoalaEntities())
            {
                ACCESSCONTROL ca = user.doLogin(qe);
                if(ca == null)
                    return BadRequest();

                return Ok(new { Token = ca.TOKEN });
            }
        }

        [HttpPost]
        public IHttpActionResult Logout(ACCESSCONTROL control)
        {
            using (QoalaEntities qe = new QoalaEntities())
            {
                ACCESSCONTROL ac = ACCESSCONTROL.find(qe, control.TOKEN);
                if (ac == null)
                    return NotFound();

                ac.Delete(qe);
                return Ok();
            }
        }
    }
}
