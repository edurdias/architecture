using System.Collections.Generic;
using AdventureWorks.Domain.Contracts;
using AdventureWorks.Domain.Contracts.Repositories;

namespace AdventureWorks.Domain.Implementation
{
    public class Person : IPerson
    {
        public Person(IPersonRepository repository)
        {
            Repository = repository;
        }

        public int? Id { get; set; }
        
        public string FirstName { get; set; }
        
        public string Type { get; set; }
        
        public string LastName { get; set; }
        
        private IPersonRepository Repository { get; set; }

        public IPerson Load(int id)
        {
            return Repository.Get(id);
        }

        public IList<IPerson> GetAll(int skip = 0, int take = int.MaxValue)
        {
            return Repository.GetAll(skip, take);
        }

        public int CountAll()
        {
            return Repository.CountAll();
        }

        public void Save()
        {
            if (Id.HasValue)
                Repository.Update(this);
            else
                Repository.Add(this);
        }

        public void Remove()
        {
            Repository.Remove(this);
        }
    }
}
