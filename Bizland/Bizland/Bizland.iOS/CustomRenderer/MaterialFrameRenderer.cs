using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Bizland.CustomControl;
using Bizland.iOS.CustomRenderer;
using CoreGraphics;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MaterialFrame), typeof(MaterialFrameRenderer))]
namespace Bizland.iOS.CustomRenderer
{
    public class MaterialFrameRenderer : FrameRenderer
    {
        public override void Draw(CGRect rect)
        {
            base.Draw(rect);

            if (Element.HasShadow)
            {
                // Update shadow to match better material design standards of elevation
                this.Layer.CornerRadius = 0f;
                this.Layer.Bounds.Inset(0, 0);
                Layer.BorderColor = Color.FromHex("#dcdde1").ToCGColor();
                Layer.BorderWidth = 0.5f;
                Layer.ShadowRadius = 1;
                Layer.ShadowColor = Color.FromHex("#dcdde1").ToCGColor();
                Layer.ShadowOpacity = 0.6f;
                Layer.ShadowOffset = new SizeF(width: 1, height: 1);
                Layer.MasksToBounds = false;
                Layer.ShadowPath = UIBezierPath.FromRect(Layer.Bounds).CGPath;
            }
        }
    }
}