using System.Collections.Generic;

namespace AdventureWorks.Foundation
{
    public interface IPaginable<T>
    {
        IList<T> GetAll(int skip = 0, int take = int.MaxValue);

        int CountAll();
    }
}