using Hefesoft.Standard.Interfaces;
using Hefesoft.Standard.Extensiones;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace Hefesoft.Standard.Util.Blob
{
    public static partial class CrudBlob
    {
        public static async Task<T> postBlob<T>(this T entidad) where T : IEntidadBase
        {
            try
            {
                if (string.IsNullOrEmpty(entidad.nombreTabla))
                {
                    entidad.nombreTabla = entidad.GetType().Name.eliminarCaracteresEspeciales().ToLower();
                }
                else
                {
                    entidad.nombreTabla = entidad.nombreTabla.ToLower();
                }

                if (string.IsNullOrEmpty(entidad.PartitionKey))
                {
                    entidad.PartitionKey = entidad.GetType().FullName.eliminarCaracteresEspeciales().ToLower();
                }

                if (string.IsNullOrEmpty(entidad.RowKey))
                {
                    string row = string.Format("{0}{1}{2}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year);
                    entidad.RowKey = entidad.GetType().Namespace.Replace('_', '.').ToLower() + row;
                }

                T valorRetorno;

                string json = JsonConvert.SerializeObject(entidad, Formatting.Indented,
                                new JsonSerializerSettings
                                {
                                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                });

                var resultadoString = await doPost(json);

                valorRetorno = JsonConvert.DeserializeObject<T>(resultadoString);

                return valorRetorno;
            }
            catch (Exception ex)
            {
                throw ex;               
            }
        }


        public static async Task<List<T>> getBlobByPartition<T>(this T entidad) where T : IEntidadBase
        {
            List<T> valorRetorno = null;

            if (string.IsNullOrEmpty(entidad.nombreTabla))
            {
                entidad.nombreTabla = entidad.GetType().Name.eliminarCaracteresEspeciales().ToLower();
            }

            if (string.IsNullOrEmpty(entidad.PartitionKey))
            {
                entidad.PartitionKey = entidad.GetType().FullName.ToLower();
            }

            string json = JsonConvert.SerializeObject(entidad);

            string parameters = string.Format("blob/?nombreTabla={0}&partitionKey={1}", entidad.nombreTabla, entidad.PartitionKey);

            valorRetorno = await doGet<T>(valorRetorno, parameters);

            return valorRetorno;
        }

        public static async Task<List<T>> getBlobByByName<T>(this T entidad, string terminosBusqueda) where T : IEntidadBase
        {
            List<T> valorRetorno = null;

            if (string.IsNullOrEmpty(entidad.nombreTabla))
            {
                entidad.nombreTabla = entidad.GetType().Name.eliminarCaracteresEspeciales().ToLower();
            }

            if (string.IsNullOrEmpty(entidad.PartitionKey))
            {
                entidad.PartitionKey = entidad.GetType().FullName.ToLower();
            }

            string json = JsonConvert.SerializeObject(entidad);
            string parameters = string.Format("blob/?nombreTabla={0}&partitionKey={1}", entidad.nombreTabla, entidad.PartitionKey);

            //Cuando se introducen terminos de busqueda usamos otro servicio
            if (!string.IsNullOrEmpty(terminosBusqueda))
            {
                parameters = string.Format("blobBusquedas/?nombreTabla={0}&partitionKey={1}&terminosBusqueda={2}", entidad.nombreTabla, entidad.PartitionKey, terminosBusqueda);
            }

            valorRetorno = await doGet<T>(valorRetorno, parameters);

            return valorRetorno;
        }

        public static async Task<List<T>> getBlobByByNamePaginated<T>(this T entidad, string terminosBusqueda, int take, int skip) where T : IEntidadBase
        {
            List<T> valorRetorno = null;

            if (string.IsNullOrEmpty(entidad.nombreTabla))
            {
                entidad.nombreTabla = entidad.GetType().Name.eliminarCaracteresEspeciales().ToLower();
            }

            if (string.IsNullOrEmpty(entidad.PartitionKey))
            {
                entidad.PartitionKey = entidad.GetType().FullName.ToLower();
            }

            string json = JsonConvert.SerializeObject(entidad);
            string parameters = string.Format("blob/?nombreTabla={0}&partitionKey={1}&take={2}&skip{3}", entidad.nombreTabla, entidad.PartitionKey, take, skip);

            //Cuando se introducen terminos de busqueda usamos otro servicio
            if (!string.IsNullOrEmpty(terminosBusqueda))
            {
                parameters = string.Format("blobBusquedas/?nombreTabla={0}&partitionKey={1}&terminosBusqueda={2}&take={3}&skip{4}", entidad.nombreTabla, entidad.PartitionKey, terminosBusqueda, take, skip);
            }


            valorRetorno = await doGet<T>(valorRetorno, parameters);

            return valorRetorno;
        }

        public static async Task<T> getBlobByPartitionAndRowKey<T>(this T entidad, string rowKey) where T : IEntidadBase
        {
            T valorRetorno = default(T);
            
            if (string.IsNullOrEmpty(entidad.nombreTabla))
            {
                entidad.nombreTabla = entidad.GetType().Name.eliminarCaracteresEspeciales().ToLower();
            }
            else
            {
                entidad.nombreTabla = entidad.nombreTabla.ToLower();
            }

            if (string.IsNullOrEmpty(entidad.PartitionKey))
            {
                entidad.PartitionKey = entidad.GetType().FullName.eliminarCaracteresEspeciales().ToLower();
            }
            else
            {
                entidad.PartitionKey = entidad.PartitionKey.ToLower();
            }

            string json = JsonConvert.SerializeObject(entidad);
            string parameters = string.Format("blob/?nombreTabla={0}&partitionKey={1}&rowKey={2}", entidad.nombreTabla, entidad.PartitionKey, rowKey);

            valorRetorno = await doGetParameters<T>(valorRetorno, parameters);

            return valorRetorno;
        }



        public static async Task<List<T>> getBlobByPartition<T>(this T entidad, string nombreTabla = "", string PartitionKey = "") where T : class
        {
            List<T> valorRetorno = null;

            string json = JsonConvert.SerializeObject(entidad);
            string parameters = string.Format("blob/?nombreTabla={0}&partitionKey={1}", nombreTabla, PartitionKey);

            valorRetorno = await doGetByPartition<T>(valorRetorno, parameters);

            return valorRetorno;
        }
    }
}

