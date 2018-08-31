using Android.Content;
using Bizland.Droid.CustomRenderer;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Xamarin.Forms.ScrollView), typeof(Scrollbardisabledrenderer))]
namespace Bizland.Droid.CustomRenderer
{
    public class Scrollbardisabledrenderer : ScrollViewRenderer
    {
        public Scrollbardisabledrenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || this.Element == null)
                return;

            if (e.OldElement != null)
                e.OldElement.PropertyChanged -= OnElementPropertyChanged;

            e.NewElement.PropertyChanged += OnElementPropertyChanged;



        }

        protected void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.HorizontalScrollBarEnabled = false;
            this.VerticalScrollBarEnabled = false;
        }
    }
}