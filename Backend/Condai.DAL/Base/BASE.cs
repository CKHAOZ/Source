using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Dynamic;
using Condai.Tools;
using System.Linq;

namespace Condai.DAL
{
    public class Base
    {
        #region [ Attribute ]

        private static Base instance;
        private string DB = "DBCONDAI";

        #endregion

        #region [ Constructor ]

        private Base() { }

        private Base(string db)
        {
            this.DB = db;
        }

        #endregion

        #region [ Properties ]

        public static Base Instance
        {
            get
            {
                if (instance == null)
                    instance = new Base();

                return instance;
            }
        }

        #endregion

        #region [ Methods ]

        public List<T> ExecutionQueryList<T>(string query, T entityCondai)
        {
            List<T> result = new List<T>();

            using (SqlConnection connectionCondai = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings[DB]))
            {
                using (SqlCommand commandCondai = new SqlCommand(query, connectionCondai))
                {
                    connectionCondai.Open();

                    using (SqlDataReader readerCondai = commandCondai.ExecuteReader())
                    {
                        if (readerCondai.HasRows)
                            while (readerCondai.Read())
                                result.Add(Mapping.Instance.ConvertSqlToEntity<T>(readerCondai, entityCondai));

                        readerCondai.Close();
                    }
                }
            }

            return result;
        }

        public List<T> ExecutionSPList<T>(string storedProcedure, Dictionary<string, object> parametersCondai, T entityCondai)
        {
            List<T> result = new List<T>();

            using (SqlConnection connectionCondai = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings[DB]))
            {
                using (SqlCommand commandCondai = new SqlCommand(storedProcedure, connectionCondai))
                {
                    commandCondai.CommandType = System.Data.CommandType.StoredProcedure;
                    AddParametersToCommand(commandCondai, parametersCondai);

                    connectionCondai.Open();

                    using (SqlDataReader readerCondai = commandCondai.ExecuteReader())
                    {
                        if (readerCondai.HasRows)
                            while (readerCondai.Read())
                                result.Add(Mapping.Instance.ConvertSqlToEntity<T>(readerCondai, entityCondai));

                        readerCondai.Close();
                    }
                }
            }

            return result;
        }

        public T ExecutionQueryObject<T>(string query, T entityCondai)
        {
            using (SqlConnection connectionCondai = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings[DB]))
            {
                using (SqlCommand commandCondai = new SqlCommand(query, connectionCondai))
                {
                    connectionCondai.Open();

                    using (SqlDataReader readerCondai = commandCondai.ExecuteReader())
                    {
                        if (readerCondai.HasRows)
                        {
                            readerCondai.Read();
                            entityCondai = Mapping.Instance.ConvertSqlToEntity<T>(readerCondai, entityCondai);
                        }

                        readerCondai.Close();
                    }
                }
            }

            return entityCondai;
        }

        public T ExecutionSPObject<T>(string storedProcedure, Dictionary<string, object> parametersCondai, T entityCondai)
        {
            using (SqlConnection connectionCondai = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings[DB]))
            {
                using (SqlCommand commandCondai = new SqlCommand(storedProcedure, connectionCondai))
                {
                    commandCondai.CommandType = System.Data.CommandType.StoredProcedure;
                    AddParametersToCommand(commandCondai, parametersCondai);

                    connectionCondai.Open();

                    using (SqlDataReader readerCondai = commandCondai.ExecuteReader())
                    {
                        if (readerCondai.HasRows)
                        {
                            readerCondai.Read();
                            entityCondai = Mapping.Instance.ConvertSqlToEntity<T>(readerCondai, entityCondai);
                        }

                        readerCondai.Close();
                    }
                }
            }

            return entityCondai;
        }

        #endregion

        #region [ Functions ]

        private void AddParametersToCommand(SqlCommand commandCondai, Dictionary<string, object> parametersCondai)
        {
            if (parametersCondai != null && parametersCondai.Count > 0)
            {
                foreach (KeyValuePair<string, object> param in parametersCondai.Where(m => m.Value != null))
                {
                    if (param.Key == null)
                        throw new Exception("Param name for stored procedure couldn't be null");

                    switch (param.Value.GetType().Name)
                    {
                        case "Int16":
                        case "Int32":
                        case "Int64":
                            commandCondai.Parameters.Add(param.Key, System.Data.SqlDbType.Int).Value = param.Value;
                            break;

                        case "String":
                            commandCondai.Parameters.Add(param.Key, System.Data.SqlDbType.VarChar).Value = param.Value;
                            break;

                        case "Boolean":
                            commandCondai.Parameters.Add(param.Key, System.Data.SqlDbType.Bit).Value = param.Value;
                            break;

                        case "Date":
                            commandCondai.Parameters.Add(param.Key, System.Data.SqlDbType.Date).Value = param.Value;
                            break;

                        case "DateTime":
                            commandCondai.Parameters.Add(param.Key, System.Data.SqlDbType.DateTime).Value = param.Value;
                            break;

                        case "Char":
                            commandCondai.Parameters.Add(param.Key, System.Data.SqlDbType.Char).Value = param.Value;
                            break;

                        case "Double":
                        case "Float":
                        case "Decimal":
                            commandCondai.Parameters.Add(param.Key, System.Data.SqlDbType.Decimal).Value = param.Value;
                            break;

                        default:
                            throw new Exception("Column type not implemented yet");
                    }
                }
            }
        }

        #endregion
    }
}