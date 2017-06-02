using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Dynamic;

namespace Condai.DAL
{
    public class Base
    {
        #region [ Attribute ]

        private static Base instance;
        private static string BD = "DBCONDAI";

        #endregion

        #region [ Constructor ]

        private Base() { }

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

        public Dictionary<int, object> ExecutionQuery(string query)
        {
            Dictionary<int, object> result = new Dictionary<int, object>();

            using (SqlConnection connectionCondai = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings[BD]))
            {
                using (SqlCommand commandCondai = new SqlCommand(query, connectionCondai))
                {
                    connectionCondai.Open();

                    using (SqlDataReader readerCondai = commandCondai.ExecuteReader())
                    {
                        if (readerCondai.HasRows)
                            while (readerCondai.Read())
                                result.Add(result.Count, SqlDataReaderToObject(readerCondai));
                        
                        readerCondai.Close();
                    }
                }
            }

            return result;
        }

        public Dictionary<int, object> ExecutionStoredProcedure(string storedProcedure, Dictionary<string, object> parametersCondai)
        {
            Dictionary<int, object> result = new Dictionary<int, object>();

            using (SqlConnection connectionCondai = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings[BD]))
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
                                result.Add(result.Count, SqlDataReaderToObject(readerCondai));

                        readerCondai.Close();
                    }
                }
            }

            return result;
        }

        #endregion

        #region [ Functions ]

        private object SqlDataReaderToObject(SqlDataReader readerCondai)
        {
            dynamic rowCondai = new ExpandoObject();

            IDictionary<string, object> newProperties = rowCondai;

            for (int i = 0; i < readerCondai.FieldCount; i++)
            {
                if (readerCondai.GetName(i) == string.Empty)
                    throw new Exception("Column name couldn't be null");

                switch (readerCondai.GetValue(i).GetType().Name)
                {
                    case "Int16":
                        newProperties.Add(readerCondai.GetName(i), readerCondai.GetInt16(i));
                        break;

                    case "Int32":
                        newProperties.Add(readerCondai.GetName(i), readerCondai.GetInt32(i));
                        break;

                    case "Int64":
                        newProperties.Add(readerCondai.GetName(i), readerCondai.GetInt64(i));
                        break;

                    case "String":
                        newProperties.Add(readerCondai.GetName(i), readerCondai.GetString(i));
                        break;

                    case "Boolean":
                        newProperties.Add(readerCondai.GetName(i), readerCondai.GetBoolean(i));
                        break;

                    case "Date":
                    case "DateTime":
                        newProperties.Add(readerCondai.GetName(i), readerCondai.GetDateTime(i));
                        break;
                        
                    case "Char":
                        newProperties.Add(readerCondai.GetName(i), readerCondai.GetChar(i));
                        break;

                    case "Double":
                        newProperties.Add(readerCondai.GetName(i), readerCondai.GetDouble(i));
                        break;

                    case "Float":
                        newProperties.Add(readerCondai.GetName(i), readerCondai.GetFloat(i));
                        break;

                    case "Decimal":
                        newProperties.Add(readerCondai.GetName(i), readerCondai.GetDecimal(i));
                        break;

                    default:
                        throw new Exception("Column type not implemented yet");
                }
            }
            
            return rowCondai;
        }

        private void AddParametersToCommand(SqlCommand commandCondai, Dictionary<string, object> parametersCondai)
        {
            foreach (KeyValuePair<string, object> param in parametersCondai)
            {
                if (param.Key == null)
                    throw new Exception("Param name for stored procedure couldn't be null");
            
                if (param.Value == null)
                    throw new Exception("Param value for stored procedure couldn't be null");

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

        #endregion

    }
}