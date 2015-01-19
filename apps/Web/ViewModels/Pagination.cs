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

        public int Offset { get; set; }
        
        public int Limit { get; set; }

        public int total { get; private set; }

        public IEnumerable<TViewModel> rows { get; private set; } 

        public void Paginate(Func<TDomain, TViewModel> expression) 
        {
            var domain = Ioc.Resolve<TDomain>() as IPaginable<TDomain>;
            if(domain != null)
            {
                rows = domain.GetAll(Offset, Limit).Select(expression);
                total = domain.CountAll();
            }
        }
    }
}