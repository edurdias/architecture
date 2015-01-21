using AdventureWorks.Foundation;

namespace AdventureWorks.Domain.Contracts
{
    public interface IProduct : IPaginable<IProduct>, IDomain<IProduct>
    {
    }
}