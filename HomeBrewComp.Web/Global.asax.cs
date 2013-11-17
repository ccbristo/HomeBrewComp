using System;
using FubuMVC.Core;
using HomeBrewComp.Persistence;

namespace HomeBrewComp.Web
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            NHibernateSetup.Configure("HomeBrewComp");
            FubuApplication.BootstrapApplication<MyApplication>();

            // TODO [ccb] Figure out a good way to make this pluggable
            DbInitializer.Initialize();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}