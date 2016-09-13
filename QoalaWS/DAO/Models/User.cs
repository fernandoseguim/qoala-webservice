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
        override
        public String ToString()
        {
            return "ID_USER: " + ID_USER + ", " +
                "NAME: " + NAME + ", " +
                "EMAIL: " + EMAIL + ", " +
                "PERMISSION: " + PERMISSION + ".";
        }
        public bool resetPassword()
        {
            // TODO: reset password
            // TODO: Send mail to user e-mail to create a new password
            return true;
        }

        public static USER findByEmail(QoalaEntities context, string email)
        {
            return context.USERS.FirstOrDefault(u => u.EMAIL == email && u.DELETED_AT == null);
        }

        public static USER findById(QoalaEntities context, Decimal id_user)
        {
            return context.USERS.FirstOrDefault(u => u.ID_USER == id_user && u.DELETED_AT == null);
        }

        public static bool emailAlreadyExist(QoalaEntities context, string email)
        {
            return context.USERS.Count(u => u.EMAIL == email && u.DELETED_AT == null) > 0;
        }

        public Decimal Delete(QoalaEntities context)
        {
            var outParameter = new ObjectParameter("ROWCOUNT", typeof(decimal));
            int ret = context.SP_DELETE_USER(ID_USER, outParameter);
            context.Entry(this).State = EntityState.Unchanged;
            return (Decimal)outParameter.Value;
        }

        public decimal? Add(QoalaEntities context)
        {
            
            if (emailAlreadyExist(context, EMAIL))
                throw new UserNotFoudException();

            var outParameter = new ObjectParameter("OUT_ID_USER", typeof(decimal));
            if(!(PERMISSION>0&& PERMISSION <= 3))
            {
                PERMISSION = 1;
            }
            int ret = context.SP_INSERT_USER(NAME, PASSWORD, EMAIL, PERMISSION, outParameter);
            if (outParameter.Value == DBNull.Value)
                ID_USER = 0m;
            else
                ID_USER = (Decimal)outParameter.Value;
            context.Entry(this).State = EntityState.Unchanged;
            return ID_USER;
        }

        public Decimal Update(QoalaEntities context)
        {
            var outParameter = new ObjectParameter("ROWCOUNT", typeof(decimal));
            int ret = context.SP_UPDATE_USER(ID_USER, NAME, PASSWORD, EMAIL, PERMISSION, outParameter);

            context.Entry(this).State = EntityState.Unchanged;
            return (Decimal)outParameter.Value;
        }
        
        public ACCESSCONTROL doLogin(QoalaEntities context)
        {
            USER user = findByEmail(context, EMAIL);
            if (user.PASSWORD.Equals(PASSWORD))
            {
                ACCESSCONTROL ca = new ACCESSCONTROL { USER = this };
                return ca.Add(context);
            }
            return null;
        }

        public class UserNotFoudException : Exception { }
    }
}