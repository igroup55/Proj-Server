using jestapp_project.Models;
using jestapp_project.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace jestapp_project
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
           

            
        }



        //public void Messi()
        //{

        //    for (int i = 0; i < 100; i++)
        //    {

        //        DBServices s = new DBServices();
        //        Packages p = new Packages(86, 2, 3, 6, true, 206042707, i);
        //        s.AddPack(p);

        //        System.Threading.Thread.Sleep(5000);

        //    }
        //}
    }
}
