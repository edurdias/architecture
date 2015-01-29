using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AdventureWorks.Domain.Contracts;

namespace AdventureWorks.Apps.Web.ViewModels.Person
{
    public class AddViewModel : IDomainConvertible<IPerson>
    {
        public AddViewModel()
        {
        }

        public AddViewModel(IPerson domain)
        {
            Domain = domain;
        }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        private IPerson Domain { get; set; }

        public virtual IPerson ToDomain()
        {
            Domain.FirstName = FirstName;
            Domain.LastName = LastName;

            Domain.Type = "EM";
            Domain.ModifiedDate = DateTime.Now;
            
            return Domain;
        }
    }
}