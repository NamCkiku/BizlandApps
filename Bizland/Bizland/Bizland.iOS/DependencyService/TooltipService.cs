using Bizland.Core;
using Bizland.iOS.DependencyService;
using CoreGraphics;
using System;
using System.Linq;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(TooltipService))]
namespace Bizland.iOS.DependencyService
{
    public class TooltipService : ITooltipService
    {


        public void ShowToast(string message, Xamarin.Forms.Point point)
        {
            var etv = new EasyTipView.EasyTipView();
            etv.Text = new Foundation.NSString(message);
            etv.ArrowPosition = EasyTipView.ArrowPosition.Top;
            etv.CornerRadius = 5;
            var window = GetCurrentWindow();
            etv.Show(new UIView(new CGRect(point.X, point.Y, 100, 50)), new UIView(new CGRect(0, 0, window.Bounds.Width, window.Bounds.Height)), true);
        }

        public UIWindow GetCurrentWindow()
        {
            UIViewController viewController = null;
            UIWindow window = UIApplication.SharedApplication.KeyWindow;
            if (window == null)
                throw new InvalidOperationException("There's no current active window");

            if (window.WindowLevel == UIWindowLevel.Normal)
                viewController = window.RootViewController;

            if (viewController == null)
            {
                window = UIApplication.SharedApplication.Windows.OrderByDescending(w => w.WindowLevel).FirstOrDefault(w => w.RootViewController != null && w.WindowLevel == UIWindowLevel.Normal);
                if (window == null)
                    throw new InvalidOperationException("Could not find current view controller");
                else
                    viewController = window.RootViewController;
            }

            while (viewController.PresentedViewController != null)
                viewController = viewController.PresentedViewController;

            return (UIWindow)viewController.View.Superview;
        }
    }
}