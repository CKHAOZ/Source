using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condai.Entity
{
    [Serializable]
    public class UserCondai
    {
        public int idUsu { get; set; }
        public string usuFirstName { get; set; }
        public string usuLastName { get; set; }
        public string usuUserName { get; set; }
        public string usuPassword { get; set; }
        public bool usuActive { get; set; }
    }
}
