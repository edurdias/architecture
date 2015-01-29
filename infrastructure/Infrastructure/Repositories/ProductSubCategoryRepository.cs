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
    public class ProductSubCategoryRepository : IProductSubCategoryRepository, ITranslator<IProductSubCategory, ProductSubcategory>
    {
        public ProductSubCategoryRepository(IDataContext context)
        {
            Context = context;
        }

        private IDataContext Context { get; set; }

        public IList<IProductSubCategory> GetAll(int skip = 0, int take = Int32.MaxValue)
        {
            return Context.Set<ProductSubcategory>()
                .OrderBy(p => p.ProductSubcategoryID)
                .Skip(skip)
                .Take(take)
                .ToArray()
                .Select(Translate)
                .ToList();
        }

        public int CountAll()
        {
            return Context.Set<ProductSubcategory>().Count();
        }

        public IProductSubCategory Get(int id)
        {
            return Translate(Context.Set<ProductSubcategory>().Find(id));
        }

        public void Add(IProductSubCategory domain)
        {
            Context.Set<ProductSubcategory>().Add(Translate(domain));
            Context.SaveChanges();
        }

        public void Update(IProductSubCategory domain)
        {
            var entity = Translate(domain);
            Context.MarkAsModified(entity);
            Context.SaveChanges();
        }

        public void Remove(IProductSubCategory domain)
        {
            var entity = Context.Set<ProductSubcategory>().Find(domain.Id);
            Context.Set<ProductSubcategory>().Remove(entity);
            Context.SaveChanges();
        }

        public ProductSubcategory Translate(IProductSubCategory instance)
        {
            if (instance == null)
                return null;

            var parent = instance.Parent;

            return new ProductSubcategory
            {
                ProductCategoryID = parent != null && parent.Id.HasValue ? parent.Id.Value : 0,
                ProductSubcategoryID = instance.Id,
                Name = instance.Name
            };
        }

        public IProductSubCategory Translate(ProductSubcategory instance)
        {
            if (instance == null)
                return null;

            var domain = Ioc.Resolve<IProductSubCategory>();
            domain.Id = instance.ProductSubcategoryID;
            domain.Name = instance.Name;

            var categoryTranslator = Ioc.Resolve<ITranslator<IProductCategory, ProductCategory>>();
            domain.Parent = categoryTranslator.Translate(instance.ProductCategory);

            return domain;
        }
    }
}