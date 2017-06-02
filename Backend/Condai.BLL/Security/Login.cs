using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condai.BLL
{
    public class Login
    {
        public void Exec()
        {
            // For Query
            Dictionary<int, object> resultQuery = Condai.DAL.Base.Instance.ExecutionQuery("Select idUsu from userCondai");

            // For Stored Procedure
            Dictionary<string, object> dicParams = new Dictionary<string, object>();
            dicParams.Add("@idUsu", 1);
            dicParams.Add("@usuAction", "s");

            Dictionary<int, object> result = Condai.DAL.Base.Instance.ExecutionStoredProcedure("SP_CRUD_USERCONDAI", dicParams);
        }
    }
}
