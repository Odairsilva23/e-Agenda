using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace eAgenda.Controladores.Shared
{
    public static class DbSqlite
    {
        public static int InsertSQLite(string sql, Dictionary<string, object> parameters)
        {
            using (SQLiteConnection connection = new SQLiteConnection(Db.connectionString))
            {

                connection.Open();

                SQLiteCommand command = new SQLiteCommand(sql.AppendSelectIdentityLite(), connection);
                command.SetParameterSQLite(parameters);

                int id = Convert.ToInt32(command.ExecuteScalar());

                connection.Close();

                return id;
            }

        }
        public static void UpdateSQLite(string sql, Dictionary<string, object> parameters = null)
        {
            using (SQLiteConnection connection = new SQLiteConnection(Db.connectionString))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(sql, connection);

                command.SetParameterSQLite(parameters);

                command.ExecuteNonQuery();

                connection.Close();
            }
        }
        public static void DeleteSQLite(string sql, Dictionary<string, object> parameters)
        {
            UpdateSQLite(sql, parameters);
        }
        public static List<T> GetAllSQLite<T>(string sql, Db.ConverterDelegate<T> convert, Dictionary<string, object> parameters = null)
        {
            using (SQLiteConnection connection = new SQLiteConnection(Db.connectionString))
            {

                connection.Open();
                SQLiteCommand command = new SQLiteCommand(sql, connection);

                command.SetParameterSQLite(parameters);


                var list = new List<T>();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var obj = convert(reader);
                        list.Add(obj);
                    }
                }
                connection.Close();
                return list;
            }
        }
        public static T GetSQLite<T>(string sql, Db.ConverterDelegate<T> convert, Dictionary<string, object> parameters)
        {
            using (SQLiteConnection connection = new SQLiteConnection(Db.connectionString))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(sql, connection);

                command.CreateParameter();
                command.SetParameterSQLite(parameters);

                T t = default;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                        t = convert(reader);
                }
                connection.Close();
                return t;
            }
        }
        public static bool ExistsSQLite(string sql, Dictionary<string, object> parameters)
        {
            using (SQLiteConnection connection = new SQLiteConnection(Db.connectionString))
            {
                connection.Open();

                SQLiteCommand command = new SQLiteCommand(sql, connection);

                command.SetParameterSQLite(parameters);

                int numberRows = Convert.ToInt32(command.ExecuteScalar());

                connection.Close();

                return numberRows > 0;
            }
        }

        #region MetodosPrivados
        private static bool IsNullOrEmptylite(this object value)
        {
            return (value is string && string.IsNullOrEmpty((string)value)) ||
                    value == null;
        }
        private static void SetParameterSQLite(this SQLiteCommand command, Dictionary<string, object> parameters)
        {
            if (parameters == null || parameters.Count == 0)
                return;

            foreach (var parameter in parameters)
            {
                string name = parameter.Key;

                object value = parameter.Value.IsNullOrEmptylite() ? DBNull.Value : parameter.Value;

                SQLiteParameter dbParameter = new SQLiteParameter(name, value);

                command.Parameters.Add(dbParameter);
            }
        }
        private static string AppendSelectIdentityLite(this string sql)
        {
            return sql + ";SELECT last_insert_rowid()";
        }

        #endregion

    }
}