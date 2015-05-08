
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


namespace Hefesoft.Standard.Util.table
{
    public static class crudTable
    {
        public static async Task<T> postTable<T>(this T entidad) where T : IEntidadBase
        {
            if (string.IsNullOrEmpty(entidad.nombreTabla))
            {
                entidad.nombreTabla = entidad.GetType().Name.eliminarCaracteresEspeciales().ToLower();
            }

            if (string.IsNullOrEmpty(entidad.PartitionKey))
            {
                entidad.PartitionKey = entidad.GetType().FullName.ToLower();
            }

            if (string.IsNullOrEmpty(entidad.RowKey))
            {
                string row = string.Format("{0}{1}{2}tick{3}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Year, DateTime.Now.Ticks);
                entidad.RowKey = entidad.GetType().Namespace.ToLower() + row;
            }


            try
            {
                T valorRetorno;
                string json = JsonConvert.SerializeObject(entidad, Formatting.Indented,
                                new JsonSerializerSettings
                                {
                                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                });

                HttpClientHandler handler = new HttpClientHandler();
                var httpClient = new HttpClient(handler);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Path_Servicio.obtenerUrlServicio() + "table");
                request.Content = new StringContent(json);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Hefesoft.Standard.Static.Variables_Globales.Bearer);

                MediaTypeHeaderValue contentType = request.Content.Headers.ContentType;
                contentType.MediaType = "application/json";

                request.Content.Headers.ContentType = contentType;

                if (handler.SupportsTransferEncodingChunked())
                {
                    request.Headers.TransferEncodingChunked = true;
                }
                HttpResponseMessage response = await httpClient.SendAsync(request);

                var resultadoString = response.Content.ReadAsStringAsync().Result;

                valorRetorno = JsonConvert.DeserializeObject<T>(resultadoString);

                return valorRetorno;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<T>> getTableByPartition<T>(this T entidad) where T : IEntidadBase
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
            string parameters = string.Format("table/?nombreTabla={0}&partitionKey={1}", entidad.nombreTabla, entidad.PartitionKey);

            HttpClientHandler handler = new HttpClientHandler();
            var httpClient = new HttpClient(handler);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Path_Servicio.obtenerUrlServicio() + parameters);

            if (handler.SupportsTransferEncodingChunked())
            {
                request.Headers.TransferEncodingChunked = true;
            }
            HttpResponseMessage response = await httpClient.SendAsync(request);

            try
            {
                var resultadoString = response.Content.ReadAsStringAsync().Result;
                valorRetorno = JsonConvert.DeserializeObject<List<T>>(resultadoString);
            }
            catch
            {

            }

            return valorRetorno;
        }


        public static async Task<T> getTableByPartitionAndRowKey<T>(this T entidad, string rowKey) where T : IEntidadBase
        {
            T valor_retorno = default(T);

            if (string.IsNullOrEmpty(entidad.nombreTabla))
            {
                entidad.nombreTabla = entidad.GetType().Name.eliminarCaracteresEspeciales().ToLower();
            }

            if (string.IsNullOrEmpty(entidad.PartitionKey))
            {
                entidad.PartitionKey = entidad.GetType().FullName.ToLower();
            }

            string json = JsonConvert.SerializeObject(entidad);
            string parameters = string.Format("table/?nombreTabla={0}&partitionKey={1}&rowKey={2}", entidad.nombreTabla, entidad.PartitionKey, rowKey);

            HttpClientHandler handler = new HttpClientHandler();
            var httpClient = new HttpClient(handler);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, Path_Servicio.obtenerUrlServicio() + parameters);

            if (handler.SupportsTransferEncodingChunked())
            {
                request.Headers.TransferEncodingChunked = true;
            }
            HttpResponseMessage response = await httpClient.SendAsync(request);

            try
            {
                var resultadoString = response.Content.ReadAsStringAsync().Result;
                valor_retorno = JsonConvert.DeserializeObject<T>(resultadoString);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return valor_retorno;
        }
    }
}
