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

        public Dictionary<int, object> Execution(string query)
        {
            Dictionary<int, object> result = new Dictionary<int, object>();

            using (SqlConnection connectionCondai = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings[BD]))
            {
                connectionCondai.Open();

                using (SqlCommand commandCondai = new SqlCommand(query, connectionCondai))
                {
                    using (SqlDataReader readerCondai = commandCondai.ExecuteReader())
                    {
                        if (readerCondai.HasRows)
                        {
                            while (readerCondai.Read())
                            {
                                dynamic row = new ExpandoObject();

                                IDictionary<string, object> newProperties = row;

                                newProperties.Add("Hi", "Hola");

                                //row.a = readerCondai.GetInt32(0);
                                //row("") = 2;

                                result.Add(1, row);
                            }
                        }
                        
                        readerCondai.Close();
                    }
                }
            }

            return result;
        }

        #endregion
    }
}