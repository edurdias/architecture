using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace AdventureWorks.Foundation.Data
{
    public class DataSetWrapper<T> : IDataSet<T> where T : class
    {
        public DataSetWrapper(IDbSet<T> internalSet)
        {
            InternalSet = internalSet;
        }

        private IDbSet<T> InternalSet { get; set; }

        public T Find(object key)
        {
            return InternalSet.Find(key);
        }

        public T Add(T entity)
        {
            return InternalSet.Add(entity);
        }

        public T Remove(T entity)
        {
            return InternalSet.Remove(entity);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return InternalSet.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)InternalSet).GetEnumerator();
        }

        public Expression Expression
        {
            get { return InternalSet.Expression; }
        }

        public Type ElementType
        {
            get { return InternalSet.ElementType; }
        }

        public IQueryProvider Provider
        {
            get { return InternalSet.Provider; }
        }
    }
}