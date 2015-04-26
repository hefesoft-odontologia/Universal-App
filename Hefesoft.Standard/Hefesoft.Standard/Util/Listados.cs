using Hefesoft.Standard.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;


    public class Listados
    {
        public static void actualizarListado<T>(string rowKey, List<T> lst, T entidadActualizar) where T : IEntidadBase
        {
            var elementoActualizar = lst.FirstOrDefault(a => a.RowKey == rowKey);
            var indexElementoActualizar = lst.IndexOf(elementoActualizar);
            lst.Remove(elementoActualizar);
            elementoActualizar = entidadActualizar;
            lst.Insert(indexElementoActualizar, elementoActualizar);
        }

        public static void actualizarListado<T>(string rowKey, ObservableCollection<T> lst, T entidadActualizar) where T : IEntidadBase
        {
            var elementoActualizar = lst.FirstOrDefault(a => a.RowKey == rowKey);
            var indexElementoActualizar = lst.IndexOf(elementoActualizar);
            lst.Remove(elementoActualizar);
            elementoActualizar = entidadActualizar;
            lst.Insert(indexElementoActualizar, elementoActualizar);
        }

        public static void eliminarPorRowKey<T>(string rowKey, List<T> lst) where T : IEntidadBase
        {
            var existe = lst.Any(a => a.RowKey == rowKey);
            if(existe)
            {
                var item = lst.First(a=>a.RowKey == rowKey);
                lst.Remove(item);
            }
        }

        public static void eliminarPorRowKey<T>(string rowKey, ObservableCollection<T> lst) where T : IEntidadBase
        {
            var existe = lst.Any(a => a.RowKey == rowKey);
            if (existe)
            {
                var item = lst.First(a => a.RowKey == rowKey);
                lst.Remove(item);
            }
        }
    }

