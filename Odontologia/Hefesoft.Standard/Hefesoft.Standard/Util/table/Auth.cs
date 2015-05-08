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
using Hefesoft.Standard.Util.table;

namespace Hefesoft.Standard.Util.Auth
{
    public class Auth
    {
        public async Task<TokenResponseModel> sigIn(Usuario entidad)
        {
            try
            {              
                string json = JsonConvert.SerializeObject(entidad, Formatting.Indented,
                                new JsonSerializerSettings
                                {
                                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                });

                HttpClientHandler handler = new HttpClientHandler();
                var httpClient = new HttpClient(handler);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Path_Servicio.obtenerUrl() + "token");
               
                //request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", Hefesoft.Standard.Static.Variables_Globales.Bearer);
                request.Content = new StringContent("grant_type=password&username=" + entidad.UserName + "&password=" + entidad.Password, Encoding.UTF8, "application/x-www-form-urlencoded");
                MediaTypeHeaderValue contentType = request.Content.Headers.ContentType;
                contentType.MediaType = "application/json";
                


                request.Content.Headers.ContentType = contentType;

                if (handler.SupportsTransferEncodingChunked())
                {
                    request.Headers.TransferEncodingChunked = true;
                }
                HttpResponseMessage response = await httpClient.SendAsync(request);

                var resultadoString = response.Content.ReadAsStringAsync().Result;

                TokenResponseModel tokenResponse = (TokenResponseModel)JsonConvert.DeserializeObject(resultadoString, typeof(TokenResponseModel));
                Hefesoft.Standard.Static.Variables_Globales.Bearer = tokenResponse.AccessToken;
                return tokenResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> registerPushService(PushAzureService push)
        {
            try
            {
                string json = JsonConvert.SerializeObject(push, Formatting.Indented,
                                new JsonSerializerSettings
                                {
                                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                });

                HttpClientHandler handler = new HttpClientHandler();
                var httpClient = new HttpClient(handler);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Path_Servicio.obtenerUrlServicio() + "Register");

                request.Headers.Authorization = new AuthenticationHeaderValue(Hefesoft.Standard.Static.Variables_Globales.Bearer);
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

                var idHub = JsonConvert.DeserializeObject<string>(resultadoString);
                return idHub;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> updatePushService(PushAzureService push)
        {
            try
            {
                string json = JsonConvert.SerializeObject(push, Formatting.Indented,
                                new JsonSerializerSettings
                                {
                                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                });

                HttpClientHandler handler = new HttpClientHandler();
                var httpClient = new HttpClient(handler);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Put, Path_Servicio.obtenerUrlServicio() + "Register");

                request.Headers.Authorization = new AuthenticationHeaderValue(Hefesoft.Standard.Static.Variables_Globales.Bearer);
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

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> NotifyService(NotifyMessage notify)
        {
            try
            {
                string json = JsonConvert.SerializeObject(notify, Formatting.Indented,
                                new JsonSerializerSettings
                                {
                                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                });

                HttpClientHandler handler = new HttpClientHandler();
                var httpClient = new HttpClient(handler);
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Path_Servicio.obtenerUrlServicio() + "Notifications");

                request.Headers.Authorization = new AuthenticationHeaderValue(Hefesoft.Standard.Static.Variables_Globales.Bearer);
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
    }

    
}
