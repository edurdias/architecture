namespace AdventureWorks.Domain.Contracts.Repositories
{
    public interface IDomainRepository<T> where T : IDomain<T>
    {
        T Get(int id);

        void Add(T domain);

        void Update(T domain);

        void Remove(T domain);
    }
}