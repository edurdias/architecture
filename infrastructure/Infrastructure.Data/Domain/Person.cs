using System;
using System.Collections.Generic;
using System.Transactions;
using AdventureWorks.Domain.Contracts;
using AdventureWorks.Domain.Contracts.Repositories;

namespace AdventureWorks.Infrastructure.Data
{
    public partial class Person : IPerson
    {
        public Person(IPersonRepository repository)
            : this()
        {
            Repository = repository;
        }

        public IPersonRepository Repository { get; set; }

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
            using (var transaction = new TransactionScope())
            {
                if (Id != 0)
                    Repository.Update(this);
                else
                {
                    BusinessEntity = new BusinessEntity
                    {
                        rowguid = Guid.NewGuid(),
                        ModifiedDate = ModifiedDate
                    };

                    Repository.Add(this);
                }

                transaction.Complete();
            }
        }

        public void Remove()
        {
            Repository.Remove(this);
        }
    }
}
