using Android.Content.Res;
using Bizland.CustomControl;
using Bizland.Droid.CustomRenderer;
using System;
using System.ComponentModel;
using System.Runtime.Remoting.Contexts;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using FAB = Android.Support.Design.Widget.FloatingActionButton;

[assembly: ExportRenderer(typeof(FloatingActionButton), typeof(FloatingActionButtonRenderer))]
namespace Bizland.Droid.CustomRenderer
{
    public class FloatingActionButtonRenderer : Xamarin.Forms.Platform.Android.AppCompat.ViewRenderer<FloatingActionButton, FAB>
    {
#pragma warning disable CS0618 // Type or member is obsolete
        public FloatingActionButtonRenderer()
        {
        }
#pragma warning restore CS0618 // Type or member is obsolete
        protected override void OnElementChanged(ElementChangedEventArgs<FloatingActionButton> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
                return;

            var fab = new FAB(Context);
            // set the bg
            fab.BackgroundTintList = ColorStateList.ValueOf(Element.ButtonColor.ToAndroid());

            // set the icon
            var elementImage = Element.Image;
            var imageFile = elementImage?.File;

            if (imageFile != null)
            {
                fab.SetImageDrawable(Context.Resources.GetDrawable(imageFile));
            }
            fab.Click += Fab_Click;
            SetNativeControl(fab);

        }
        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            Control.BringToFront();
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var fab = Control;
            if (e.PropertyName == nameof(Element.ButtonColor))
            {
                fab.BackgroundTintList = ColorStateList.ValueOf(Element.ButtonColor.ToAndroid());
            }
            if (e.PropertyName == nameof(Element.Image))
            {
                var elementImage = Element.Image;
                var imageFile = elementImage?.File;

                if (imageFile != null)
                {
                    fab.SetImageDrawable(Context.Resources.GetDrawable(imageFile));
                }
            }
            base.OnElementPropertyChanged(sender, e);

        }

        private void Fab_Click(object sender, EventArgs e)
        {
            // proxy the click to the element
            ((IButtonController)Element).SendClicked();
        }
    }
}