using Autofac;
using Autofac.Integration.Mvc;
using AzureKeyVault;
using Pluralsight.Todo.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Pluralsight.Todo
{

    public class MvcApplication : System.Web.HttpApplication
    {
        // 05/16/2021 10:52 am - SSN - [20210516-1011] - [001] - M03-02 - Introducing Azure table storage in a .NET application

        public static string ps312AzureTableConnectionString_azureTable;
        public static string ps312AzureTableConnectionString_cosmboDBTable;

        public static string Application_Name = "PS-312 - Another Todo List";
        

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);



            setup_Autofac();



            ps312AzureTableConnectionString_azureTable = Environment.GetEnvironmentVariable("ps312AzureTableConnectionString_azureTable");
            ps312AzureTableConnectionString_cosmboDBTable = Environment.GetEnvironmentVariable("ps312AzureTableConnectionString_cosmboDBTable");

            processAzureVaultRequests().GetAwaiter().GetResult();

        }


        async Task processAzureVaultRequests()
        {

            //await VaultDataAccess.writeSecrets_Incomplete_But_Functional("ps312AzureTableConnectionString_azureTable", @"");
            //await VaultDataAccess.writeSecrets_Incomplete_But_Functional("ps312AzureTableConnectionString_cosmboDBTable", @"");


            //string p1 = await VaultDataAccess.getSecret("ps312AzureTableConnectionString-azureTable");
            //string p2 = await VaultDataAccess.getSecret("ps312AzureTableConnectionString-cosmboDBTable");

             
            try
            {
                string tempValue  = await VaultDataAccess.getSecret("ssn-secret-test-pc-20210224"); 

            }
            catch (Exception ex)
            {
                 
            }


        }


        void setup_Autofac()
        {

            ContainerBuilder builder = new ContainerBuilder();

            // Register individual components
            // builder.RegisterInstance(new TodoRepository()) .As<ITodoRepository>();
            // install-package autofac
            // install-package autofac.integration.mvc  -> install-package autofac.mvc5 NOT mvc4
            // There is also autofac.integration.webapi

            //////////// builder.RegisterType<TodoRepository>().As<ITodoRepository>();
            //builder.RegisterType<TodoRepository>().Named<ITodoRepository>("StorageTable").WithParameter("AzureTableSource", "StorageTable");
            //builder.RegisterType<TodoRepository>().Named<ITodoRepository>("CosmoDBTable").WithParameter("AzureTableSource", "CosmoDBTable");



            // HttpWorkerRequest but only fires once on creation.
            // builder.RegisterType<TodoRepository>().As<ITodoRepository>().WithParameter(new TypedParameter(typeof(string), "ConnectionStringName"));
            builder.RegisterType<TodoRepository>().As<ITodoRepository>();



            Assembly execAssembly = Assembly.GetExecutingAssembly();
            // builder.RegisterAssemblyTypes(execAssembly).AsSelf().AsImplementedInterfaces();
            builder.RegisterControllers(execAssembly);

            builder.RegisterTypes();


            var container = builder.Build();


            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));


        }


    }

}