using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Madera.Data;
using MaderaSoft.App_Start;
using MaderaSoft.Migrations;

namespace MaderaSoft
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Initialisation de la bdd
            System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<MaderaEntities,Configuration>());
            System.Data.Entity.Database.SetInitializer(new MaderaSeeder());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Bootstrapper.Run();
        }
    }
}
