using QoalaWS.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoalaWS.Models
{
    public class ControlAccess
    {
        public USER User { get; set; }
        public string Token { get; set; }
        public string ExpiresAt { get; set; }

        public ControlAccess()
        {

        }

        public ControlAccess(USER user)
        {
            this.User = user;
        }

        public static ControlAccess find(string tokenID)
        {
            // should find the register in DB
            ControlAccess ca = new ControlAccess { Token=tokenID };
            return ca;
        }

        public ControlAccess createToken()
        {
            // 
            Token = "asdasdsadsadsadsadsadsad";
            return this;
        }

        public bool destroyToken()
        {
            // should delete record
            return true;
        }
    }
}
