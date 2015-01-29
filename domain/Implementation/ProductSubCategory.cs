using System;
using System.Collections.Generic;
using AdventureWorks.Domain.Contracts;
using AdventureWorks.Domain.Contracts.Repositories;

namespace AdventureWorks.Domain.Implementation
{
    public class ProductSubCategory : IProductSubCategory
    {
        public ProductSubCategory(IProductSubCategoryRepository repository)
        {
            Repository = repository;
        }

        private IProductSubCategoryRepository Repository { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }

        public IProductCategory Parent { get; set; }

        public IProductSubCategory Load(int id)
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

        public IList<IProductSubCategory> GetAll(int skip = 0, int take = Int32.MaxValue)
        {
            return Repository.GetAll(skip, take);
        }

        public int CountAll()
        {
            return Repository.CountAll();
        }
    }
}