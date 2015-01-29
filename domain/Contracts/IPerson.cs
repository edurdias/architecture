using System;
using AdventureWorks.Foundation;

namespace AdventureWorks.Domain.Contracts
{
    public interface IPerson : IPaginable<IPerson>, IDomain<IPerson>
    {
        string Type { get; set; }
        
        string FirstName { get; set; }
        
        string LastName { get; set; }
        
        DateTime ModifiedDate { get; set; }
    }
}