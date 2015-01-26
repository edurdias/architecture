using System.Linq;

namespace AdventureWorks.Foundation.Data
{
    public interface IDataSet<T> : IQueryable<T> where T : class
    {
        T Find(object key);

        T Add(T entity);

        T Remove(T entity);
    }
}