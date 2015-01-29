using System;
using AdventureWorks.Foundation;

namespace AdventureWorks.Domain.Contracts
{
    public interface IProduct : IPaginable<IProduct>, IDomain<IProduct>
    {
        string Name { get; set; }

        string Number { get; set; }

        decimal StandardCost { get; set; }

        decimal ListPrice { get; set; }

        bool InHouse { get; set; }

        bool IsSalable { get; set; }

        int SafetyStockLevel { get; set; }

        int ReorderPoint { get; set; }

        int DaysToManufacture { get; set; }

        DateTime SellStartDate { get; set; }

        IProductModel Model { get; set; }

        IProductSubCategory SubCategory { get; set; }

        DateTime ModifiedDate { get; set; }
    }
}