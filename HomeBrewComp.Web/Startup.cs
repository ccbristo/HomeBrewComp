using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HomeBrewComp.Web.Startup))]
namespace HomeBrewComp.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
