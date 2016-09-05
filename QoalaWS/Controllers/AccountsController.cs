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
            return Ok(user.register());
        }

        [HttpPost]
        public IHttpActionResult ResetPassword([FromBody] string email)
        {
            Models.User user = Models.User.findByEmail(email);
            user.resetPassword();
            return null;
        }

        [HttpPost]
        public IHttpActionResult Login(Models.User user)
        {
            return Ok(user.doLogin());
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
