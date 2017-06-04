using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Condai.Entity;
using System.Data.SqlClient;

namespace Condai.DAL
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

        #region [ Method ]

        public List<UserCondai> GetUserList()
        {
            return Condai.DAL.Base.Instance.ExecutionQueryList<UserCondai>("Select * from userCondai", new UserCondai());
        }

        public UserCondai GetUser(int idUser)
        {
            Dictionary<string, object> dicParams = new Dictionary<string, object>();

            dicParams.Add("@idUsu", idUser);
            dicParams.Add("@usuAction", Crud.Instance.Select);

            return Condai.DAL.Base.Instance.ExecutionSPObject<UserCondai>("SP_CRUD_USERCONDAI", dicParams, new UserCondai());
        }

        public int CreateUser(UserCondai user)
        {
            Dictionary<string, object> dicParams = new Dictionary<string, object>();

            dicParams.Add("@usuFirstName", user.usuFirstName);
            dicParams.Add("@usuLastName", user.usuLastName);
            dicParams.Add("@usuUserName", user.usuUserName);
            dicParams.Add("@usuPassword", user.usuPassword);
            dicParams.Add("@usuActive", true);
            dicParams.Add("@usuAction", Crud.Instance.Insert);

            return Condai.DAL.Base.Instance.ExecutionSPObject<Result>("SP_CRUD_USERCONDAI", dicParams, new Result()).intResult;
        }

        public bool UpdateUser(UserCondai user)
        {
            Dictionary<string, object> dicParams = new Dictionary<string, object>();

            dicParams.Add("@idUsu", user.idUsu);
            dicParams.Add("@usuFirstName", user.usuFirstName);
            dicParams.Add("@usuLastName", user.usuLastName);
            dicParams.Add("@usuUserName", user.usuUserName);
            dicParams.Add("@usuPassword", user.usuPassword);
            dicParams.Add("@usuActive", true);
            dicParams.Add("@usuAction", Crud.Instance.Update);
            
            return Condai.DAL.Base.Instance.ExecutionSPObject<Result>("SP_CRUD_USERCONDAI", dicParams, new Result()).boolResult;
        }

        public bool DeleteUser(int idUser, bool active)
        {
            Dictionary<string, object> dicParams = new Dictionary<string, object>();

            dicParams.Add("@idUsu", idUser);
            dicParams.Add("@usuActive", active);
            dicParams.Add("@usuAction", Crud.Instance.Delete);

            return Condai.DAL.Base.Instance.ExecutionSPObject<Result>("SP_CRUD_USERCONDAI", dicParams, new Result()).boolResult;
        }

        #endregion
    }
}
