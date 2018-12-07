using Bizland.iOS.CustomRenderer;
using Bizland.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

//[assembly: ExportRenderer(typeof(TransparentNavigation), typeof(UntintedNavigationPageRenderer))]
namespace Bizland.iOS.CustomRenderer
{
    public class UntintedNavigationPageRenderer : NavigationRenderer
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var item = this.NavigationBar?.TopItem?.LeftBarButtonItem;

            if (item != null)
                item.Image = item.Image.ImageWithRenderingMode(UIImageRenderingMode.AlwaysOriginal);
        }
    }
}