using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Bizland.Droid.DependencyService;
using Bizland.Service;
using Bizland.Views;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.Dependency(typeof(LoadingPageServiceDroid))]
namespace Bizland.Droid.DependencyService
{
    public class LoadingPageServiceDroid : ILoadingPageService
    {
        private Android.Views.View _nativeView;

        private Dialog _dialog;

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

                _nativeView = renderer.View;

                _dialog = new Dialog(CrossCurrentActivity.Current.Activity);
                _dialog.RequestWindowFeature((int)WindowFeatures.NoTitle);
                _dialog.SetCancelable(false);
                _dialog.SetContentView(_nativeView);
                Window window = _dialog.Window;
                window.SetLayout(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent);
                window.ClearFlags(WindowManagerFlags.DimBehind);
                window.SetBackgroundDrawable(new ColorDrawable(Android.Graphics.Color.Transparent));

                _isInitialized = true;
            }
        }

        public void ShowLoadingPage()
        {
            // check if the user has set the page or not
            if (!_isInitialized)
                InitLoadingPage(new LoadingIndicatorPage()); // set the default page

            // showing the native loading page
            _dialog.Show();
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
            _dialog.Hide();
        }
    }

    internal static class PlatformExtension
    {
        public static IVisualElementRenderer GetOrCreateRenderer(this VisualElement bindable)
        {
            var renderer = Xamarin.Forms.Platform.Android.Platform.GetRenderer(bindable);
            if (renderer == null)
            {
                renderer = Xamarin.Forms.Platform.Android.Platform.CreateRendererWithContext(bindable, CrossCurrentActivity.Current.Activity);
                Xamarin.Forms.Platform.Android.Platform.SetRenderer(bindable, renderer);
            }
            return renderer;
        }
    }
}