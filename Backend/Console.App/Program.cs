using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console.App
{
    public class Program
    {
        static void Main(string[] args)
        {
            Condai.BLL.Login d = new Condai.BLL.Login();
            d.Exec();
        }
    }
}
