using Hefesoft.Standard.Interfaces;
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
        private static async Task<List<T>> doGet<T>(List<T> valorRetorno, string parameters) where T : IEntidadBase
        {
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

        private static async Task<T> doGetParameters<T>(T valorRetorno, string parameters) where T : IEntidadBase
        {
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
                valorRetorno = JsonConvert.DeserializeObject<T>(resultadoString);
            }
            catch
            {

            }
            return valorRetorno;
        }

        private static async Task<List<T>> doGetByPartition<T>(List<T> valorRetorno, string parameters) where T : class
        {
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

        private static async Task<string> doPost(string json)
        {
            HttpClientHandler handler = new HttpClientHandler();
            var httpClient = new HttpClient(handler);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Path_Servicio.obtenerUrlServicio() + "blob");
            request.Content = new StringContent(json);
            MediaTypeHeaderValue contentType = request.Content.Headers.ContentType;
            contentType.MediaType = "application/json";

            request.Content.Headers.ContentType = contentType;

            if (handler.SupportsTransferEncodingChunked())
            {
                request.Headers.TransferEncodingChunked = true;
            }
            HttpResponseMessage response = await httpClient.SendAsync(request);

            var resultadoString = response.Content.ReadAsStringAsync().Result;
            return resultadoString;
        }
    }

}

