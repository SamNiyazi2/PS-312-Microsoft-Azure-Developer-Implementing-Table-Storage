using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Pluralsight.Todo
{

    public class MvcApplication : System.Web.HttpApplication
    {
        // 05/16/2021 10:52 am - SSN - [20210516-1011] - [001] - M03-02 - Introducing Azure table storage in a .NET application
        public static string ps312AzureTableConnectionString;
        public static string Application_Name = "PS-312 - Another Todo List";
        

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            ps312AzureTableConnectionString = Environment.GetEnvironmentVariable("ps312AzureTableConnectionString");

        }
    }
}
