namespace AdventureWorks.Foundation.Services
{
    public interface IMessageDisplayService
    {
        void Success(string message);

        void Error(string message);
    }
}