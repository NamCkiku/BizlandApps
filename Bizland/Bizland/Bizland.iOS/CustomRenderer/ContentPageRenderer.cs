using Bizland.iOS.CustomRenderer;
using Bizland.iOS.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPage), typeof(ContentPageRenderer))]
namespace Bizland.iOS.CustomRenderer
{
    public class ContentPageRenderer : PageRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            // Some basic navigationbar styling.
            //var color = Color.FromHex("#79b338");

            //UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes
            //{
            //    Font = UIFont.FromName("MuseoSans-500", 18),
            //    TextColor = UIColor.White
            //});

            UINavigationBar.Appearance.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
            UINavigationBar.Appearance.ShadowImage = new UIImage();
            UINavigationBar.Appearance.BackgroundColor = UIColor.Clear;
            UINavigationBar.Appearance.TintColor = UIColor.Clear;
            UINavigationBar.Appearance.BarTintColor = UIColor.Clear;
            UINavigationBar.Appearance.Translucent = false;
            UINavigationBar.Appearance.BarStyle = UIBarStyle.Black;
            // Set the status bar to light
            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.Default, true);
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            var contentPage = this.Element as ContentPage;
            if (contentPage == null || NavigationController == null)
                return;

            var itemsInfo = contentPage.ToolbarItems;

            // Move toolbaritems over to the left if we have more than 1.
            var navigationItem = this.NavigationController.TopViewController.NavigationItem;
            var leftNativeButtons = (navigationItem.LeftBarButtonItems ?? new UIBarButtonItem[] { }).ToList();
            var rightNativeButtons = (navigationItem.RightBarButtonItems ?? new UIBarButtonItem[] { }).ToList();

            var newLeftButtons = new UIBarButtonItem[] { }.ToList();
            var newRightButtons = new UIBarButtonItem[] { }.ToList();

            rightNativeButtons.ForEach(nativeItem =>
            {
                // [Hack] Get Xamarin private field "item"
                var field = nativeItem.GetType().GetField("_item", BindingFlags.NonPublic | BindingFlags.Instance);
                if (field == null)
                    return;

                var info = field.GetValue(nativeItem) as ToolbarItem;
                if (info == null)
                    return;

                if (info.Priority == 0)
                    newLeftButtons.Add(nativeItem);
                else
                    newRightButtons.Add(nativeItem);
            });

            leftNativeButtons.ForEach(nativeItem =>
            {
                newLeftButtons.Add(nativeItem);
            });

            navigationItem.RightBarButtonItems = newRightButtons.ToArray();
            navigationItem.LeftBarButtonItems = newLeftButtons.ToArray();


            ModalPresentationCapturesStatusBarAppearance = true;

        }

        public override void ViewWillDisappear(bool animated)
        {
            base.ViewWillDisappear(animated);
        }
    }
}
