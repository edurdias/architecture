using System;
using System.Collections.Generic;
using AdventureWorks.Domain.Contracts;
using AdventureWorks.Domain.Contracts.Repositories;
using AdventureWorks.Infrastructure.Data;
using Foundation.Services;

namespace AdventureWorks.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository, ITranslator<IProduct, Product>
    {
        public IProduct Get(int id)
        {
            using (var context = new AdventureWorksContext())
                return Translate(context.Products.Find(id));
        }

        public IList<IProduct> GetAll(int skip = 0, int take = Int32.MaxValue)
        {
            throw new NotImplementedException();
        }

        public void Add(IProduct person)
        {
            throw new NotImplementedException();
        }

        public void Update(IProduct person)
        {
            throw new NotImplementedException();
        }

        public void Remove(IProduct person)
        {
            throw new NotImplementedException();
        }

        public int CountAll()
        {
            throw new NotImplementedException();
        }

        public Product Translate(IProduct instance)
        {
            throw new NotImplementedException();
        }

        public IProduct Translate(Product instance)
        {
            throw new NotImplementedException();
        }
    }
}