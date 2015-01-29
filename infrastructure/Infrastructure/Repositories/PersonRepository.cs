using System;
using System.Collections.Generic;
using System.Linq;
using AdventureWorks.Domain.Contracts;
using AdventureWorks.Domain.Contracts.Repositories;
using AdventureWorks.Foundation;
using AdventureWorks.Foundation.Data;
using AdventureWorks.Foundation.Services;
using AdventureWorks.Infrastructure.Data;
using Foundation.Services;

namespace AdventureWorks.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository, ITranslator<IPerson, Person>
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
            return Translate(Context.Set<Person>().Find(id));
        }

        public IList<IPerson> GetAll(int skip = 0, int take = int.MaxValue)
        {
            return Context.Set<Person>()
                    .OrderBy(p => p.BusinessEntityID)
                    .Skip(skip)
                    .Take(take)
                    .ToArray()
                    .Select(Translate)
                    .ToList();
        }

        public int CountAll()
        {
            return Context.Set<Person>().Count();
        }

        public void Add(IPerson person)
        {
            Context.Set<Person>().Add(Translate(person));
            Context.SaveChanges();
        }

        public void Update(IPerson person)
        {
            var entity = Translate(person);
            Context.MarkAsModified(entity);
            Context.SaveChanges();
        }

        public void Remove(IPerson person)
        {
            var entity = Context.Set<Person>().Find(person.Id);
            Context.Set<Person>().Remove(entity);
            Context.SaveChanges();
        }

        public Person Translate(IPerson instance)
        {
            if (instance == null)
                return null;

            return new Person
            {
                BusinessEntityID = instance.Id,
                PersonType = instance.Type ?? "EM",
                FirstName = instance.FirstName,
                LastName = instance.LastName,
                ModifiedDate = DateTime.Now,
                rowguid = Guid.NewGuid(),

                BusinessEntity = new BusinessEntity
                {
                    BusinessEntityID = instance.Id,
                    rowguid = Guid.NewGuid(),
                    ModifiedDate = DateTime.Now
                }
            };
        }

        public IPerson Translate(Person instance)
        {
            if (instance == null)
                return null;

            var person = Ioc.Resolve<IPerson>();
            person.Id = instance.BusinessEntityID;
            person.Type = instance.PersonType;
            person.FirstName = instance.FirstName;
            person.LastName = instance.LastName;

            return person;
        }
    }
}
