using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using Hefesoft.Standard.Extensiones;

namespace Hefesoft.Standard.Util.Convertir_Entidades
{
    public static class Convertir_Entidades
    {
        /// <summary>
        /// Transforma una entidad en otra copiando sus propiedades
        /// </summary>
        /// <typeparam name="P" a la que sedea convertir></typeparam>
        /// <typeparam name="T" Fuente></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static P ConvertirEntidades<P, T>(this T source)
            where T : class
            where P : class
        {
            P Entidad = null;
            try
            {
                Mapper.CreateMap<T, P>();
                Entidad = Mapper.DynamicMap<P>(source);
            }
            catch
            {

            }

            return Entidad;
        }

        public static P ConvertirEntidades<P, T>(T source, P Transform)
            where T : class
            where P : class
        {
            P Entidad = null;
            try
            {
                Mapper.CreateMap<T, P>();
                Entidad = Mapper.DynamicMap<P>(source);

                if (HasProperty(source, "RowKey") && HasProperty(source, "Identificador"))
                {
                    var identificador = Convert.ToInt64((source.GetType().GetProperty("RowKey").GetValue(source, null)));
                    PropertyInfo propertyInfo = Entidad.GetType().GetProperty("Identificador");
                    propertyInfo.SetValue(Entidad, identificador, null);
                }
            }
            catch
            {

            }

            return Entidad;
        }

        public static ObservableCollection<P> ConvertirObservables<P, T>(this ObservableCollection<T> source, ObservableCollection<P> lst)
            where T : class
            where P : class
        {

            if (source != null)
            {
                foreach (var item in source)
                {
                    try
                    {
                        Mapper.CreateMap<T, P>();
                        P elemento = Mapper.DynamicMap<P>(item);
                        lst.Add(elemento);
                    }
                    catch
                    {

                    }
                }
            }

            return lst;
        }

        public static List<P> ConvertirIEnumerable<P, T>(this IEnumerable<T> source, List<P> lst)
            where T : class
            where P : class
        {

            if (source != null)
            {
                lst = new List<P>();
                foreach (var item in source)
                {
                    try
                    {
                        Mapper.CreateMap<T, P>().IgnoreAllNonExisting();
                        P elemento = Mapper.DynamicMap<P>(item);
                        lst.Add(elemento);
                    }
                    catch
                    {

                    }
                }
            }

            return lst;
        }

        public static bool HasProperty(this object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName) != null;
        }
    }
}
