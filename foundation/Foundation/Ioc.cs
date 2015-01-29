using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using Autofac;

namespace AdventureWorks.Foundation
{
    public static class Ioc
    {
        private static IContainer container;
        private static ContainerBuilder containerBuilder;
        private static IScopeProvider provider;

        public static IContainer Container
        {
            get { return container ?? (container = ContainerBuilder.Build()); }
            set { container = value; }
        }

        public static IScopeProvider Provider
        {
            get { return provider ?? (provider = new IocScopeProvider(Container)); }
            set { provider = value; }
        }

        public static ContainerBuilder ContainerBuilder
        {
            get
            {
                if (containerBuilder == null)
                {
                    containerBuilder = new ContainerBuilder();

                    var assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory)
                        .Where(f => Path.GetExtension(f) == ".dll")
                        .Select(Assembly.LoadFrom)
                        .Where(asm => asm.FullName.StartsWith("AdventureWorks"));

                    foreach (var assembly in assemblies)
                    {
                        containerBuilder.RegisterAssemblyTypes(assembly)
                            .AsImplementedInterfaces();
                    }
                }
                return containerBuilder;
            }
            set { containerBuilder = value; }
        }

        public static T Resolve<T>()
        {
            return BeginScope().Resolve<T>();
        }

        public static T Resolve<T>(object parameters)
        {
            var properties = parameters.GetType().GetProperties();
            var @params = properties.Select(p => new NamedParameter(p.Name, p.GetValue(parameters)));
            return BeginScope().Resolve<T>(@params);
        }

        public static IocScope BeginScope()
        {
            return new IocScope(Provider.BeginScope());
        }

        public static IocScope BeginScope(string scopeName)
        {
            return new IocScope(Provider.BeginScope(scopeName));
        }
    }
}