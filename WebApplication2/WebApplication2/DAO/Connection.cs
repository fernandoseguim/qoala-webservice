using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WebApplication2.DAO
{
    public class Connection
    {
        private static OracleConnection instance = null;

        /// <summary>
        /// Retorna objeto singleton da conexão, cria e mantem o mesmo em memória.
        /// Usar com cuidado para não deixar conexão presa.
        /// </summary>
        public static OracleConnection Instance
        {
            get
            {
                if (instance == null)
                    instance = getConnection();
                return instance;
            }
        }
        private static String connectionString = null;
        /// <summary>
        /// Busca a string de conexão
        /// </summary>
        public static String ConnectionString
        {
            get
            {
                if (connectionString == null)
                    connectionString = ConfigurationManager.ConnectionStrings["ORCL"].ConnectionString;

                return connectionString;
            }
        }

        private Connection()
        {
        }

        /// <summary>
        ///   Cria uma nova conexão do pool
        /// </summary>
        /// <returns>novo OracleConnection</returns>
        public static OracleConnection getConnection()
        {
            OracleConnection conn = new OracleConnection(ConnectionString);
            return conn;
        }
    }

    [Microsoft.VisualStudio.TestTools.UnitTesting.TestClass]
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
    }
}