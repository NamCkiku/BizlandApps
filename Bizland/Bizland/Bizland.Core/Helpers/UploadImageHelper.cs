using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bizland.Core.Helpers
{
    public static class UploadImageHelper
    {
        const int MAX_AVATAR_WIDTH = 300;
        const int MAX_AVATAR_HEIGHT = 300;

        //public static byte[] ReSize(byte[] file)
        //{
        //    var fileSize = file.Length;
        //    if (fileSize < 1024 * 70)//nếu nhỏ hơn 70kb thì thôi không resize
        //    {
        //        return file;
        //    }

        //    byte[] result = null;

        //    //resize file ảnh
        //    var _resize = App.Instance.ImgResizer;
        //    result = _resize.ResizeImage(file, MAX_AVATAR_WIDTH, MAX_AVATAR_HEIGHT);

        //    return result;
        //}
        public static async Task<string> UploadImage(ImageType imageType)
        {
            string filePath = string.Empty;
            try
            {
                string[] buttonCamera = new string[] { };

                buttonCamera = new string[] { "Chụp ảnh", "Chọn từ thư viện ảnh" };

                var action = await Application.Current.MainPage.DisplayActionSheet(
                             null,
                              "Cancel",
                              null,
                              buttonCamera);

                switch (action)
                {
                    case "Chụp ảnh":
                        {

                            filePath = await CameraHelper.TakePhotoPathAsync();
                        }
                        break;
                    case "Chọn từ thư viện ảnh":
                        {
                            filePath = await CameraHelper.PickPhotoPathAsync();
                        }
                        break;
                    default:
                        return filePath;
                }
            }
            catch (System.Exception ex)
            {

                Logger.WriteError(MethodInfo.GetCurrentMethod().Name, ex);
            }
            return filePath;
        }
    }
    public enum ImageType
    {
        Avatar = 0
    }
}
