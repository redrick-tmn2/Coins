using System.Collections.Generic;
using System.Linq;

namespace CoinsApplication.Extensions
{
    public static class CollectionExtensions
    {
        public static T PreviousOrNext<T>(this ICollection<T> items, T item)
            where T : class 
        {
            T previousItem = null, resultItem = null;
            foreach (var targetItem in items)
            {
                if (targetItem == item)
                {
                    resultItem = previousItem;
                    break;
                }

                previousItem = item;
            }

            if (resultItem != null)
            {
                return resultItem;
            }

            return items.SkipWhile(x => x != item).Skip(1).FirstOrDefault();
        }
    }
}
