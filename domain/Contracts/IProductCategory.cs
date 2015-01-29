using AdventureWorks.Foundation;

namespace AdventureWorks.Domain.Contracts
{
    public interface IProductCategory : IPaginable<IProductCategory>
    {
        int? Id { get; set; }

        string Name { get; set; }
    }
}