using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Condai.Entity
{
    [Serializable]
    public class Result
    {
        public string stringResult { get; set; }
        public int intResult { get; set; }
        public bool boolResult { get; set; }
    }
}
