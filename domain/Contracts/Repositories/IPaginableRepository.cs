using System.Collections.Generic;

namespace AdventureWorks.Domain.Contracts.Repositories
{
    public interface IPaginableRepository<T>
    {
        IList<T> GetAll(int skip = 0, int take = int.MaxValue);

        int CountAll();
    }
}