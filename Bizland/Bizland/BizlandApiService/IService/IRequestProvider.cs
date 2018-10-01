using Plugin.Media.Abstractions;
using System.IO;
using System.Threading.Tasks;

namespace BizlandApiService.IService
{
    public interface IRequestProvider
    {
        Task<TResult> GetAsync<TResult>(string uri, string token = "");

        Task<TResult> PostAsync<TResult>(string uri, TResult data, string token = "", string header = "");

        Task<TResult> PostAsync<TRequest, TResult>(string uri, TRequest data, string token = "", string header = "");

        Task<TResult> PostAsync<TResult>(string uri, string data, string clientId, string clientSecret);

        Task<TResult> PutAsync<TResult>(string uri, TResult data, string token = "", string header = "");

        Task<TResult> UploadFile<TResult>(string uri, MediaFile _mediaFile, string token = "", string header = "");

        Task<TResult> GetHandleOutputAsync<TResult>(string uri, string token = "");

        Task DeleteAsync(string uri, string token = "");

        Task<bool> UploadImageAsync(string uri, Stream image, string fileName, string tocken = "", string header = "");
    }
}
