﻿using Bizland.Core;
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
        private readonly PHAsset[] _preselectedAssets;
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

        private void Picker_FinishedPickingAssets(object sender, MultiAssetEventArgs args)
        {
            _pickAsyncResult = new List<ImageSource>();
            PHImageManager imageManager = new PHImageManager();
            // For demo purposes: just show all chosen pictures in order every second
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
                CustomSmartCollections = new[] { PHAssetCollectionSubtype.AlbumRegular, PHAssetCollectionSubtype.AlbumImported },
                NavigationBarBackgroundColor = UIColor.White,
                NavigationBarTextColor = UIColor.Blue,
                NavigationBarTintColor = UIColor.White,
                PickerBackgroundColor = UIColor.White,
                ToolbarBarTintColor = UIColor.White,
                ToolbarTextColor = UIColor.Blue,
                PickerStatusBarStyle = UIStatusBarStyle.Default,
                GridSortOrder = SortOrder.Descending
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
                _pickAsyncResult = null;

                return result;
            });
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