using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SQLite;

namespace eAgenda.Controladores.Shared
{

    public static class Db
    {
        public static readonly string connectionString = "";
        private static readonly string bancoEscolhido = "";
        public delegate T ConverterDelegate<T>(IDataReader reader);
        static Db()
        {
            bancoEscolhido = ConfigurationManager.AppSettings["bancodedados"];
            connectionString = ConfigurationManager.ConnectionStrings[bancoEscolhido].ConnectionString;
        }

        public static int Insert(string sql, Dictionary<string, object> parameters)
        {
            int id = 0;
            if (bancoEscolhido == "DBeAgendasqlite")
                id = DbSqlite.InsertSQLite(sql, parameters);
            if (bancoEscolhido == "DBeAgenda")
               id = SqlServer.InsertServer(sql, parameters);

            return id;
        }

        public static void Update(string sql, Dictionary<string, object> parameters = null)
        {

            if (bancoEscolhido == "DBeAgendasqlite")
                DbSqlite.UpdateSQLite(sql, parameters);
            if (bancoEscolhido == "DBeAgenda")
               SqlServer.UpdateServer(sql, parameters);

        }

        public static void Delete(string sql, Dictionary<string, object> parameters)
        {
            if (bancoEscolhido == "DBeAgendasqlite")
                DbSqlite.DeleteSQLite(sql, parameters);
           if (bancoEscolhido == "DBeAgenda")
                SqlServer.DeleteServer(sql, parameters);
        }

        public static List<T> GetAll<T>(string sql, ConverterDelegate<T> convert, Dictionary<string, object> parameters = null)
        {
            if (bancoEscolhido == "DBeAgendasqlite")
               return DbSqlite.GetAllSQLite(sql, convert, parameters);
            if (bancoEscolhido == "DBeAgenda")
               return SqlServer.GetAllServer(sql, convert, parameters);
            return new List<T>();
        }

        public static T Get<T>(string sql, ConverterDelegate<T> convert, Dictionary<string, object> parameters)
        {
            if (bancoEscolhido == "DBeAgendasqlite")
              return  DbSqlite.GetSQLite(sql, convert, parameters);
            if (bancoEscolhido == "DBeAgenda")
               return SqlServer.GetServer(sql,convert, parameters);
            return default;

        }

        public static bool Exists(string sql, Dictionary<string, object> parameters)
        {
            if (bancoEscolhido == "DBeAgendasqlite")
                return DbSqlite.ExistsSQLite(sql, parameters);
          if (bancoEscolhido == "DBeAgenda")
                return SqlServer.ExistsServer(sql, parameters);
            return false;
        }
    }
}
