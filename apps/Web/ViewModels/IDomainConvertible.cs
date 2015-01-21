namespace AdventureWorks.Apps.Web.ViewModels
{
    public interface IDomainConvertible<out TDomain>
    {
        TDomain ToDomain();
    }
}