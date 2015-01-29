using System;
using System.Collections.Generic;
using System.Linq;
using AdventureWorks.Foundation;

namespace AdventureWorks.Apps.Web.ViewModels
{
    public class Pagination<TDomain, TViewModel> where TDomain : IPaginable<TDomain>
    {
        public Pagination()
        {
            Limit = 10;
        }

        public Pagination(IPaginable<TDomain> domain) : this()
        {
            Domain = domain;
        }

        public int Offset { get; set; }
        
        public int Limit { get; set; }

        public int total { get; private set; }

        public IEnumerable<TViewModel> rows { get; private set; }

        private IPaginable<TDomain> Domain { get; set; }

        public void Paginate(Func<TDomain, TViewModel> expression)
        {
            if (Domain == null)
                Domain = Ioc.Resolve<TDomain>();

            rows = Domain.GetAll(Offset, Limit).Select(expression);
            total = Domain.CountAll();
        }
    }
}