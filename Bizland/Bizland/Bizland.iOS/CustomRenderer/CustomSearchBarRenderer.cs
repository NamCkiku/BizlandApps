using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Bizland.CustomControl;
using Bizland.iOS.CustomRenderer;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRendererAttribute(typeof(CustomSearchBar), typeof(CustomSearchBarRenderer))]
namespace Bizland.iOS.CustomRenderer
{
    public class CustomSearchBarRenderer : SearchBarRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<SearchBar> args)
        {
            base.OnElementChanged(args);

            if (args.OldElement != null || args.NewElement == null)
                return;

            var csb = (CustomSearchBar)Element;
            UISearchBar bar = (UISearchBar)this.Control;

            bar.AutocapitalizationType = UITextAutocapitalizationType.AllCharacters;
            bar.AutocorrectionType = UITextAutocorrectionType.Default;
            bar.BarStyle = (UIBarStyle)Enum.Parse(typeof(UIBarStyle), csb.BarStyle);
            //bar.BarTintColor = csb.BarTint.GetValueOrDefault().ToUIColor();
            bar.KeyboardType = UIKeyboardType.Default;
            bar.SearchBarStyle = (UISearchBarStyle)Enum.Parse(typeof(UISearchBarStyle), csb.BarStyle);
            bar.ShowsScopeBar = true;
            bar.ShowsCancelButton = csb.CancelButtonIsVisible;
        }
    }
}