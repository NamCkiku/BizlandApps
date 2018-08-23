using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bizland.iOS.DependencyService;
using Bizland.Service;
using Bizland.Views;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(LoadingPageServiceiOS))]
namespace Bizland.iOS.DependencyService
{
    public class LoadingPageServiceiOS : ILoadingPageService
    {
        private UIView _nativeView;

        private bool _isInitialized;

        public void InitLoadingPage(ContentPage loadingIndicatorPage)
        {
            // check if the page parameter is available
            if (loadingIndicatorPage != null)
            {
                // build the loading page with native base
                loadingIndicatorPage.Parent = Xamarin.Forms.Application.Current.MainPage;

                loadingIndicatorPage.Layout(new Rectangle(0, 0,
                    Xamarin.Forms.Application.Current.MainPage.Width,
                    Xamarin.Forms.Application.Current.MainPage.Height));

                var renderer = loadingIndicatorPage.GetOrCreateRenderer();

                _nativeView = renderer.NativeView;

                _isInitialized = true;
            }
        }

        public void ShowLoadingPage()
        {
            // check if the user has set the page or not
            if (!_isInitialized)
                InitLoadingPage(new LoadingIndicatorPage()); // set the default page

            // showing the native loading page
            UIApplication.SharedApplication.KeyWindow.AddSubview(_nativeView);
        }

        private void XamFormsPage_Appearing(object sender, EventArgs e)
        {
            var animation = new Animation(callback: d => ((ContentPage)sender).Content.Rotation = d,
                start: ((ContentPage)sender).Content.Rotation,
                end: ((ContentPage)sender).Content.Rotation + 360,
                easing: Easing.Linear);
            animation.Commit(((ContentPage)sender).Content, "RotationLoopAnimation", 16, 800, null, null, () => true);
        }

        public void HideLoadingPage()
        {
            _nativeView.RemoveFromSuperview();
        }
    }

    internal static class PlatformExtension
    {
        public static IVisualElementRenderer GetOrCreateRenderer(this VisualElement bindable)
        {
            var renderer = Xamarin.Forms.Platform.iOS.Platform.GetRenderer(bindable);
            if (renderer == null)
            {
                renderer = Xamarin.Forms.Platform.iOS.Platform.CreateRenderer(bindable);
                Xamarin.Forms.Platform.iOS.Platform.SetRenderer(bindable, renderer);
            }
            return renderer;
        }
    }
}