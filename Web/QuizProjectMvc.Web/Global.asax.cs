﻿namespace QuizProjectMvc.Web
{
    using System.Data.Entity;
    using System.Web;
    using System.Web.Http;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using Data;
    using Helpers;
    using Infrastructure.Mapping;
    using Configuration = Data.Migrations.Configuration;

#pragma warning disable SA1649 // File name must match first type name
    public class MvcApplication : HttpApplication
#pragma warning restore SA1649 // File name must match first type name
    {
        protected void Application_Start()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorEngineExtension());

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());

            AutofacConfig.RegisterAutofac();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var config = new HttpConfiguration();

            JsonNetConfig.UseCamelCase(config);
            WebApiConfig.Register(config);

            AutoMapperConfig.Init();
        }
    }
}
