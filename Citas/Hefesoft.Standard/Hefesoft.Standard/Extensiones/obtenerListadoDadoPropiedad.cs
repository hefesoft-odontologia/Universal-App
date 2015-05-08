using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Hefesoft.Standard.Extensiones
{
    public static class ObtenerListadoDadoPropiedad
    {
        // Saca una propiedad de un listado y devuelve una propiedad de la misma
        // Ademas llena el rowkey con el identificador
        public static List<P> obtenerListadoDadoPropiedad<T,P>(this IEnumerable<T> lst, string nombrePropiedad) 
            where T : class
            where P : class
        {
            List<P> lstDevolver = new List<P>();
            foreach (var item in lst)
            {
                try
                {
                    var identificador = Convert.ToInt64((item.GetType().GetProperty("RowKey").GetValue(item, null)));
                    var elemento = (P)(item.GetType().GetProperty(nombrePropiedad).GetValue(item, null));

                    PropertyInfo propertyInfo = elemento.GetType().GetProperty("Identificador");
                    propertyInfo.SetValue(elemento, identificador, null);

                    lstDevolver.Add(elemento);
                }
                catch { }
            }

            return lstDevolver;
        }
    }
}
