using System.Collections.Generic;

namespace AdventureWorks.Domain.Contracts.Repositories
{
    public interface IProductRepository
    {
        IProduct Get(int id);

        IList<IProduct> GetAll(int skip = 0, int take = int.MaxValue);

        int CountAll();

        void Add(IProduct domain);

        void Update(IProduct domain);

        void Remove(IProduct domain);
    }
}