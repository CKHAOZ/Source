using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Condai.Entity;

namespace Console.App
{
    public class Security
    {
        public void Crud()
        {
            List<UserCondai> ma = Condai.BLL.Login.Instance.GetUserList();

            UserCondai a = Condai.BLL.Login.Instance.GetUser(1);

            bool res = Condai.BLL.Login.Instance.DeleteUser(1);

            int resulta = Condai.BLL.Login.Instance.CreateUser(new Condai.Entity.UserCondai()
            {
                usuFirstName = "Catalina",
                usuLastName = "Bernal",
                usuPassword = "123456",
                usuUserName = "CBernal"
            });

            bool qu = Condai.BLL.Login.Instance.UpdateUser(new Condai.Entity.UserCondai()
            {
                idUsu = 1
            });
        }
    }
}
