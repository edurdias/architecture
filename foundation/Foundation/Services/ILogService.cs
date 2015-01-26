using System;

namespace AdventureWorks.Foundation.Services
{
    public interface ILogService
    {
        void Error(Exception exception);

        void Info(string message);
    }
}