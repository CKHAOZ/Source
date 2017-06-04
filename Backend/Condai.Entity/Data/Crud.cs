namespace Condai.Entity
{
    public class Crud
    {
        #region [ Attribute ]

        private static Crud instance;

        public string Select = "s";
        public string Insert = "i";
        public string Update = "u";
        public string Delete = "d";
        
        #endregion

        #region [ Constructor ]

        private Crud() { }

        #endregion

        #region [ Properties ]

        public static Crud Instance
        {
            get
            {
                if (instance == null)
                    instance = new Crud();

                return instance;
            }
        }

        #endregion
    }
}
