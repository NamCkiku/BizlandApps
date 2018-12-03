﻿using Android.Content;
using Android.Support.V7.Graphics.Drawable;
using Android.Widget;
using Bizland.Droid.CustomRenderer;
using Bizland.Views;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(RootPage), typeof(IconNavigationPageRenderer))]
namespace Bizland.Droid.CustomRenderer
{
    public class IconNavigationPageRenderer : MasterDetailPageRenderer
    {
        public IconNavigationPageRenderer(Context context) : base(context)
        {
        }

        private static Android.Support.V7.Widget.Toolbar GetToolbar() => (CrossCurrentActivity.Current?.Activity as MainActivity)?.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            var toolbar = GetToolbar();
            if (toolbar != null)
            {
                for (var i = 0; i < toolbar.ChildCount; i++)
                {
                    var imageButton = toolbar.GetChildAt(i) as Android.Widget.ImageButton;

                    var drawerArrow = imageButton?.Drawable as DrawerArrowDrawable;
                    if (drawerArrow == null)
                        continue;

                    imageButton.SetImageDrawable(Forms.Context.GetDrawable(Resource.Drawable.hamburger));
                }
            }
        }
    }
}