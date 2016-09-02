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
        private Connection()
        {
        }

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
}