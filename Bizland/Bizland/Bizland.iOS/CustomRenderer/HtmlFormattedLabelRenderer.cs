using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bizland.CustomControl;
using Bizland.iOS.CustomRenderer;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(HtmlFormattedLabel), typeof(HtmlFormattedLabelRenderer))]
namespace Bizland.iOS.CustomRenderer
{
    public class HtmlFormattedLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            var view = (HtmlFormattedLabel)Element;
            if (view == null) return;

            //Original Credits : https://forums.xamarin.com/discussion/23670/how-to-display-html-formatted-text-in-a-uilabel
            var attr = new NSAttributedStringDocumentAttributes();
            var nsError = new NSError();
            attr.DocumentType = NSDocumentType.HTML;

            Control.AttributedText = new NSAttributedString(view.Text, attr, ref nsError);
        }
    }
}