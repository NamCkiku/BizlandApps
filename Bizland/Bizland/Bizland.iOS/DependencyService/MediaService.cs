using Bizland.Core;
using Bizland.iOS.DependencyService;
using GMImagePicker;
using Photos;
using System.Collections.Generic;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(MediaService))]
namespace Bizland.iOS.DependencyService
{
    public class MediaService : UIViewController, IMediaService
    {
        public void ClearFiles(List<string> filePaths)
        {
            throw new System.NotImplementedException();
        }
        private readonly PHAsset[] _preselectedAssets;
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

            if (_preselectedAssets != null)
            {
                foreach (var asset in _preselectedAssets)
                {
                    picker.SelectedAssets.Add(asset);
                }
            }

            // Other events to implement in order to influence selection behavior:
            // Set EventArgs::Cancel flag to true in order to prevent the action from happening
            picker.ShouldDeselectAsset += (s, e) => { /* allow deselection of (mandatory) assets */ };
            picker.ShouldEnableAsset += (s, e) => { /* determine if a specific asset should be enabled */ };
            picker.ShouldHighlightAsset += (s, e) => { /* determine if a specific asset should be highlighted */ };
            picker.ShouldShowAsset += (s, e) => { /* determine if a specific asset should be displayed */ };
            picker.ShouldSelectAsset += (s, e) => { /* determine if a specific asset can be selected */ };

            picker.AssetSelected += (s, e) => { /* keep track of individual asset selection */ };
            picker.AssetDeselected += (s, e) => { /* keep track of individual asset de-selection */ };

            // GMImagePicker can be treated as a PopOver as well:
            var popPC = picker.PopoverPresentationController;
            popPC.PermittedArrowDirections = UIPopoverArrowDirection.Any;
            //popPC.BackgroundColor = UIColor.Black;

            UIWindow window = UIApplication.SharedApplication.KeyWindow;
            var viewController = window.RootViewController;
            viewController.PresentModalViewController(picker, true);
        }
    }
}