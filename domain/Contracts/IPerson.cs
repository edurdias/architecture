using AdventureWorks.Foundation;

namespace AdventureWorks.Domain.Contracts
{
    public interface IPerson : IPaginable<IPerson>
    {
        int? Id { get; set; }
        string Type { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }

        IPerson Load(int id);

        void Save();

        void Remove();
    }
}