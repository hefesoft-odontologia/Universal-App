using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Hefesoft.Standard.Util.Collection.Observables
{
    public static class Observables
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerableList)
        {
            if (enumerableList != null)
            {

                var observableCollection = new ObservableCollection<T>();

                foreach (var item in enumerableList)
                    observableCollection.Add(item);


                return observableCollection;
            }
            return null;
        }

        public static void UpdateElementCollection<T>(this ObservableCollection<T> lst, T item) where T : class
        {
            if (lst != null && item != null)
            {                
                var elemento = lst.FirstOrDefault(a => a == item);
                var index = lst.IndexOf(item);
                lst.Remove(item);
                lst.Insert(index, item);        
            }            
        }
    }
}
