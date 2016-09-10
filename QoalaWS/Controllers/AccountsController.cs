using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using QoalaWS.Models;
using QoalaWS.DAO;

namespace QoalaWS.Controllers
{
    public class AccountsController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Register(USER user)
        {
            //TODO: Fazer um model para usar somente no login(nome, somente email, senha), sem os demais atributos no model USER.
            using (QoalaEntities qe = new QoalaEntities())
            {
                Models.ControlAccess ca = user.register(qe);
                this.
                if (ca == null)
                    return BadRequest();
                else
                    return Ok(ca);
            }
        }

        [HttpPost]
        public IHttpActionResult ResetPassword([FromBody] string email)
        {
            using (QoalaEntities qe = new QoalaEntities())
            {
                USER user = USER.findByEmail(qe, email);
                if (user.resetPassword())
                    return Ok();
                else
                    return BadRequest();
            }
        }

        [HttpPost]
        public IHttpActionResult Login(USER user)
        {
            //TODO: Fazer um model para usar somente no login(somente email, senha), sem os demais atributos no model USER.
            using (QoalaEntities qe = new QoalaEntities())
            {
                Models.ControlAccess ca = USER.doLogin(qe, user.EMAIL, user.PASSWORD);
                if (ca == null)
                    return BadRequest();
                else
                    return Ok(ca);
            }
        }

        [HttpPost]
        public IHttpActionResult Logout([FromBody] string token)
        {
            using (QoalaEntities qe = new QoalaEntities())
            {
                if (USER.doLogout(qe, token))
                    return Ok();
                else
                    return BadRequest();
            }
        }
    }
}
