using System;
using System.Collections.Generic;
using AdventureWorks.Domain.Contracts;
using AdventureWorks.Domain.Contracts.Repositories;

namespace AdventureWorks.Domain.Implementation
{
    public class Product : IProduct
    {
        public Product(IProductRepository repository)
        {
            Repository = repository;
        }

        public IProductRepository Repository { get; set; }

        public int? Id { get; set; }

        public IProduct Load(int id)
        {
            return Repository.Get(id);
        }

        public IList<IProduct> GetAll(int skip = 0, int take = int.MaxValue)
        {
            return Repository.GetAll(skip, take);
        }

        public int CountAll()
        {
            return Repository.CountAll();
        }

        public void Save()
        {
            if (Id.HasValue)
                Repository.Update(this);
            else
                Repository.Add(this);
        }

        public void Remove()
        {
            Repository.Remove(this);
        }
    }
}