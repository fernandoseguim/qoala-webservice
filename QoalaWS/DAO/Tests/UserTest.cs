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
                //USER.findByEmail(qe, "email").Delete(qe);
                //qe.SaveChanges();
                USER u = new USER { NAME = "teste", EMAIL = "email", PASSWORD = "senhhaa" };
                u.Add(qe);
                qe.SaveChanges();
                //Assert.AreNotEqual(0m, u.ID_USER, "Usuario nao foi criado");
                u.Delete(qe);
                qe.SaveChanges();
            }
        }

        [TestMethod]
        public void UserUpdate()
        {
            using (var qe = new QoalaEntities())
            {
                USER u = USER.findByEmail(qe, "email");
                if (u != null)
                {
                    u.NAME = "Teste Atualizado";
                    var ret = u.Update(qe);
                    qe.SaveChanges();
                    Assert.AreNotEqual(0, u.ID_USER);
                }
            }
        }

        [TestMethod]
        public void UserDelete()
        {
            using (var qe = new QoalaEntities())
            {
                USER u = USER.findByEmail(qe, "email");
                if (u != null)
                {
                    u.Delete(qe);
                    qe.SaveChanges();
                    Assert.AreEqual(0, qe.USERS.Count(a => a.ID_USER == u.ID_USER));
                }
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

        [TestMethod]
        public void UserHasBeenTaken()
        {
            using (var qe = new QoalaEntities())
            {
                var email = "abc@abc";
                USER u = new USER { NAME = "teste", EMAIL = email, PASSWORD = "senhhaa" };
                u.Add(qe);
                qe.SaveChanges();
                Assert.IsTrue(USER.emailAlreadyExist(qe, email));
            }
        }

        [TestMethod]
        public void UserDeletedWithSameEmailNotHasTaken()
        {
            using (var qe = new QoalaEntities())
            {
                var email = "abcc@abcc";
                USER u = new USER { NAME = "teste", EMAIL = email, PASSWORD = "senhhaa" };
                u.Add(qe);
                qe.SaveChanges();
                u.DELETED_AT = DateTime.Now;
                u.Update(qe);
                Assert.IsFalse(USER.emailAlreadyExist(qe, email));
            }
        }


    }
}