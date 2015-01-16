using Domain.Contracts;
using Foundation;
using Foundation.Services;

namespace Web.ViewModels.Person
{
    public class AddViewModel
    {
        public IPerson ToDomain()
        {
            return Ioc.Resolve<IPerson>();
        }
    }
}