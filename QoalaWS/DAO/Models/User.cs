using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QoalaWS.DAO
{
    public partial class USER
    {
        public bool register()
        {
            var attr=this.GetType().GetProperties();
            var id = attr[0].GetMethod.Invoke(this, new object[0]);
            var a = attr.ToList();
            Console.Write(id);
            
            return true;
        }
    }
}