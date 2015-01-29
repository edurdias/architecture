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
    public class ProductRepository : IProductRepository, ITranslator<IProduct, Product>
    {
        public ProductRepository(IDataContext context)
        {
            Context = context;
        }

        public IDataContext Context { get; set; }

        public IList<IProduct> GetAll(int skip = 0, int take = Int32.MaxValue)
        {
            return Context.Set<Product>()
                .OrderBy(p => p.ProductID)
                .Skip(skip)
                .Take(take)
                .ToArray()
                .Select(Translate)
                .ToList();
        }

        public int CountAll()
        {
            return Context.Set<Product>().Count();
        }

        public IProduct Get(int id)
        {
            return Translate(Context.Set<Product>().Find(id));
        }

        public void Add(IProduct domain)
        {
            Context.Set<Product>().Add(Translate(domain));
            Context.SaveChanges();
        }

        public void Update(IProduct domain)
        {
            var entity = Translate(domain);
            Context.MarkAsModified(entity);
            Context.SaveChanges();
        }

        public void Remove(IProduct domain)
        {
            var entity = Context.Set<Product>().Find(domain.Id);
            Context.Set<Product>().Remove(entity);
            Context.SaveChanges();
        }

        public Product Translate(IProduct instance)
        {
            if (instance == null)
                return null;

            return new Product
            {
                ProductID = instance.Id,
                Name = instance.Name,
                ProductNumber = instance.Number,
                StandardCost = instance.StandardCost,
                ListPrice = instance.ListPrice,
                ModifiedDate = instance.ModifiedDate,
                DaysToManufacture = instance.DaysToManufacture,
                ReorderPoint = (short) instance.ReorderPoint,
                SafetyStockLevel = (short) instance.SafetyStockLevel,
                SellStartDate = instance.SellStartDate,
                ProductModelID = instance.Model.Id,
                ProductSubcategoryID = instance.SubCategory.Id
            };
        }

        public IProduct Translate(Product instance)
        {
            if (instance == null)
                return null;

            var domain = Ioc.Resolve<IProduct>();

            domain.Id = instance.ProductID;
            domain.Name = instance.Name;
            domain.Number = instance.ProductNumber;
            domain.StandardCost = instance.StandardCost;
            domain.ListPrice = instance.ListPrice;

            var modelTranslator = Ioc.Resolve<ITranslator<IProductModel, ProductModel>>();
            domain.Model = modelTranslator.Translate(instance.ProductModel);

            var subCategoryTranslator = Ioc.Resolve<ITranslator<IProductSubCategory, ProductSubcategory>>();
            domain.SubCategory = subCategoryTranslator.Translate(instance.ProductSubcategory);

            domain.ModifiedDate = instance.ModifiedDate;
            domain.DaysToManufacture = instance.DaysToManufacture;
            domain.ReorderPoint = instance.ReorderPoint;
            domain.SafetyStockLevel = instance.SafetyStockLevel;
            domain.SellStartDate = instance.SellStartDate;

            return domain;
        }
    }
}