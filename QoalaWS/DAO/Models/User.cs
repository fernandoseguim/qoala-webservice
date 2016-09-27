using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;
using QoalaWS.Exceptions;

namespace QoalaWS.DAO
{
    public partial class User
    {
        override
        public String ToString()
        {
            return "ID_USER: " + ID_USER + ", " +
                "NAME: " + NAME + ", " +
                "EMAIL: " + EMAIL + ", " +
                "PERMISSION: " + PERMISSION + ".";
        }

        public static User findByEmail(QoalaEntities context, string email)
        {
            return context.USERS.FirstOrDefault(u => u.EMAIL == email && u.DELETED_AT == null);
        }

        public static User findById(QoalaEntities context, Decimal id_user)
        {
            return context.USERS.FirstOrDefault(u => u.ID_USER == id_user && !u.DELETED_AT.HasValue);
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
                throw new UserEmailExistsException();

            var outParameter = new ObjectParameter("OUT_ID_USER", typeof(decimal));
            if (!(PERMISSION > 0 && PERMISSION <= 3))
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
            var outParameter = new ObjectParameter("PROWCOUNT", typeof(decimal));
            context.SP_UPDATE_USER(ID_USER, NAME, PASSWORD, EMAIL, PERMISSION, outParameter);
            return 1;
        }

        // this may should be on ACCESSCONTROL class
        public AccessControl doLogin(QoalaEntities context)
        {
            User user = findByEmail(context, EMAIL);
            if (user != null && user.PASSWORD.Equals(PASSWORD))
            {
                AccessControl access=null;
                //ja teve sessões, busca uma ativa
                if (user.ACCESSCONTROLs.Count() > 0)
                {
                    // melhoria: Adicionar Client ID. e cada tipo de aplicação deverá passar um ClientID para o login, e filtrar aqui.
                    access = user.ACCESSCONTROLs.FirstOrDefault(ac => ac.EXPIRED_AT >= DateTime.Now);
                }

                if(access==null)
                {
                    access = new AccessControl { USER = user };
                    access.Add(context);
                    context.SaveChanges();
                }
                return access;
            }
            return null;
        }

        public AccessControl createAccessControl(QoalaEntities context)
        {
            AccessControl ac = new AccessControl { USER = this };
            return ac.Add(context);
        }
    }
}