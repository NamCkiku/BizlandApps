using Bizland.Model;
using System.Threading.Tasks;

namespace Bizland.ApiService
{
    public interface IProfileService
    {
        Task<IdentityResult> UpdateInfomationUser(AppUser model);
    }
}
