using System.Collections.Generic;
using System.Threading.Tasks;

namespace BizlandApiService.Service
{
    public interface IPlacesGeocode
    {
        Task<Geocode> GetGeocode(string input);


        Task<Geocode> GetAddressesForPosition(string lat, string lng);
    }
}
