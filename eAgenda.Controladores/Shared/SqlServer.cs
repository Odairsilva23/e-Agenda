using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace eAgenda.Controladores.Shared
{
    public static class SqlServer
    {
        public static int InsertServer(string sql, Dictionary<string, object> parameters)
        {
            SqlConnection connection = new SqlConnection(Db.connectionString);

            SqlCommand command = new SqlCommand(sql.AppendSelectIdentity(), connection);

            command.SetParameters(parameters);

            connection.Open();

            int id = Convert.ToInt32(command.ExecuteScalar());

            connection.Close();

            return id;
        }

        public static void UpdateServer(string sql, Dictionary<string, object> parameters = null)
        {
            SqlConnection connection = new SqlConnection(Db.connectionString);

            SqlCommand command = new SqlCommand(sql, connection);

            command.SetParameters(parameters);

            connection.Open();

            command.ExecuteNonQuery();

            connection.Close();
        }

        public static void DeleteServer(string sql, Dictionary<string, object> parameters)
        {
            UpdateServer(sql, parameters);
        }

        public static List<T> GetAllServer<T>(string sql, Db.ConverterDelegate<T> convert, Dictionary<string, object> parameters = null)
        {
            SqlConnection connection = new SqlConnection(Db.connectionString);

            SqlCommand command = new SqlCommand(sql, connection);

            command.SetParameters(parameters);

            connection.Open();

            var list = new List<T>();

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var obj = convert(reader);
                list.Add(obj);
            }

            connection.Close();
            return list;
        }

        public static T GetServer<T>(string sql, Db.ConverterDelegate<T> convert, Dictionary<string, object> parameters)
        {
            SqlConnection connection = new SqlConnection(Db.connectionString);

            SqlCommand command = new SqlCommand(sql, connection);

            command.SetParameters(parameters);

            connection.Open();

            T t = default;

            var reader = command.ExecuteReader();

            if (reader.Read())
                t = convert(reader);

            connection.Close();
            return t;
        }

        public static bool ExistsServer(string sql, Dictionary<string, object> parameters)
        {
            SqlConnection connection = new SqlConnection(Db.connectionString);

            SqlCommand command = new SqlCommand(sql, connection);

            command.SetParameters(parameters);

            connection.Open();

            int numberRows = Convert.ToInt32(command.ExecuteScalar());

            connection.Close();

            return numberRows > 0;
        }
        private static bool IsNullOrEmpty(this object value)
        {
            return (value is string && string.IsNullOrEmpty((string)value)) ||
                    value == null;
        }

        #region Metodos Privados
        private static void SetParameters(this SqlCommand command, Dictionary<string, object> parameters)
        {
            if (parameters == null || parameters.Count == 0)
                return;

            foreach (var parameter in parameters)
            {
                string name = parameter.Key;

                object value = parameter.Value.IsNullOrEmpty() ? DBNull.Value : parameter.Value;

                SqlParameter dbParameter = new SqlParameter(name, value);

                command.Parameters.Add(dbParameter);
            }
        }

        private static string AppendSelectIdentity(this string sql)
        {
            return sql + ";SELECT SCOPE_IDENTITY()";
        }
        private static string AppendSelectIdentityLite(this string sql)
        {
            return sql + ";SELECT last_insert_rowid()";
        }

        #endregion
    }
}
