using jestapp_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jestapp_project.Models.DAL;


namespace ReserveLocker
{
    class Program
    {
        static void Main(string[] args)
        {
            Helper helper = new Helper();
            helper.HandlePackages();
            Console.Read();








        }
    }
}
