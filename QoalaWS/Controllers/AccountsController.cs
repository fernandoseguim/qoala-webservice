using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QoalaWS.Models;

namespace QoalaWS.Controllers
{
    public class AccountsController : ApiController
    {
        [HttpPost]
        public ControlAccess Register([FromBody] string name, [FromBody] string email, [FromBody] string password)
        {
            User user = new User { Name = name, Email = email, Password = password };
            if(user.register())
            {
                ControlAccess controllAccess = new ControlAccess { UserId = user.Id };
                if(controllAccess.createToken())
                {
                    return controllAccess;
                }
            }
            return new ControlAccess { Id = 0, UserId = 0, Token = "abcd" };
        }

        [HttpPost]
        public ControlAccess ResetPassword([FromBody] string email)
        {
            return null;
        }

        [HttpPost]
        public ControlAccess Login([FromBody] string email, [FromBody] string password)
        {
            return null;
        }

        [HttpPost]
        public ControlAccess Logout([FromBody] string token)
        {
            return null;
        }
    }
}
