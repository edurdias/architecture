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
    public class ProductModelRepository : IProductModelRepository, ITranslator<IProductModel, ProductModel>
    {
        public ProductModelRepository(IDataContext context)
        {
            Context = context;
        }

        public IDataContext Context { get; set; }

        public IList<IProductModel> GetAll(int skip = 0, int take = Int32.MaxValue)
        {
            return Context.Set<ProductModel>()
                .OrderBy(p => p.ProductModelID)
                .Skip(skip)
                .Take(take)
                .ToArray()
                .Select(Translate)
                .ToList();
        }

        public int CountAll()
        {
            return Context.Set<ProductModel>().Count();
        }

        public IProductModel Get(int id)
        {
            return Translate(Context.Set<ProductModel>().Find(id));
        }

        public void Add(IProductModel domain)
        {
            Context.Set<ProductModel>().Add(Translate(domain));
            Context.SaveChanges();
        }

        public void Update(IProductModel domain)
        {
            var entity = Translate(domain);
            Context.MarkAsModified(entity);
            Context.SaveChanges();
        }

        public void Remove(IProductModel domain)
        {
            var entity = Context.Set<ProductModel>().Find(domain.Id);
            Context.Set<ProductModel>().Remove(entity);
            Context.SaveChanges();
        }

        public ProductModel Translate(IProductModel instance)
        {
            if (instance == null)
                return null;

            return new ProductModel
            {
                ProductModelID = instance.Id,
                Name = instance.Name
            };
        }

        public IProductModel Translate(ProductModel instance)
        {
            if (instance == null)
                return null;

            var domain = Ioc.Resolve<IProductModel>();

            domain.Id = instance.ProductModelID;
            domain.Name = instance.Name;

            return domain;
        }
    }
}