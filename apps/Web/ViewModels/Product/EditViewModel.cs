using AdventureWorks.Domain.Contracts;
using AdventureWorks.Foundation;

namespace AdventureWorks.Apps.Web.ViewModels.Product
{
    public class EditViewModel : IDomainConvertible<IProduct>
    {
        public EditViewModel(IProduct domain)
        {
            
        }

        public IProduct ToDomain()
        {
            var domain = Ioc.Resolve<IProduct>();
            return domain;
        }
    }
}