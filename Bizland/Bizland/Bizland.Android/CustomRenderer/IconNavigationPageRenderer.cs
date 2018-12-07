using Android.Content;
using Bizland.Droid.CustomRenderer;
using Bizland.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(RootPage), typeof(IconNavigationPageRenderer))]
namespace Bizland.Droid.CustomRenderer
{
    public class IconNavigationPageRenderer : MasterDetailPageRenderer
    {
        public IconNavigationPageRenderer(Context context) : base(context)
        {
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            SetNavigationIcon(Forms.Context, Resource.Drawable.hamburger);
        }

        private void SetNavigationIcon(Context context, int resourceId)
        {
            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            var navIcon = toolbar.NavigationIcon.Callback as Android.Widget.ImageButton;

            navIcon?.SetImageDrawable(context.GetDrawable(resourceId));
        }
    }
}