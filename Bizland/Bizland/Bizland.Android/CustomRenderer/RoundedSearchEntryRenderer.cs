using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using Bizland.CustomControl;
using Bizland.Droid.CustomRenderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RoundedSearchEntry), typeof(RoundedSearchEntryRenderer))]
namespace Bizland.Droid.CustomRenderer
{
    public class RoundedSearchEntryRenderer : EntryRenderer
    {
        private RoundedSearchEntry _element;
        public RoundedSearchEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            //check if the entry is not rendered yet
            if (e.OldElement != null || e.NewElement == null)
                return;

            //Borders Renderer
            _element = (RoundedSearchEntry)Element;
            var gradientDrawable = new GradientDrawable();
            gradientDrawable.SetCornerRadius(_element.BorderRadius);
            gradientDrawable.SetStroke(_element.BorderWidth, _element.BorderColor.ToAndroid());
            gradientDrawable.SetColor(_element.Color.ToAndroid());

            Control.SetBackground(gradientDrawable);


            Control.SetPadding(10, 10, 10, 10);

            // Icon Renderer
            if (!string.IsNullOrEmpty(_element.Image))
            {
                switch (_element.ImageAlignment)
                {
                    case ImageAlignment.Left:
                        Control.SetCompoundDrawablesWithIntrinsicBounds(GetDrawable(_element.Image), null, null, null);
                        break;
                    case ImageAlignment.Right:
                        Control.SetCompoundDrawablesWithIntrinsicBounds(null, null, GetDrawable(_element.Image), null);
                        break;
                }
            }
            Control.CompoundDrawablePadding = 10;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roundedEntryImage">Image name</param>
        /// <returns>Bitmap image to show inside the entry </returns>
        private BitmapDrawable GetDrawable(string roundedEntryImage)
        {
            var d = Resources.GetDrawable(roundedEntryImage).Mutate();
            var bitmap = ((BitmapDrawable)d).Bitmap;

            return new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, _element.ImageWidth * 2, _element.ImageHeight * 2, true));
        }
    }
}