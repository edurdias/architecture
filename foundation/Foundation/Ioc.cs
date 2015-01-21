using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using Autofac;

namespace AdventureWorks.Foundation
{
    public static class Ioc
    {
        private static IContainer container;

        public static IContainer Container
        {
            get
            {
                if (container == null)
                {
                    var builder = new ContainerBuilder();

                    var assemblies = BuildManager.GetReferencedAssemblies()
                            .Cast<Assembly>()
                            .Where(asm => asm.FullName.StartsWith("AdventureWorks"));

                    foreach (var assembly in assemblies)
                    {
                        builder.RegisterAssemblyTypes(assembly)
                            .AsImplementedInterfaces();
                    }
                    
                    container = builder.Build();
                }
                return container;
            }
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();

        }
        public static T Resolve<T>(object parameters)
        {
            var properties = parameters.GetType().GetProperties();
            var @params = properties.Select(p => new NamedParameter(p.Name, p.GetValue(parameters)));
            return Container.Resolve<T>(@params);
        }
    }
}