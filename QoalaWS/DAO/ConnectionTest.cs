using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QoalaWS.DAO;
using System.Linq;

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
            var data = new QoalaEntities().Database.ExecuteSqlCommand("select sysdate from dual");
        }

        [TestMethod]
        public void TestUsingEntity()
        {
            QoalaEntities qe = new QoalaEntities();
            var posts = qe.POSTS.Select(s => s);
            Assert.AreEqual(0, posts.Count());
        }

        
        [TestMethod]
        public void TestAddingUsingEntity()
        {
            QoalaEntities qe = new QoalaEntities();
            var ccc = new COMMENT_LOGS
            {
                COMMENTS_ID = 1,
                CREATED_AT = DateTime.Now,
                LOG = "Teste"
            };
            qe.COMMENT_LOGS.Add(ccc);
            qe.SaveChanges();
            
            var c = qe.COMMENT_LOGS.Select(meuobjet => meuobjet.LOG);
            Assert.AreEqual(1, c.Count());

            foreach (var item in c)
            {
                Assert.AreEqual("Teste", item);
            }
        }
    }
}
