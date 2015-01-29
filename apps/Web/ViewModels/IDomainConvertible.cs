namespace AdventureWorks.Apps.Web.ViewModels
{
    public interface IDomainConvertible<TDomain>
    {
        TDomain ToDomain();
    }
}