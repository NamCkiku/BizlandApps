using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Bizland.Core
{
    public static class CameraHelper
    {
        public static async Task<Stream> TakePhotoSteamAsync()
        {
            Stream result = null;
            try
            {
                var permisstion = await PermissionHelper.CheckCameraPermissions();
                if (permisstion)
                {
                    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                    {
                        //await App.Current.MainPage.DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                        return null;
                    }

                    var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    {
                        PhotoSize = Plugin.Media.Abstractions.PhotoSize.MaxWidthHeight,
                        MaxWidthHeight = 2000,
                        DefaultCamera = CameraDevice.Front,
                        CustomPhotoSize = 50,
                        SaveToAlbum = true,
                        CompressionQuality = 75,
                        Directory = "Sample",
                        Name = "test.jpg"
                    });

                    if (file == null)
                        return null;

                    result = file.GetStream();
                    file.Dispose();
                }
            }
            catch (Exception)
            {

                throw;
            }


            return result;
        }

        public static async Task<string> TakePhotoPathAsync()
        {
            var path = string.Empty;

            try
            {
                var permisstion = await PermissionHelper.CheckCameraPermissions();
                if (permisstion)
                {
                    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                    {
                        //await App.Current.MainPage.DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                        return null;
                    }

                    var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                    {
                        PhotoSize = Plugin.Media.Abstractions.PhotoSize.MaxWidthHeight,
                        MaxWidthHeight = 2000,
                        DefaultCamera = CameraDevice.Front,
                        CustomPhotoSize = 50,
                        SaveToAlbum = true,
                        CompressionQuality = 75,
                        Directory = "Sample",
                        Name = "test.jpg"
                    });

                    if (file == null)
                        return null;

                    path = file.Path;
                    file.Dispose();
                }
            }
            catch (Exception)
            {

                throw;
            }


            return path;
        }


        public static async Task<Stream> PickPhotoSteamAsync()
        {
            Stream result = null;
            try
            {
                var permisstion = await PermissionHelper.CheckPhotoPermissions();
                if (permisstion)
                {
                    if (!CrossMedia.Current.IsPickPhotoSupported)
                    {
                        //await App.Current.MainPage.DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                        return null;
                    }

                    var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                    {
                        PhotoSize = Plugin.Media.Abstractions.PhotoSize.MaxWidthHeight
                    });

                    if (file == null)
                        return null;

                    result = file.GetStream();
                    file.Dispose();

                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public static async Task<string> PickPhotoPathAsync()
        {
            var path = string.Empty;

            try
            {
                var permisstion = await PermissionHelper.CheckPhotoPermissions();
                if (permisstion)
                {
                    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                    {
                        //await App.Current.MainPage.DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                        return null;
                    }

                    var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
                    {
                        PhotoSize = Plugin.Media.Abstractions.PhotoSize.MaxWidthHeight
                    });


                    if (file == null)
                        return null;

                    path = file.Path;

                    file.Dispose();
                }
            }
            catch (Exception)
            {

                throw;
            }


            return path;
        }


        public static async Task<Stream> TakeVideoSteamAsync()
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakeVideoSupported)
            {
                //await App.Current.MainPage.DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                return null;
            }

            var file = await CrossMedia.Current.TakeVideoAsync(new Plugin.Media.Abstractions.StoreVideoOptions
            {
                Name = "video.mp4",
                Directory = "DefaultVideos",
            });

            if (file == null)
                return null;
            var steam = file.GetStream();
            file.Dispose();

            return steam;
        }

        public static async Task<Stream> PicVideoSteamAsync()
        {
            if (!CrossMedia.Current.IsPickVideoSupported)
            {
                //await App.Current.MainPage.DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                return null;
            }

            var file = await CrossMedia.Current.PickVideoAsync();

            if (file == null)
                return null;

            var steam = file.GetStream();
            file.Dispose();

            return steam;
        }

    }
}
