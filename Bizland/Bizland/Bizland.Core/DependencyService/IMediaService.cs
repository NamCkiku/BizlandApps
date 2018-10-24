using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bizland.Core
{
    public interface IMediaService
    {
        Task<List<ImageSource>> PickImageAsync();
    }
}
