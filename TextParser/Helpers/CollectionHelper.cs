using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextParser.Helpers
{
    public static class CollectionHelper
    {
        public static void AddRange<T>(this ICollection<T> destination, IEnumerable<T> source)
        {
            foreach (var item in source)
            {
                destination.Add(item);
            }
        }
    }
}
