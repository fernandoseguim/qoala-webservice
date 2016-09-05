using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Models = QoalaWS.Models;

namespace QoalaWS.Controllers
{
    public class AccountsController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Register(Models.User user)
        {
            Models.ControlAccess ca = user.register();
            if (ca == null)
                return BadRequest();
            else
                return Ok(ca);
        }

        [HttpPost]
        public IHttpActionResult ResetPassword([FromBody] string email)
        {
            Models.User user = Models.User.findByEmail(email);
            if (user.resetPassword())
                return Ok();
            else
                return BadRequest();
        }

        [HttpPost]
        public IHttpActionResult Login(Models.User user)
        {
            Models.ControlAccess ca = user.doLogin();
            if (ca == null)
                return BadRequest();
            else
                return Ok(ca);
        }

        [HttpPost]
        public IHttpActionResult Logout([FromBody] string token)
        {
            if (Models.User.doLogout(token))
                return Ok();
            else
                return BadRequest();
        }
    }
}
