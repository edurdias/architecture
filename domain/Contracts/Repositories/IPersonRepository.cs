using System.Collections.Generic;

namespace AdventureWorks.Domain.Contracts.Repositories
{
    public interface IPersonRepository
    {
        IPerson Get(int id);
        
        IList<IPerson> GetAll(int skip = 0, int take = int.MaxValue);
        
        void Add(IPerson person);
        
        void Update(IPerson person);
        
        void Remove(IPerson person);

        int CountAll();
    }
}