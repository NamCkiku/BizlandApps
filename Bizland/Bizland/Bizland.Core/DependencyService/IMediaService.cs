using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bizland.Core
{
    public interface IMediaService
    {
        void OpenGallery();

        void ClearFiles(List<string> filePaths);
    }
}
