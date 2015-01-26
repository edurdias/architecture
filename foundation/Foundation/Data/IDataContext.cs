namespace AdventureWorks.Foundation.Data
{
    public interface IDataContext
    {
        IDataSet<T> Set<T>() where T : class;

        void SaveChanges();

        void MarkAsModified(object entity);
    }
}