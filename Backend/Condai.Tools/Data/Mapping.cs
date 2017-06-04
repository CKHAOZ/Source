using System;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;

namespace Condai.Tools
{
    public class Mapping
    {
        #region [ Attribute ]

        private static Mapping instance;

        #endregion

        #region [ Constructor ]

        private Mapping() { }

        #endregion

        #region [ Properties ]

        public static Mapping Instance
        {
            get
            {
                if (instance == null)
                    instance = new Mapping();

                return instance;
            }
        }

        #endregion

        #region [ Method ]

        public T ConvertSqlToEntity<T>(SqlDataReader readerCondai, T entityCondai)
        {
            T newEntity = DeepClone<T>(entityCondai);

            for (int i = 0; i < readerCondai.FieldCount; i++)
            {
                if (readerCondai.GetName(i) == string.Empty)
                    throw new Exception("Column name couldn't be null");

                switch (readerCondai.GetValue(i).GetType().Name)
                {
                    case "Int16":
                        SetEntity<T>(readerCondai.GetName(i), readerCondai.GetInt16(i), newEntity);
                        break;

                    case "Int32":
                        SetEntity<T>(readerCondai.GetName(i), readerCondai.GetInt32(i), newEntity);
                        break;

                    case "Int64":
                        SetEntity<T>(readerCondai.GetName(i), readerCondai.GetInt64(i), newEntity);
                        break;

                    case "String":
                        SetEntity<T>(readerCondai.GetName(i), readerCondai.GetString(i), newEntity);
                        break;

                    case "Boolean":
                        SetEntity<T>(readerCondai.GetName(i), readerCondai.GetBoolean(i), newEntity);
                        break;

                    case "Date":
                    case "DateTime":
                        SetEntity<T>(readerCondai.GetName(i), readerCondai.GetDateTime(i), newEntity);
                        break;

                    case "Char":
                        SetEntity<T>(readerCondai.GetName(i), readerCondai.GetChar(i), newEntity);
                        break;

                    case "Double":
                        SetEntity<T>(readerCondai.GetName(i), readerCondai.GetDouble(i), newEntity);
                        break;

                    case "Float":
                        SetEntity<T>(readerCondai.GetName(i), readerCondai.GetFloat(i), newEntity);
                        break;

                    case "Decimal":
                        SetEntity<T>(readerCondai.GetName(i), readerCondai.GetDecimal(i), newEntity);
                        break;

                    default:
                        throw new Exception("Column type not implemented yet");
                }
            }

            return newEntity;
        }

        #endregion

        #region [ Function ]

        public void SetEntity<T>(string name, object value, T entityCondai)
        {
            foreach (PropertyInfo property in entityCondai.GetType().GetProperties())
            {
                if (property.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    switch (name)
                    {
                        case "stringResult":
                            property.SetValue(entityCondai, Convert.ToString(value));
                            break;

                        case "intResult":
                            property.SetValue(entityCondai, Convert.ToInt32(value));
                            break;

                        case "boolResult":
                            property.SetValue(entityCondai, Convert.ToBoolean(value));
                            break;

                        default:
                            property.SetValue(entityCondai, value);
                            break;
                    }
                } 
            }
        }

        public T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var bf = new BinaryFormatter();
                bf.Serialize(ms, obj);
                ms.Position = 0;
                return (T)bf.Deserialize(ms);
            }
        }

        #endregion
    }
}
