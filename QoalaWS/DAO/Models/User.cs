using QoalaWS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace QoalaWS.DAO
{
    public partial class USER
    {
        public bool resetPassword()
        {
            // reset password
            return true;
        }

        public static USER findByEmail(QoalaEntities context, string email)
        {
            return context.USERS.FirstOrDefault(u => u.EMAIL == email && u.DELETED_AT <= DateTime.Now);
        }

        public static USER findById(QoalaEntities context, Decimal id_user)
        {
            return context.USERS.FirstOrDefault(u => u.ID_USER == id_user);
        }

        public static bool emailAlreadyExist(QoalaEntities context, string email)
        {
            return context.USERS.Count(u => u.EMAIL == email && u.DELETED_AT <= DateTime.Now) > 0;
        }

        public Decimal Delete(QoalaEntities context)
        {
            var outParameter = new ObjectParameter("ROWCOUNT", typeof(decimal));
            int ret = context.SP_DELETE_USER(ID_USER, outParameter);
            return (Decimal)outParameter.Value;
        }

        public Decimal Add(QoalaEntities context)
        {
            var outParameter = new ObjectParameter("OUT_ID_USER", typeof(decimal));
            int ret = context.SP_INSERT_USER(NAME, PASSWORD, EMAIL, PERMISSION, outParameter);
            ID_USER = (Decimal)outParameter.Value;
            return (Decimal)outParameter.Value;
        }

        public Decimal Update(QoalaEntities context)
        {
            var outParameter = new ObjectParameter("ROWCOUNT", typeof(decimal));
            int ret = context.SP_UPDATE_USER(ID_USER, NAME, PASSWORD, EMAIL, PERMISSION, outParameter);

            return (Decimal)outParameter.Value;
        }

        #region AccessControl

        public ControlAccess register(QoalaEntities context)
        {
            if (!emailAlreadyExist(context, EMAIL))
                return null;
            // TODO: Email já existe, não deveria trazer um retorno de exceção? ou algo que pudesse dizer ao registrando que já está cadastrado
            // TODO: O que exatamente isso precisa fazer?

            return doLogin(context, EMAIL, PASSWORD);
        }

        public static ControlAccess doLogin(QoalaEntities context, String UserEmail, String userPassword)
        {
            USER user = findByEmail(context, UserEmail);

            if (user.PASSWORD.Equals(userPassword))
            {
                ControlAccess controllAccess = new ControlAccess();
                return controllAccess.createToken();
            }

            return null;
        }

        public static bool doLogout(QoalaEntities context, string TokenID)
        {
            ControlAccess ca = ControlAccess.find(TokenID);
            return ca.destroyToken();
        }
        #endregion
    }
}