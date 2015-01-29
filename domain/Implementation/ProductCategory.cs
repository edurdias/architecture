using System;
using System.Collections.Generic;
using AdventureWorks.Domain.Contracts;
using AdventureWorks.Domain.Contracts.Repositories;

namespace AdventureWorks.Domain.Implementation
{
    public class ProductCategory : IProductCategory
    {
        public ProductCategory(IProductCategoryRepository repository)
        {
            Repository = repository;
        }

        private IProductCategoryRepository Repository { get; set; }

        public int? Id { get; set; }
        
        public string Name { get; set; }

        public IList<IProductCategory> GetAll(int skip = 0, int take = Int32.MaxValue)
        {
            return Repository.GetAll(skip, take);
        }

        public int CountAll()
        {
            return Repository.CountAll();
        }
    }
}