using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QoalaWS.DAO.Models
{
    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class AccessControlTest
    {
        [TestMethod]
        public void AccessControlAdd()
        {
            using (var qe = new QoalaEntities())
            {
                var ac = new ACCESSCONTROL();
                ac.USER = new USER
                {
                    EMAIL = "access@control.com",
                    NAME = "access",
                    PASSWORD = "accac",
                    PERMISSION = 1
                };
                ac.createToken(qe);
                qe.ACCESSCONTROLs.Add(ac);
                qe.SaveChanges();
            }
        }
    }

}