using Android.Content;
using Bizland.Droid.CustomRenderer;
using Bizland.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using AView = Android.Views.View;

[assembly: ExportRenderer(typeof(TransparentNavigation), typeof(TransparentNavigationRenderer))]
namespace Bizland.Droid.CustomRenderer
{
    public class TransparentNavigationRenderer : NavigationPageRenderer
    {
        public TransparentNavigationRenderer(Context context) : base(context)
        {

        }

        IPageController PageController => Element as IPageController;
        TransparentNavigation CustomNavigationPage => Element as TransparentNavigation;

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            CustomNavigationPage.IgnoreLayoutChange = true;
            base.OnLayout(changed, l, t, r, b);
            CustomNavigationPage.IgnoreLayoutChange = false;

            int containerHeight = b - t;

            PageController.ContainerArea = new Rectangle(0, 0, Context.FromPixels(r - l), Context.FromPixels(containerHeight));

            for (var i = 0; i < ChildCount; i++)
            {
                AView child = GetChildAt(i);

                if (child is Android.Support.V7.Widget.Toolbar)
                {
                    continue;
                }

                child.Layout(0, 0, r, b);
            }
        }
    }
}