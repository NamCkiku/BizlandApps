using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bizland.Core
{
    public interface IMediaService
    {
        void OpenGallery();

        void ClearFiles(List<string> filePaths);


        void PickedResult(List<ImageSource> result);
    }
}
