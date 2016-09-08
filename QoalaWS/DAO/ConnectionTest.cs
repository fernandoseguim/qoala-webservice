using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QoalaWS.DAO;
using System.Linq;
using System.Data.Entity.Core.Objects;

namespace QoalaWS.DAOTest
{
    [TestClass]
    public class ConnectionTest
    {


        [TestMethod]
        public void ConnectAndDisconnect()
        {

            var con = Connection.getConnection();
            try
            {
                Assert.IsNotNull(con);
                con.Open();
                Assert.AreEqual(con.State, System.Data.ConnectionState.Open);
            }
            finally
            {
                con.Close();
                Assert.AreEqual(con.State, System.Data.ConnectionState.Closed);
                con.Dispose();
            }
        }

        [TestMethod]
        public void SelectScalar()
        {
            var con = Connection.getConnection();
            try
            {
                Assert.IsNotNull(con);
                con.Open();
                var cmd = con.CreateCommand();
                Assert.IsNotNull(cmd);
                cmd.CommandText = "select 1 + 2 as tres from dual";
                cmd.CommandType = System.Data.CommandType.Text;
                var tres = cmd.ExecuteScalar();
                Assert.AreEqual(tres, Decimal.Parse("3"));
            }
            finally
            {
                con.Close();
                Assert.AreEqual(con.State, System.Data.ConnectionState.Closed);
                con.Dispose();
            }
        }

        [TestMethod]
        public void SelectReader()
        {
            using (var con = Connection.getConnection())
            {
                con.Open();
                var cmd = con.CreateCommand();
                cmd.CommandText = "select 1 as coluna from dual" +
                " union select 2 as coluna from dual";
                cmd.CommandType = System.Data.CommandType.Text;
                using (var reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
                {
                    Decimal i = 1;
                    while (reader.Read())
                    {
                        var col = (Decimal)reader["coluna"];
                        Assert.AreEqual(col, i++);
                    }
                }
            }
        }

        [TestMethod]
        public void TestConnectionEntity()
        {
            var data = new QoalaEntities();
            //var i = data.Database.ExecuteSqlCommand("select sysdate from dual");

            var i = data.Database.SqlQuery<DateTime>("select sysdate from dual");
            var lista=i.ToList();
            Assert.AreEqual(1, lista.Count());
        }

        [TestMethod]
        public void TestUsingEntity()
        {
            QoalaEntities qe = new QoalaEntities();
            var posts = qe.POSTS.Select(s => s);
            Assert.AreEqual(0, posts.Count());
        }
        
        [TestMethod]
        public void TestAddedUsingEntity()
        {
            QoalaEntities qe = new QoalaEntities();

            var transaction = qe.Database.BeginTransaction();
            try
            {

                ObjectParameter idt_user = new ObjectParameter("OUT_ID_USER", typeof(Decimal));
                
                qe.SP_INSERT_USER("Gabriel", "qwe123", "gabriel@meu.com", 1, idt_user);
                var users = qe.USERS.Where(u => u.NAME == "Gabriel");

                Assert.AreNotEqual(0, users.Count());
            }
            finally
            {
                transaction.Commit();
                qe.SaveChanges();
            }
        }
    }
}
