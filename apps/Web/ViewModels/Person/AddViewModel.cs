using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AdventureWorks.Domain.Contracts;
using AdventureWorks.Foundation;

namespace AdventureWorks.Apps.Web.ViewModels.Person
{
    public class AddViewModel : IDomainConvertible<IPerson>
    {
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        public virtual IPerson ToDomain()
        {
            var domain = Ioc.Resolve<IPerson>();
            domain.FirstName = FirstName;
            domain.LastName = LastName;
            return domain;
        }
    }
}