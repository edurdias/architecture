using AdventureWorks.Domain.Contracts;

namespace AdventureWorks.Apps.Web.ViewModels.Person
{
    public class EditViewModel : AddViewModel
    {
        public EditViewModel()
        {
        }

        public EditViewModel(IPerson domain)
        {
            Id = domain.Id;
            FirstName = domain.FirstName;
            LastName = domain.LastName;
        }

        public int? Id { get; set; }

        public override IPerson ToDomain()
        {
            var domain = base.ToDomain();
            if (Id != null) domain.Id = Id.Value;
            return domain;
        }
    }
}