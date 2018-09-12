using Bizland.CustomControl;
using Bizland.iOS.CustomRenderer;
using CoreGraphics;
using Foundation;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(FloatingActionButton), typeof(FloatingActionButtonRenderer))]
namespace Bizland.iOS.CustomRenderer
{
    [Preserve]
    public class FloatingActionButtonRenderer : ButtonRenderer
    {
        public FloatingActionButtonRenderer()
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
                return;

            // remove text from button and set the width/height/radius
            Element.HeightRequest = Element.WidthRequest;
            Element.CornerRadius = 10;
            Element.BorderWidth = 0;
            Element.Text = null;

            // set background
            Control.BackgroundColor = ((FloatingActionButton)Element).ButtonColor.ToUIColor();
        }
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);
            // add shadow
            Layer.ShadowRadius = 2.0f;
            Layer.ShadowColor = UIColor.Black.CGColor;
            Layer.ShadowOffset = new CGSize(1, 1);
            Layer.ShadowOpacity = 0.80f;
            Layer.ShadowPath = UIBezierPath.FromOval(Layer.Bounds).CGPath;
            Layer.MasksToBounds = false;

        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == "ButtonColor")
            {
                Control.BackgroundColor = ((FloatingActionButton)Element).ButtonColor.ToUIColor();
            }
        }
    }
}