using Bizland.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bizland.ApiService
{
    public interface IRoomTypeService
    {
        Task<List<RoomType>> GetAllRoomType();
    }
}
