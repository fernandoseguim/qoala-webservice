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
        
        public static ACCESSCONTROL find(QoalaEntities context, String token)
        {
            return context.ACCESSCONTROLs.FirstOrDefault(a => a.TOKEN == token);
        }

        public ACCESSCONTROL Add(QoalaEntities context)
        {
            TOKEN = DateTime.Now.Ticks.ToString() + "-" + USER.ID_USER.ToString();
            context.SaveChanges();
            return this;
        }

        public bool Delete(QoalaEntities context)
        {
            context.ACCESSCONTROLs.Remove(this);
            context.SaveChanges();
            return find(context, TOKEN) == null;
        }
    }
}
