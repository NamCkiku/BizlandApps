using System.Collections.Generic;

namespace Bizland.Core
{
    public interface IMediaService
    {
        void OpenGallery();

        void ClearFiles(List<string> filePaths);
    }
}
