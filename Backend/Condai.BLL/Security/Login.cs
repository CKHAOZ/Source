using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Condai.Entity;
using System.Reflection;
using System.Dynamic;
using System.Data.SqlClient;

namespace Condai.BLL
{
    public class Login
    {
        #region [ Attribute ]

        private static Login instance;

        #endregion

        #region [ Constructor ]

        private Login() { }

        #endregion

        #region [ Properties ]

        public static Login Instance
        {
            get
            {
                if (instance == null)
                    instance = new Login();

                return instance;
            }
        }

        #endregion

        public List<UserCondai> GetUserList()
        {
            return Condai.DAL.Login.Instance.GetUserList();
        }

        public UserCondai GetUser(int idUser)
        {
            return Condai.DAL.Login.Instance.GetUser(idUser);
        }

        public int CreateUser(UserCondai user)
        {
            return Condai.DAL.Login.Instance.CreateUser(user);
        }

        public bool UpdateUser(UserCondai user)
        {
            return Condai.DAL.Login.Instance.UpdateUser(user);
        }

        public bool DeleteUser(int idUser)
        {
            return Condai.DAL.Login.Instance.DeleteUser(idUser, false);
        }
    }
}