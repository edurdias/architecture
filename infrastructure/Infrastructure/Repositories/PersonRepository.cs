using System.Collections.Generic;
using System.Linq;
using AdventureWorks.Domain.Contracts;
using AdventureWorks.Domain.Contracts.Repositories;
using AdventureWorks.Foundation.Data;
using AdventureWorks.Foundation.Services;
using AdventureWorks.Infrastructure.Data;

namespace AdventureWorks.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        public PersonRepository(ILogService log, IDataContext context)
        {
            Log = log;
            Context = context;
        }

        public IDataContext Context { get; set; }

        public ILogService Log { get; set; }

        public IPerson Get(int id)
        {
            return Context.Set<Person>().Find(id);
        }

        public IList<IPerson> GetAll(int skip = 0, int take = int.MaxValue)
        {
            return Context.Set<Person>()
                .OrderBy(p => p.Id)
                .Skip(skip)
                .Take(take)
                .ToArray()
                .OfType<IPerson>()
                .ToList();
        }

        public int CountAll()
        {
            return Context.Set<Person>().Count();
        }

        public void Add(IPerson person)
        {
            Context.Set<Person>().Add((Person)person);
            Context.SaveChanges();
        }

        public void Update(IPerson person)
        {
            Context.MarkAsModified(person);
            Context.SaveChanges();
        }

        public void Remove(IPerson person)
        {
            var entity = Context.Set<Person>().Find(person.Id);
            Context.Set<Person>().Remove(entity);
            Context.SaveChanges();
        }
    }
}
