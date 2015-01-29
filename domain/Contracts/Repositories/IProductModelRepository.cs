namespace AdventureWorks.Domain.Contracts.Repositories
{
    public interface IProductModelRepository : IDomainRepository<IProductModel>, IPaginableRepository<IProductModel>
    {
    }
}