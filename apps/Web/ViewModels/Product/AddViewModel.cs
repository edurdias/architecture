using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AdventureWorks.Domain.Contracts;
using AdventureWorks.Foundation;

namespace AdventureWorks.Apps.Web.ViewModels.Product
{
    public class AddViewModel : IDomainConvertible<IProduct>
    {
        [Required]
        [DisplayName("Name")]
        public string Name { get; set; }

        [Required]
        [DisplayName("Identification Number")]
        public string Number { get; set; }

        [Required]
        [DisplayName("Standard Cost")]
        public decimal? StandardCost { get; set; }

        [Required]
        [DisplayName("List Price")]
        public decimal? ListPrice { get; set; }

        [Required]
        [DisplayName("Model")]
        public int? ModelId { get; set; }

        public string ModelName { get; set; }

        [Required]
        [DisplayName("Sub-Category")]
        public int? SubCategoryId { get; set; }

        public string SubCategoryName { get; set; }

        [Required]
        [DisplayName("Safety Stock Level")]
        public int? SafetyStockLevel { get; set; }

        [Required]
        [DisplayName("Reorder Point")]
        public int? ReorderPoint { get; set; }

        [Required]
        [DisplayName("Days to Manufacture")]
        public int? DaysToManufacture { get; set; }

        [Required]
        [DisplayName("Sell Start Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? SellStartDate { get; set; }

        public virtual IProduct ToDomain()
        {
            return UpdateDomain(Ioc.Resolve<IProduct>());
        }

        protected IProduct UpdateDomain(IProduct domain)
        {
            domain.Name = Name;
            domain.Number = Number;
            domain.StandardCost = StandardCost.HasValue ? StandardCost.Value : 0;
            domain.ListPrice = ListPrice.HasValue ? ListPrice.Value : 0;
            domain.DaysToManufacture = DaysToManufacture.HasValue ? DaysToManufacture.Value : 0;
            domain.ReorderPoint = ReorderPoint.HasValue ? ReorderPoint.Value : 0;
            domain.SafetyStockLevel = SafetyStockLevel.HasValue ? SafetyStockLevel.Value : 0;
            domain.SellStartDate = SellStartDate.HasValue ? SellStartDate.Value : DateTime.Today;

            if (ModelId.HasValue)
            {
                var modelDomain = Ioc.Resolve<IProductModel>();
                domain.Model = modelDomain.Load(ModelId.Value);
            }

            if (SubCategoryId.HasValue)
            {
                var subCategoryDomain = Ioc.Resolve<IProductSubCategory>();
                domain.SubCategory = subCategoryDomain.Load(SubCategoryId.Value);
            }

            domain.ModifiedDate = DateTime.Now;

            return domain;
        }
    }
}