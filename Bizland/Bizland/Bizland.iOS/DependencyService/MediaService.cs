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

        private List<ImageSource> _pickAsyncResult;
        private readonly EventWaitHandle _waitHandle = new AutoResetEvent(false);
        public Task<List<ImageSource>> PickImageAsync()
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
                NavigationBarBackgroundColor = UIColor.White,
                NavigationBarTextColor = UIColor.Black,
                NavigationBarTintColor = UIColor.Black,
                PickerStatusBarStyle = UIStatusBarStyle.BlackOpaque,
                GridSortOrder = SortOrder.Descending,
            };

            picker.Canceled += Picker_Canceled;
            // Event handling
            picker.FinishedPickingAssets += Picker_FinishedPickingAssets;


            // GMImagePicker can be treated as a PopOver as well:
            var popPC = picker.PopoverPresentationController;
            popPC.PermittedArrowDirections = UIPopoverArrowDirection.Any;


            var viewController = GetTheMostPresentedViewController();
            viewController.PresentViewController(picker, true, null);

            return Task.Run(() =>
            {
                _waitHandle.WaitOne();
                var result = _pickAsyncResult;

                return result;
            });
        }

        void Picker_Canceled(object sender, EventArgs e)
        {
            if (sender is UIImagePickerController)
            {
                ((UIImagePickerController)sender).DismissViewController(true, null);
            }
        }

        private void Picker_FinishedPickingAssets(object sender, MultiAssetEventArgs args)
        {
            _pickAsyncResult = new List<ImageSource>();
            PHImageManager imageManager = new PHImageManager();
            foreach (var asset in args.Assets)
            {
                imageManager.RequestImageForAsset(asset,
                new CGSize(asset.PixelWidth, asset.PixelHeight),
                PHImageContentMode.Default,
                null,
                (image, info) =>
                {
                    var imagesource = ImageSource.FromStream(() => image.AsPNG().AsStream());
                    _pickAsyncResult.Add(imagesource);
                });
            }
            _waitHandle.Set();
        }


        private UIViewController GetTheMostPresentedViewController()
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            var theMostPresentedViewController = window.RootViewController;

            while (theMostPresentedViewController.PresentedViewController != null)
            {
                theMostPresentedViewController = theMostPresentedViewController.PresentedViewController;
            }

            return theMostPresentedViewController;
        }
    }
}