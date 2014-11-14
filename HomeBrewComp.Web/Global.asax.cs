using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using HomeBrewComp.Domain;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;

namespace HomeBrewComp.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static IDocumentStore DocumentStore { get; set; }

        public static IDocumentSession OpenSession()
        {
            return DocumentStore.OpenSession();
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DocumentStore = new DocumentStore() { ConnectionStringName = "HomeBrewComp" };
            DocumentStore.Initialize();

            IndexCreation.CreateIndexes(typeof(User).Assembly, DocumentStore);
        }

        protected void Application_End()
        {
            DocumentStore.Dispose();
        }

        internal static IAsyncDocumentSession OpenAsyncSession()
        {
            return DocumentStore.OpenAsyncSession();
        }
    }
}
