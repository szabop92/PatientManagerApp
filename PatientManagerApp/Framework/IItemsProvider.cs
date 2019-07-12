using System.Collections.Generic;

namespace PatientManagerApp.Framework
{
    public interface IItemsProvider<T>
    {
        int FetchCount();

        IList<T> FetchRange(int startIndex, int count);
    }
}
