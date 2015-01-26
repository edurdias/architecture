using Autofac;

namespace AdventureWorks.Foundation
{
    public interface IScopeProvider
    {
        ILifetimeScope BeginScope();

        ILifetimeScope BeginScope(string scopeName);
    }
}