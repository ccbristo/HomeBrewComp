using FubuMVC.Core;
using FubuMVC.StructureMap;
using HomeBrewComp.Persistence;
using HomeBrewComp.Web.Features.Home;
using NHibernate;
using StructureMap;
using StructureMap.Configuration.DSL;

namespace HomeBrewComp.Web
{
    public class MyApplication : IApplicationSource
    {
        public FubuApplication BuildApplication()
        {
            var container = new Container();
            container.Configure(config => config.Scan(scanner =>
                {
                    scanner.LookForRegistries();
                    scanner.TheCallingAssembly();
                }));

            return FubuApplication.For<MyFubuMvcPolicies>()
                .StructureMap(container);
        }
    }

    public class MyStructureMapRegistry : Registry
    {
        public MyStructureMapRegistry()
        {
            For<ISessionFactory>().Use(NHibernateSetup.SessionFactory);
            For<ISession>().HybridHttpOrThreadLocalScoped()
                .Use(context => context.GetInstance<ISessionFactory>().OpenSession());
        }
    }

    public class MyFubuMvcPolicies : FubuRegistry
    {
        public MyFubuMvcPolicies()
        {
            Routes.HomeIs<HomeRequest>();
            Actions.IncludeClassesSuffixedWithEndpoint();
        }
    }
}