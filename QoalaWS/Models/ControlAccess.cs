using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QoalaWS.Models
{
    public class ControlAccess
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public string ExpiresAt { get; set; }

        public static ControlAccess find(string tokenID)
        {
            // should find the register in DB
            ControlAccess ca = new ControlAccess { };
            return ca;
        }

        public ControlAccess createToken()
        {
            Id = 5;
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
