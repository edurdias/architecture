using AdventureWorks.Domain.Contracts;
using AdventureWorks.Foundation;

namespace AdventureWorks.Apps.Web.ViewModels.Product
{
    public class EditViewModel : AddViewModel
    {
        public EditViewModel()
        {
        }

        public EditViewModel(IProduct domain)
        {
            Id = domain.Id;
            Name = domain.Name;
            Number = domain.Number;
            StandardCost = domain.StandardCost;
            ListPrice = domain.ListPrice;
            DaysToManufacture = domain.DaysToManufacture;
            ReorderPoint = domain.ReorderPoint;
            SafetyStockLevel = domain.SafetyStockLevel;
            SellStartDate = domain.SellStartDate;

            if(domain.Model != null)
            {
                ModelId = domain.Model.Id;
                ModelName = domain.Model.Name;
            }

            if (domain.SubCategory != null)
            {
                SubCategoryId = domain.SubCategory.Id;
                SubCategoryName = domain.SubCategory.Name;
            }
        }

        public int? Id { get; set; }

        public override IProduct ToDomain()
        {
            var domain = Ioc.Resolve<IProduct>();

            if (Id.HasValue)
                domain = domain.Load(Id.Value);

            return UpdateDomain(domain);
        }
    }
}