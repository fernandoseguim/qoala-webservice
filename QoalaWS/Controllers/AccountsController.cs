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
        public Models.ControlAccess Register([FromBody] string name, [FromBody] string email, [FromBody] string password)
        {
            Models.User user = new Models.User { Name = name, Email = email, Password = password };
            return user.register();
        }

        [HttpPost]
        public Models.ControlAccess ResetPassword([FromBody] string email)
        {
            Models.User user = Models.User.findByEmail(email);
            user.resetPassword();
            return null;
        }

        [HttpPost]
        public Models.ControlAccess Login([FromBody] string email, [FromBody] string password)
        {
            return Models.User.doLogin(email, password);
        }

        [HttpPost]
        public bool Logout([FromBody] string token)
        {
            return Models.User.doLogout(token);
        }
    }
}
