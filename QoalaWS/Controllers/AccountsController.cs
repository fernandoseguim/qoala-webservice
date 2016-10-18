using System;
using System.Net;
using System.Web.Http;
using QoalaWS.DAO;
using QoalaWS.Filters;

namespace QoalaWS.Controllers
{
    public class AccountsController : ApiController
    {
        private QoalaEntities db = new QoalaEntities();

        [HttpPost]
        [ValidateModel]
        public IHttpActionResult Register(User user)
        {
            try
            {
                user.Add(db);

                return Created("",
                    new
                    {
                        token = user.createAccessControl(db).TOKEN,
                        user = new
                        {
                            id_user = user.ID_USER,
                            email = user.EMAIL,
                            name = user.NAME,
                            permission = user.PERMISSION
                        }
                    }
                );
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [ValidateModel]
        public IHttpActionResult Login(User user)
        {
            AccessControl ac = user.doLogin(db);
            if (ac == null)
                return BadRequest("Email ou senha inválido");
            user = ac.GetUser(db);
            return Created("",
                new
                {
                    token = ac.TOKEN,
                    user = new
                    {
                        id_user = user.ID_USER,
                        email = user.EMAIL,
                        name = user.NAME,
                        permission = user.PERMISSION
                    }
                }
            );
        }

        //needs to check if the token on the body is the same on the headers
        [HttpPost]
        [BasicAuthorization]
        [ValidateModel]
        public IHttpActionResult Logout(AccessControl control)
        {
            AccessControl ac = AccessControl.find(db, control.TOKEN);
            if (ac == null)
                return NotFound();

            ac.Delete(db);

            return StatusCode(HttpStatusCode.OK);
        }

        [HttpGet]
        [BasicAuthorization]
        public IHttpActionResult Me()
        {
            var token = ActionContext.Request.Headers.Authorization.Parameter;

            AccessControl ac = AccessControl.find(db, token);
            if (ac == null)
                return NotFound();

            User user = ac.GetUser(db);

            if (user == null)
                return NotFound();

            return Ok(
                new
                {
                    id_user = user.ID_USER,
                    email = user.EMAIL,
                    name = user.NAME,
                    permission = user.PERMISSION
                }
            );

        }
    }
}
