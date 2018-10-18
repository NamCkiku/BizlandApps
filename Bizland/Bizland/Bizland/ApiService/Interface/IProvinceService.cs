using Bizland.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bizland.ApiService
{
    public interface IProvinceService
    {
        Task<List<Province>> GetProvince();

        Task<List<District>> GetAllDistrict();

        Task<List<Ward>> GetAllWard();
    }
}
