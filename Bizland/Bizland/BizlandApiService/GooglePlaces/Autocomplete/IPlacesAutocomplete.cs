using System.Threading.Tasks;

namespace BizlandApiService.Service
{
    public interface IPlacesAutocomplete
    {
        Task<Predictions> GetAutocomplete(string input);
    }
}
