using AdventureWorks.Foundation;

namespace AdventureWorks.Domain.Contracts
{
    public interface IProductSubCategory : IDomain<IProductSubCategory>, IPaginable<IProductSubCategory>
    {
        string Name { get; set; }

        IProductCategory Parent { get; set; }
    }
}