using Bizland.Core;
using Bizland.iOS.DependencyService;
using GMImagePicker;
using Photos;
using System.Collections.Generic;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(MediaService))]
namespace Bizland.iOS.DependencyService
{
    public class MediaService : IMediaService
    {
        public void ClearFiles(List<string> filePaths)
        {
            throw new System.NotImplementedException();
        }

        public void OpenGallery()
        {
            var picker = new GMImagePickerController
            {
                Title = "Custom Title",
                CustomDoneButtonTitle = "Finished",
                CustomCancelButtonTitle = "Nope",
                CustomNavigationBarPrompt = "Take a new photo or select an existing one!",
                ColsInPortrait = 3,
                ColsInLandscape = 5,
                MinimumInteritemSpacing = 2.0f,
                DisplaySelectionInfoToolbar = true,
                AllowsMultipleSelection = true,
                ShowCameraButton = true,
                AutoSelectCameraImages = true,
                AllowsEditingCameraImages = true,
                ModalPresentationStyle = UIModalPresentationStyle.Popover,
                MediaTypes = new[] { PHAssetMediaType.Image },

            };

            // You can limit which galleries are available to browse through
            picker.CustomSmartCollections = new[] {
                PHAssetCollectionSubtype.SmartAlbumUserLibrary,
                PHAssetCollectionSubtype.AlbumRegular
            };
        }
    }
}