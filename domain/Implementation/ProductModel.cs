using System;
using System.Collections.Generic;
using AdventureWorks.Domain.Contracts;
using AdventureWorks.Domain.Contracts.Repositories;

namespace AdventureWorks.Domain.Implementation
{
    public class ProductModel : IProductModel
    {
        public ProductModel(IProductModelRepository repository)
        {
            Repository = repository;
        }

        private IProductModelRepository Repository { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public IProductModel Load(int id)
        {
            return Repository.Get(id);
        }

        public void Save()
        {
            if (Id != 0)
                Repository.Update(this);
            else
                Repository.Add(this);
        }

        public void Remove()
        {
            Repository.Remove(this);
        }

        public IList<IProductModel> GetAll(int skip = 0, int take = Int32.MaxValue)
        {
            return Repository.GetAll(skip, take);
        }

        public int CountAll()
        {
            return Repository.CountAll();
        }
    }
}