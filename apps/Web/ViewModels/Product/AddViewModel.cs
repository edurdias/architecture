using AdventureWorks.Domain.Contracts;
using AdventureWorks.Foundation;

namespace AdventureWorks.Apps.Web.ViewModels.Product
{
    public class AddViewModel : IDomainConvertible<IProduct>
    {
        public IProduct ToDomain()
        {
            var domain = Ioc.Resolve<IProduct>();
            return domain;
        }
    }
}