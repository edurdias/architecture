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

        public int Id { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        public decimal StandardCost { get; set; }

        public decimal ListPrice { get; set; }
        
        public bool InHouse { get; set; }
        
        public bool IsSalable { get; set; }
        
        public int SafetyStockLevel { get; set; }
        
        public int ReorderPoint { get; set; }
        
        public int DaysToManufacture { get; set; }
        
        public DateTime SellStartDate { get; set; }
        
        public IProductModel Model { get; set; }

        public IProductSubCategory SubCategory { get; set; }

        public DateTime ModifiedDate { get; set; }

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
            if (Id != 0)
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