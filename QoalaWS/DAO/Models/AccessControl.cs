using QoalaWS.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoalaWS.DAO
{
    public partial class ACCESSCONTROL
    {
        
        public static ACCESSCONTROL find(QoalaEntities context, Decimal tokenID)
        {
            return context.ACCESSCONTROLs.FirstOrDefault(a => a.EXPIRED_AT <= DateTime.Now && a.TOKEN == tokenID);
        }

        public ACCESSCONTROL createToken(QoalaEntities context, USER user=null)
        {
            // TODO: Generate token using this USER identity 
            if (user != null) USER = user;
            TOKEN = DateTime.Now.Ticks;// * USER.ID_USER;
            context.SaveChangesAsync();
            return this;
        }

        public bool destroyToken(QoalaEntities context)
        {
            if (EXPIRED_AT <= DateTime.Now)
            {
                var entry = context.Entry<ACCESSCONTROL>(this);
                this.EXPIRED_AT = DateTime.Now;
                entry.State = System.Data.Entity.EntityState.Unchanged;
            }
            return find(context, TOKEN) == null;
        }
    }
}
