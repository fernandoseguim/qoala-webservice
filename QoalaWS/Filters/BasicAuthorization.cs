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
        [DefaultValue(Permission.Public)]
        public Permission Permission { get; set; }
        /// <summary>
        /// Check if the UserID that come from route be the same de UserID from Token, 
        /// if not do not have the right rights
        /// </summary>
        [DefaultValue(false)]
        public bool PassForSameUserFromToken { get; set; }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var authHeader = actionContext.Request.Headers.Authorization;
            if (authHeader == null)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            else
            {
                // RFC 2617 sec 1.2, "scheme" name is case-insensitive
                if (authHeader.Scheme.Equals("Token",
                       StringComparison.OrdinalIgnoreCase) &&
                    authHeader.Parameter != null)
                {
                    //Need check if user has this control access
                    DAO.AccessControl ac = DAO.AccessControl.find(new DAO.QoalaEntities(), authHeader.Parameter);

                    if (ac == null)
                    {
                        actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                        return;
                    }

                    if (ac.USER.PERMISSION.Equals(Permission.Admin))
                        return;

                    if (this.Permission == Permission.NotSet)
                        this.Permission = Permission.Public;

                    if (!PermissionsRole.getRoles((Permission)ac.USER.PERMISSION).Contains(this.Permission))
                    {
                        //verifica se é o proprio usuario quem está acessando a requisição, e neste caso permite, mesmo não tendo direitos admin
                        if (PassForSameUserFromToken)
                        {
                            String user_id = "";
                            try
                            {
                                //UserID from route to check de UserID from Token
                                user_id = actionContext.ControllerContext.RouteData.Values.FirstOrDefault(v => v.Key.Equals("id")).Value.ToString();
                            }
                            catch { }
                            // if not equals then it is not authorized
                            if (!ac.USER.ID_USER.ToString().Equals(user_id))
                                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                        }
                        else
                        {
                            //for do not pass through, do not check token user
                            actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                        }
                    }
                }
            }
        }
    }
}