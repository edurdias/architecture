using System;
using System.Collections.Generic;
using System.Linq;
using AdventureWorks.Domain.Contracts;
using AdventureWorks.Domain.Contracts.Repositories;
using AdventureWorks.Foundation;
using AdventureWorks.Foundation.Data;
using AdventureWorks.Infrastructure.Data;
using Foundation.Services;

namespace AdventureWorks.Infrastructure.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository, ITranslator<IProductCategory, ProductCategory>
    {
        public ProductCategoryRepository(IDataContext context)
        {
            Context = context;
        }

        private IDataContext Context { get; set; }

        public ProductCategory Translate(IProductCategory instance)
        {
            if (instance == null)
                return null;

            return new ProductCategory
            {
                ProductCategoryID = instance.Id.HasValue ? instance.Id.Value : 0,
                Name = instance.Name
            };
        }

        public IProductCategory Translate(ProductCategory instance)
        {
            if (instance == null)
                return null;

            var domain = Ioc.Resolve<IProductCategory>();
            domain.Id = instance.ProductCategoryID;
            domain.Name = instance.Name;
            return domain;
        }

        public IList<IProductCategory> GetAll(int skip = 0, int take = Int32.MaxValue)
        {
            return Context.Set<ProductCategory>()
                .OrderBy(c => c.ProductCategoryID)
                .Skip(skip)
                .Take(take)
                .ToArray()
                .Select(Translate)
                .ToList();
        }

        public int CountAll()
        {
            return Context.Set<ProductCategory>().Count();
        }
    }
}