using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Standard.Util.Collection.IEnumerable
{
    public static class IEnumerable
    {
        public static void UpdateElementCollection<T>(this IEnumerable<T> lst, T item) where T : class
        {
            if (lst != null && item != null)
            {
                var elemento = lst.FirstOrDefault(a => a == item);
                var index = lst.ToList().IndexOf(item);
                lst.ToList().Remove(item);
                lst.ToList().Insert(index, item);
            }
        }

    }
}
