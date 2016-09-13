using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QoalaWS.DAO;
using System.Linq;
using System.Data.Entity.Core.Objects;
using Oracle.ManagedDataAccess.Client;

namespace QoalaWS.DAOTest
{
    [TestClass]
    public class ConnectionTest
    {

        [TestMethod]
        public void ConnectionEntity()
        {
            using (var data = new QoalaEntities())
            {
                //var i = data.Database.ExecuteSqlCommand("select sysdate from dual");

                var i = data.Database.SqlQuery<DateTime>("select sysdate from dual");
                var lista = i.ToList();
                Assert.AreEqual(1, lista.Count());
            }
        }
    }
}
