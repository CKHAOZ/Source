using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Condai.DAL
{
    public class BASE
    {
        #region [ Attribute ]

        private static BASE instance;

        #endregion

        #region [ Constructor ]

        private BASE() { }

        #endregion

        #region [ Properties ]

        public static BASE Instance
        {
            get
            {
                if (instance == null)
                    instance = new BASE();

                return instance;
            }
        }

        #endregion

        #region [ Methods ]



        #endregion
    }
}