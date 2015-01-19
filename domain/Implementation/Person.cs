using System.Collections.Generic;
using AdventureWorks.Domain.Contracts;
using AdventureWorks.Domain.Contracts.Repositories;
using AdventureWorks.Foundation;

namespace AdventureWorks.Domain.Implementation
{
    public class Person : IPerson
    {
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string Type { get; set; }
        public string LastName { get; set; }

        public IPerson Load(int id)
        {
            return Ioc.Resolve<IPersonRepository>().Get(id);
        }

        public IList<IPerson> GetAll(int skip = 0, int take = int.MaxValue)
        {
            return Ioc.Resolve<IPersonRepository>().GetAll(skip, take);
        }

        public int CountAll()
        {
            return Ioc.Resolve<IPersonRepository>().CountAll();
        }

        public void Save()
        {
            var repository = Ioc.Resolve<IPersonRepository>();
            if (Id.HasValue)
                repository.Update(this);
            else
                repository.Add(this);
        }

        public void Remove()
        {
            Ioc.Resolve<IPersonRepository>().Remove(this);
        }
    }
}
