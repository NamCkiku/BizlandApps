using Bizland.Core;
using BizlandApiService.IService;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BizlandApiService.Service
{
    public class RequestProvider : IRequestProvider
    {
        private readonly JsonSerializerSettings _serializerSettings;
        private readonly HttpClient _httpClient;
        public RequestProvider()
        {
            _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore
            };
            _serializerSettings.Converters.Add(new StringEnumConverter());
        }

        public async Task<TResult> GetAsync<TResult>(string uri, string token = "")
        {
            HttpClient httpClient = CreateHttpClient(token);
            httpClient.BaseAddress = new Uri(ServerConfig.ApiEndpoint);
            HttpResponseMessage response = await httpClient.GetAsync(uri);

            await HandleResponse(response);
            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() =>
                JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

            return result;
        }

        public async Task<TResult> GetHandleOutputAsync<TResult>(string uri, string token = "")
        {
            HttpClient httpClient = CreateHttpClient(token);
            httpClient.BaseAddress = new Uri(ServerConfig.ApiEndpoint);
            HttpResponseMessage response = await httpClient.GetAsync(uri);

            TResult result = default(TResult);

            result = await HandleResponseWithResult<TResult>(response);

            return result;
        }

        public async Task<TResult> PostAsync<TResult>(string uri, TResult data, string token = "", string header = "")
        {
            HttpClient httpClient = CreateHttpClient(token);
            httpClient.BaseAddress = new Uri(ServerConfig.ApiEndpoint);
            if (!string.IsNullOrEmpty(header))
            {
                AddHeaderParameter(httpClient, header);
            }

            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PostAsync(uri, content);

            await HandleResponse(response);
            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() =>
                JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

            return result;
        }


        public async Task<TResult> PostAsync<TRequest, TResult>(string uri, TRequest data, string token = "", string header = "")
        {
            HttpClient httpClient = CreateHttpClient(token);
            httpClient.BaseAddress = new Uri(ServerConfig.ApiEndpoint);
            if (!string.IsNullOrEmpty(header))
            {
                AddHeaderParameter(httpClient, header);
            }

            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await httpClient.PostAsync(uri, content);

            TResult result = default(TResult);

            result = await HandleResponseWithResult<TResult>(response);

            return result;
        }

        public async Task<TResult> PostAsync<TResult>(string uri, string data, string clientId, string clientSecret)
        {
            HttpClient httpClient = CreateHttpClient(string.Empty);
            httpClient.BaseAddress = new Uri(ServerConfig.ApiEndpoint);
            if (!string.IsNullOrWhiteSpace(clientId) && !string.IsNullOrWhiteSpace(clientSecret))
            {
                AddBasicAuthenticationHeader(httpClient, clientId, clientSecret);
            }

            var content = new StringContent(data);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            HttpResponseMessage response = await httpClient.PostAsync(uri, content);

            await HandleResponse(response);
            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() =>
                JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

            return result;
        }

        public async Task<TResult> PutAsync<TResult>(string uri, TResult data, string token = "", string header = "")
        {
            HttpClient httpClient = CreateHttpClient(token);
            httpClient.BaseAddress = new Uri(ServerConfig.ApiEndpoint);
            if (!string.IsNullOrEmpty(header))
            {
                AddHeaderParameter(httpClient, header);
            }

            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await httpClient.PutAsync(uri, content);

            await HandleResponse(response);
            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() =>
                JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

            return result;
        }

        public async Task DeleteAsync(string uri, string token = "")
        {
            HttpClient httpClient = CreateHttpClient(token);
            httpClient.BaseAddress = new Uri(ServerConfig.ApiEndpoint);
            await httpClient.DeleteAsync(uri);
        }

        public async Task<TResult> UploadFile<TResult>(string uri, MediaFile _mediaFile, string token = "", string header = "")
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ServerConfig.ApiEndpoint);
            if (!string.IsNullOrEmpty(header))
            {
                AddHeaderParameter(httpClient, header);
            }

            var content = new MultipartFormDataContent();

            content.Add(new StreamContent(_mediaFile.GetStream()),
                "\"file\"",
                $"\"{_mediaFile.Path}\"");

            var response = await httpClient.PostAsync(uri, content);
            TResult result = default(TResult);

            result = await HandleResponseWithResult<TResult>(response);

            return result;
        }

        private HttpClient CreateHttpClient(string token = "")
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return httpClient;
        }

        private void AddHeaderParameter(HttpClient httpClient, string parameter)
        {
            if (httpClient == null)
                return;

            if (string.IsNullOrEmpty(parameter))
                return;

            httpClient.DefaultRequestHeaders.Add(parameter, Guid.NewGuid().ToString());
        }

        private void AddBasicAuthenticationHeader(HttpClient httpClient, string clientId, string clientSecret)
        {
            if (httpClient == null)
                return;
            if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(clientSecret))
                return;
            httpClient.DefaultRequestHeaders.Authorization = new BasicAuthenticationHeaderValue(clientId, clientSecret);
        }


        private async Task<TResult> HandleResponseWithResult<TResult>(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            TResult result = default(TResult);
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Forbidden ||
                    response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    Logger.WriteError($"status :{response.StatusCode} content:{content}");
                }
                var message = await Task.Run(() =>
                    JsonConvert.DeserializeObject<ResponeMessage>(content, _serializerSettings));
                message.Message.ToToast(ToastNotificationType.Error);
            }
            else
            {
                result = await Task.Run(() =>
                    JsonConvert.DeserializeObject<TResult>(content, _serializerSettings));
            }

            return result;
        }

        private async Task HandleResponse(HttpResponseMessage response)
        {
            try
            {
                if (!response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    if (response.StatusCode == HttpStatusCode.Forbidden ||
                        response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        Logger.WriteError($"status :{response.StatusCode} content:{content}");
                    }
                    Logger.WriteError($"status :{response.StatusCode} content:{content}");
                }
            }
            catch (Exception ex)
            {
                Logger.WriteError($"HandleResponse-error:{ex.Message}");
            }
        }


        public async Task<bool> UploadImageAsync(string uri, Stream image, string fileName, string tocken = "", string header = "")
        {
            HttpClient httpClient = CreateHttpClient(tocken);
            httpClient.BaseAddress = new Uri(ServerConfig.ApiEndpoint);
            if (!string.IsNullOrEmpty(header))
            {
                AddHeaderParameter(httpClient, header);
            }
            HttpContent fileStreamContent = new StreamContent(image);
            fileStreamContent.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("form-data") { Name = "file", FileName = fileName };

            fileStreamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/octet-stream");

            using (var formData = new MultipartFormDataContent())
            {
                formData.Add(fileStreamContent);
                var response = await httpClient.PostAsync(uri, formData);

                return response.IsSuccessStatusCode;
            }
        }
    }
    internal class ResponeMessage
    {
        public string Message { get; set; }
    }

    enum TypeMessage
    {
        Image = 1,
        File = 2,
        Http = 3
    }
}
