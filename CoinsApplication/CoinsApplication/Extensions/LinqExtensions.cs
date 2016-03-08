using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CoinsApplication.Extensions
{
    public static class LinqExtensions
    {
        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }
    }
}
