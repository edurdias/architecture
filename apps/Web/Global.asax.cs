using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AdventureWorks.Foundation;
using AdventureWorks.Foundation.Data;
using AdventureWorks.Infrastructure.Data;
using Autofac;
using Autofac.Integration.Mvc;

namespace AdventureWorks.Apps.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var resolver = new AutofacDependencyResolver(Ioc.Container, builder => builder
                .RegisterType<IDataContext>()
                .AsImplementedInterfaces()
                .InstancePerRequest());

            Ioc.Provider = new WebScopeProvider(resolver);
            DependencyResolver.SetResolver(resolver);
        }
    }

    public class WebScopeProvider : IScopeProvider
    {
        public WebScopeProvider(AutofacDependencyResolver resolver)
        {
            Resolver = resolver;
        }

        public AutofacDependencyResolver Resolver { get; set; }

        public ILifetimeScope BeginScope()
        {
            return Resolver.RequestLifetimeScope.BeginLifetimeScope();
        }

        public ILifetimeScope BeginScope(string scopeName)
        {
            return Resolver.RequestLifetimeScope.BeginLifetimeScope(scopeName);
        }
    }
}
