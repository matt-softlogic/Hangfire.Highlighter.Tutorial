using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing.Configuration;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Common.Logging;
using Common.Logging.Configuration;
using Common.Logging.NLog;
using Common.Logging.Simple;
using Hangfire.Highlighter.Migrations;
using Hangfire.Highlighter.Models;

namespace Hangfire.Highlighter
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ConfigureHangfireLogging();

            Database.SetInitializer(new MigrateDatabaseToLatestVersion<HighlighterDbContext, Configuration>());
        }

        private static void ConfigureHangfireLogging()
        {
            var properties = new NameValueCollection();
            properties["Level"] = "All";
            LogManager.Adapter = new TraceLoggerFactoryAdapter(properties);
        }


        protected void Application_BeginRequest()
        {
            StackExchange.Profiling.MiniProfiler.Start();
        }

        protected void Application_EndRequest()
        {
            StackExchange.Profiling.MiniProfiler.Stop();
        }
    }
}