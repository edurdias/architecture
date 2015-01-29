using AdventureWorks.Foundation;

namespace AdventureWorks.Domain.Contracts
{
    public interface IProductModel : IDomain<IProductModel>, IPaginable<IProductModel>
    {
        string Name { get; set; }
    }
}