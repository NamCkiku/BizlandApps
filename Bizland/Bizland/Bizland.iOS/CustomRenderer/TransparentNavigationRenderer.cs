﻿using Bizland.iOS.CustomRenderer;
using Bizland.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TransparentNavigation), typeof(TransparentNavigationRenderer))]
namespace Bizland.iOS.CustomRenderer
{
    public class TransparentNavigationRenderer : NavigationRenderer
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            UINavigationBar.Appearance.SetBackgroundImage(new UIImage(), UIBarMetrics.Default);
            UINavigationBar.Appearance.ShadowImage = new UIImage();
            UINavigationBar.Appearance.BackgroundColor = UIColor.Clear;
            UINavigationBar.Appearance.TintColor = UIColor.White;
            UINavigationBar.Appearance.BarTintColor = Color.FromHex("#037cb3").ToUIColor();
            UINavigationBar.Appearance.Translucent = true;

            var item = this.NavigationBar?.TopItem?.LeftBarButtonItem;

            if (item != null)
                item.Image = item.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            base.Dispose(disposing);
        }
    }
}