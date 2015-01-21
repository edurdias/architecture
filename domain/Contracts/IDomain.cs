namespace AdventureWorks.Domain.Contracts
{
    public interface IDomain<out T>
    {
        int? Id { get; set; }

        T Load(int id);

        void Save();
        
        void Remove();
    }
}