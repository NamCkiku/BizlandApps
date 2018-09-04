using Bizland.Model;
using System.Threading.Tasks;

namespace Bizland.ApiService
{
    public interface IAuthenticationService
    {
        Task<UserToken> GetTokenAsync(string userName, string password);

        Task<bool> Logout();
    }
}
