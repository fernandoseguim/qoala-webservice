using System;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace QoalaWS.Filters
{
    public enum Permission
    {
        NotSet,
        Public = 1,
        Editor = 2,
        Admin = 3
    }

    public sealed class PermissionsRole
    {
        public static Permission[] getRoles(Permission permission)
        {
            switch (permission)
            {
                case Permission.Public:
                    return RolePublic;
                case Permission.Editor:
                    return RoleEditor;
                case Permission.Admin:
                    return RoleAdmin;
            }
            return RolePublic;
        }

        public static Permission[] RoleAdmin = new Permission[] { Permission.Public, Permission.Editor, Permission.Admin };
        public static Permission[] RoleEditor = new Permission[] { Permission.Public, Permission.Editor };
        public static Permission[] RolePublic = new Permission[] { Permission.Public };
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class BasicAuthorization : ActionFilterAttribute
    {
        /// <summary>
        /// Regras de permissão de acesso a Action - Default QoalaWS.Filters.Permissions.Public 
        /// </summary>
        public Permission Permission { get; set; }
        
        public override void OnActionExecuting(HttpActionContext actionContext)
        {

            var request = HttpContext.Current.Request;
            var authHeader = request.Headers["Authorization"];
            if (authHeader == null)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            else
            {
                var authHeaderVal = AuthenticationHeaderValue.Parse(authHeader);

                // RFC 2617 sec 1.2, "scheme" name is case-insensitive
                if (authHeaderVal.Scheme.Equals("Token",
                       StringComparison.OrdinalIgnoreCase) &&
                    authHeaderVal.Parameter != null)
                {
                    //Need check if user has this control access
                    DAO.AccessControl ac = DAO.AccessControl.find(new DAO.QoalaEntities(), authHeaderVal.Parameter);

                    if (ac == null)
                    {
                        actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                        return;
                    }

                    if (ac.USER.PERMISSION.Equals(Permission.Admin))
                        return;

                    if (Permission == Permission.NotSet)
                        Permission = Permission.Public;

                    if (!PermissionsRole.getRoles((Permission)ac.USER.PERMISSION).Contains(Permission))
                        actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                }
            }
        }
    }
}