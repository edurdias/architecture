using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Core;

namespace AdventureWorks.Foundation
{
    public class IocScope : IDisposable
    {
        public IocScope(ILifetimeScope scope)
        {
            Scope = scope;
        }

        public ILifetimeScope Scope { get; set; }

        public T Resolve<T>()
        {
            return Scope.Resolve<T>();
        }

        public T Resolve<T>(IEnumerable<Parameter> @params)
        {
            return Scope.Resolve<T>(@params);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                Scope.Dispose();
        }
    }
}