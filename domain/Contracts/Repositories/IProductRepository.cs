using System.Collections.Generic;

namespace AdventureWorks.Domain.Contracts.Repositories
{
    public interface IProductRepository
    {
        IProduct Get(int id);

        IList<IProduct> GetAll(int skip = 0, int take = int.MaxValue);

        void Add(IProduct person);

        void Update(IProduct person);

        void Remove(IProduct person);

        int CountAll();
    }
}