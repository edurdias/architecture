using System;
using System.Configuration;
using System.Data.Entity;

namespace AdventureWorks.Foundation.Data
{
    public class DataContextWrapper : IDataContext
    {
        public DataContextWrapper()
        {
            var contextTypeName = ConfigurationManager.AppSettings["DataContext:Type"];
            var contextType = Type.GetType(contextTypeName);
            
            if (contextType == null)
                throw new Exception(string.Format("Cannot find DbContext type {0}", contextTypeName));

            InternalContext = (DbContext) Activator.CreateInstance(contextType);
        }

        public DataContextWrapper(DbContext context)
        {
            InternalContext = context;
        }

        public DbContext InternalContext { get; set; }

        public IDataSet<T> Set<T>() where T : class
        {
            return new DataSetWrapper<T>(InternalContext.Set<T>());
        }

        public void SaveChanges()
        {
            InternalContext.SaveChanges();
        }

        public void MarkAsModified(object entity)
        {
            InternalContext.Entry(entity).State = EntityState.Modified;
        }
    }
}