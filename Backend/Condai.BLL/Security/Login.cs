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
            Condai.DAL.Base.Instance.Execution("Select idUsu from userCondai where idUsu = 1");
        }
    }
}
