using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication2.Models
{
    public class ControlAccess
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public string ExpiresAt { get; set; }

        public bool createToken()
        {
            Id = 5;
            Token = "asdasdsadsadsadsadsadsad";
            return true;
        }
    }
}
