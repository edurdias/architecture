using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AdventureWorks.Domain.Contracts;
using AdventureWorks.Domain.Contracts.Repositories;
using AdventureWorks.Foundation;
using AdventureWorks.Infrastructure.Data;
using Foundation.Services;

namespace AdventureWorks.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository, ITranslator<IPerson, Person>
    {
        public IPerson Get(int id)
        {
            using (var context = new AdventureWorksContext())
            {
                return Translate(context.People.Find(id));
            }
        }

        public IList<IPerson> GetAll(int skip = 0, int take = int.MaxValue)
        {
            using (var context = new AdventureWorksContext())
            {
                return context.People
                    .OrderBy(p => p.BusinessEntityID)
                    .Skip(skip)
                    .Take(take)
                    .ToArray()
                    .Select(Translate)
                    .ToList();
            }
        }

        public void Add(IPerson person)
        {
            using (var context = new AdventureWorksContext())
            {
                context.People.Add(Translate(person));
                context.SaveChanges();
            }
        }

        public void Update(IPerson person)
        {
            using (var context = new AdventureWorksContext())
            {
                var entity = Translate(person);
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Remove(IPerson person)
        {
            using (var context = new AdventureWorksContext())
            {
                var entity = context.People.Find(person.Id);
                context.People.Remove(entity);
                context.SaveChanges();
            }
        }

        public int CountAll()
        {
            using (var context = new AdventureWorksContext())
            {
                return context.People.Count();
            }
        }

        public Person Translate(IPerson instance)
        {
            if (instance == null)
                return null;

            var businessEntityID = instance.Id.HasValue ? instance.Id.Value : 0;

            return new Person
            {
                BusinessEntityID = businessEntityID,
                PersonType = instance.Type ?? "EM",
                FirstName = instance.FirstName,
                LastName = instance.LastName,
                ModifiedDate = DateTime.Now,
                rowguid = Guid.NewGuid(),

                BusinessEntity = new BusinessEntity
                {
                    BusinessEntityID = businessEntityID,
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
