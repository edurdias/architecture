using Autofac;

namespace AdventureWorks.Foundation
{
    public class IocScopeProvider : IScopeProvider
    {
        public IocScopeProvider(ILifetimeScope lifetimeScope)
        {
            Scope = lifetimeScope;
        }

        public ILifetimeScope Scope { get; set; }

        public ILifetimeScope BeginScope()
        {
            return Scope.BeginLifetimeScope();
        }

        public ILifetimeScope BeginScope(string scopeName)
        {
            return Scope.BeginLifetimeScope(scopeName);
        }
    }
}