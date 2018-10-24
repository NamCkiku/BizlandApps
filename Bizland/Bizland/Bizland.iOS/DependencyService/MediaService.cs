using Bizland.Core;
using Bizland.iOS.DependencyService;
using CoreGraphics;
using GMImagePicker;
using Photos;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(MediaService))]
namespace Bizland.iOS.DependencyService
{
    public class MediaService : UIViewController, IMediaService
    {
        public void ClearFiles(List<string> filePaths)
        {
            throw new System.NotImplementedException();
        }
        private PHAsset[] _preselectedAssets;
        public void OpenGallery()
        {
            var picker = new GMImagePickerController
            {
                Title = "Đăng ảnh",
                CustomDoneButtonTitle = "Hoàn thành",
                CustomCancelButtonTitle = "Đóng",
                CustomNavigationBarPrompt = "Chụp ảnh mới hoặc chọn ảnh hiện có",
                ColsInPortrait = 3,
                ColsInLandscape = 4,
                MinimumInteritemSpacing = 2.0f,
                DisplaySelectionInfoToolbar = true,
                AllowsMultipleSelection = true,
                ShowCameraButton = true,
                AutoSelectCameraImages = true,
                AllowsEditingCameraImages = true,
                ModalPresentationStyle = UIModalPresentationStyle.Popover,
                MediaTypes = new[] { PHAssetMediaType.Image },
                CustomSmartCollections = new[] { PHAssetCollectionSubtype.AlbumRegular, PHAssetCollectionSubtype.AlbumImported },
                NavigationBarBackgroundColor = UIColor.White,
                NavigationBarTextColor = UIColor.Blue,
                NavigationBarTintColor = UIColor.White,
                PickerBackgroundColor = UIColor.White,
                ToolbarBackgroundColor = UIColor.White,
                ToolbarBarTintColor = UIColor.Blue,
                ToolbarTextColor = UIColor.Blue,
                PickerStatusBarStyle = UIStatusBarStyle.Default,
                GridSortOrder = SortOrder.Descending,
                AdditionalToolbarItems = new UIBarButtonItem[] { new UIBarButtonItem(UIBarButtonSystemItem.Bookmarks), new UIBarButtonItem("Custom", UIBarButtonItemStyle.Bordered, (s, e) => { Console.WriteLine("test"); }) },

            };

            if (_preselectedAssets != null)
            {
                foreach (var asset in _preselectedAssets)
                {
                    picker.SelectedAssets.Add(asset);
                }
            }
            // Event handling
            picker.FinishedPickingAssets += Picker_FinishedPickingAssets;
            picker.Canceled += Picker_Canceled;

            // GMImagePicker can be treated as a PopOver as well:
            var popPC = picker.PopoverPresentationController;
            popPC.PermittedArrowDirections = UIPopoverArrowDirection.Any;

            UIWindow window = UIApplication.SharedApplication.KeyWindow;
            var viewController = window.RootViewController;
            viewController.PresentModalViewController(picker, true);
        }


        void Picker_Canceled(object sender, EventArgs e)
        {
            if (sender is UIImagePickerController)
            {
                ((UIImagePickerController)sender).DismissViewController(true, null);
            }
            Console.WriteLine("User canceled picking image.");
        }

        async void Picker_FinishedPickingAssets(object sender, MultiAssetEventArgs args)
        {
            PHImageManager imageManager = new PHImageManager();

            _preselectedAssets = args.Assets;

            PHImageRequestOptions options = new PHImageRequestOptions();
            options.DeliveryMode = PHImageRequestOptionsDeliveryMode.HighQualityFormat;
            options.ResizeMode = PHImageRequestOptionsResizeMode.Exact;
            options.Synchronous = true;
            options.NetworkAccessAllowed = true;

            // For demo purposes: just show all chosen pictures in order every second
            foreach (var asset in args.Assets)
            {

                // Get information about the asset, e.g. file patch
                asset.RequestContentEditingInput(new PHContentEditingInputRequestOptions(),
                    (input, _) =>
                    {
                        Console.WriteLine(input.FullSizeImageUrl);
                    });

                imageManager.RequestImageForAsset(asset,
                    new CGSize(asset.PixelWidth, asset.PixelHeight),
                    PHImageContentMode.Default,
                    null,
                    (image, info) =>
                    {
                    });
                await Task.Delay(1000);
            }
        }

        private ImageSource _pickAsyncResult;
        private readonly EventWaitHandle _waitHandle = new AutoResetEvent(false);
        public Task<ImageSource> PickImageAsync()
        {
            _pickAsyncResult = null;

            var picker = new GMImagePickerController
            {
                Title = "Đăng ảnh",
                CustomDoneButtonTitle = "Hoàn thành",
                CustomCancelButtonTitle = "Đóng",
                CustomNavigationBarPrompt = "Chụp ảnh mới hoặc chọn ảnh hiện có",
                ColsInPortrait = 3,
                ColsInLandscape = 4,
                MinimumInteritemSpacing = 2.0f,
                DisplaySelectionInfoToolbar = true,
                AllowsMultipleSelection = true,
                ShowCameraButton = true,
                AutoSelectCameraImages = true,
                AllowsEditingCameraImages = true,
                ModalPresentationStyle = UIModalPresentationStyle.Popover,
                MediaTypes = new[] { PHAssetMediaType.Image },
                CustomSmartCollections = new[] { PHAssetCollectionSubtype.AlbumRegular, PHAssetCollectionSubtype.AlbumImported },
                NavigationBarBackgroundColor = UIColor.White,
                NavigationBarTextColor = UIColor.Blue,
                NavigationBarTintColor = UIColor.White,
                PickerBackgroundColor = UIColor.White,
                ToolbarBackgroundColor = UIColor.White,
                ToolbarBarTintColor = UIColor.Blue,
                ToolbarTextColor = UIColor.Blue,
                PickerStatusBarStyle = UIStatusBarStyle.Default,
                GridSortOrder = SortOrder.Descending,
                AdditionalToolbarItems = new UIBarButtonItem[] { new UIBarButtonItem(UIBarButtonSystemItem.Bookmarks), new UIBarButtonItem("Custom", UIBarButtonItemStyle.Bordered, (s, e) => { Console.WriteLine("test"); }) },

            };

            if (_preselectedAssets != null)
            {
                foreach (var asset in _preselectedAssets)
                {
                    picker.SelectedAssets.Add(asset);
                }
            }
            picker.Canceled += Picker_Canceled;
            // Event handling
            picker.FinishedPickingAssets += (s, e) =>
            {
                PHImageManager imageManager = new PHImageManager();
                // For demo purposes: just show all chosen pictures in order every second
                foreach (var asset in e.Assets)
                {
                    imageManager.RequestImageForAsset(asset,
                    new CGSize(asset.PixelWidth, asset.PixelHeight),
                    PHImageContentMode.Default,
                    null,
                    (image, info) =>
                    {
                        _pickAsyncResult = ImageSource.FromStream(() => image.AsPNG().AsStream());
                        _waitHandle.Set();
                    });
                }
            };


            // GMImagePicker can be treated as a PopOver as well:
            var popPC = picker.PopoverPresentationController;
            popPC.PermittedArrowDirections = UIPopoverArrowDirection.Any;

            UIWindow window = UIApplication.SharedApplication.KeyWindow;
            var viewController = window.RootViewController;
            viewController.PresentModalViewController(picker, true);

            return Task.Run(() =>
            {
                _waitHandle.WaitOne();
                var result = _pickAsyncResult;
                _pickAsyncResult = null;

                return result;
            });
        }
    }
}