using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using AdventureWorks.Domain.Contracts;
using AdventureWorks.Domain.Contracts.Repositories;
using AdventureWorks.Infrastructure.Data;
using Foundation.Services;
using Person = AdventureWorks.Domain.Implementation.Person;

namespace AdventureWorks.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository, ITranslator<IPerson, Data.Person>
    {
        public IPerson Get(int id)
        {
            using (var context = new Data.AdventureWorks2012())
            {
                return Translate(context.People.Find(id));
            }
        }

        public IList<IPerson> GetAll(int skip = 0, int take = int.MaxValue)
        {
            using (var context = new Data.AdventureWorks2012())
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
            using (var context = new Data.AdventureWorks2012())
            {
                context.People.Add(Translate(person));
                context.SaveChanges();
            }
        }

        public void Update(IPerson person)
        {
            using (var context = new Data.AdventureWorks2012())
            {
                var entity = Translate(person);
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public void Remove(IPerson person)
        {
            using (var context = new Data.AdventureWorks2012())
            {
                var entity = Translate(person);
                context.Entry(entity).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public int CountAll()
        {
            using (var context = new Data.AdventureWorks2012())
            {
                return context.People.Count();
            }
        }

        public Data.Person Translate(IPerson instance)
        {
            if (instance == null)
                return null;

            return new Data.Person
            {
                BusinessEntityID = instance.Id.HasValue ? instance.Id.Value : 0,
                PersonType = instance.Type ?? "EM",
                FirstName = instance.FirstName,
                LastName = instance.LastName,
                ModifiedDate = DateTime.Now,
                rowguid = Guid.NewGuid(),

                BusinessEntity = new BusinessEntity
                {
                    rowguid = Guid.NewGuid(),
                    ModifiedDate = DateTime.Now
                }
            };
        }

        public IPerson Translate(Data.Person instance)
        {
            if (instance == null)
                return null;

            return new Person
            {
                Id = instance.BusinessEntityID,
                Type = instance.PersonType,
                FirstName = instance.FirstName,
                LastName = instance.LastName
            };
        }
    }
}
