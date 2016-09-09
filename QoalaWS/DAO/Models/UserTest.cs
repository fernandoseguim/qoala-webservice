using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QoalaWS.DAO
{

    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
    public class UserTestClass
    {

        [TestMethod]
        public void UserAdd()
        {
            using (var qe = new QoalaEntities())
            {
                USER u = new USER { NAME = "teste", EMAIL = "email", PASSWORD = "senhhaa" };
                u.Add(qe);
                qe.SaveChanges();
                Assert.AreNotEqual(0, u.ID_USER);
            }
        }

        [TestMethod]
        public void UserUpdate()
        {
            using (var qe = new QoalaEntities())
            {
                USER u = USER.findByEmail(qe, "email");
                Assert.IsNotNull(u);
                u.NAME = "Teste Atualizado";
                var ret = u.Update(qe);
                qe.SaveChanges();
                Assert.AreNotEqual(0, u.ID_USER);
            }
        }

        [TestMethod]
        public void UserDelete()
        {
            using (var qe = new QoalaEntities())
            {
                USER u = USER.findByEmail(qe, "email");

                Assert.IsNotNull(u);
                u.Delete(qe);
                qe.SaveChanges();
                Assert.AreEqual(0, qe.USERS.Count(a => a.ID_USER == u.ID_USER));
            }
        }


        [TestMethod]
        public void UserSelect1()
        {
            using (var qe = new QoalaEntities())
            {
                qe.USERS.Where(a => a.DELETED_AT <= DateTime.Now).FirstOrDefault();
            }
        }
    }
}