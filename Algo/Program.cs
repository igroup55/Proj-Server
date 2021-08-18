using jestapp_project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jestapp_project.Models.DAL;


namespace Algo
{
    class Program
    {
        static void Main(string[] args)
        {
            DBServices dbs = new DBServices();
            Users DTNotify = new Users();
            List<Users> userlist = DTNotify.GetUsers();

            foreach (var item in userlist)
            {
                
                if (item.DTtoken != null)
                {
                    TimeSpan difference = item.DTtoken.Subtract(DateTime.Now);
                    if(difference.Minutes <= 30 && item.DTtoken.Date == DateTime.Now.Date && difference.Minutes >=0 && difference.Hours == 0 && difference.Days == 0 )
                    {                    
                        Console.WriteLine(item.DTtoken + "  -  " + item.Token + " - " + item.DTtoken.Date + " - "+ DateTime.Now.Date + " - " +difference.Minutes);  
                        dbs.GetToken(item.Token);                       
                    }                    
                }
            }
            Console.Read();
        }
    }
}
