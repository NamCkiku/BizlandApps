using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bizland.Core;
using Bizland.iOS.DependencyService;
using Bizland.iOS.Utils;
using Foundation;
using ObjCRuntime;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: Dependency(typeof(ToolbarItemBadgeService))]
namespace Bizland.iOS.DependencyService
{
    public class ToolbarItemBadgeService : IToolbarItemBadgeService
    {
        public void SetBadge(Page page, ToolbarItem item, string value, Color backgroundColor, Color textColor)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var renderer = Xamarin.Forms.Platform.iOS.Platform.GetRenderer(page);
                if (renderer == null)
                {
                    renderer = Xamarin.Forms.Platform.iOS.Platform.CreateRenderer(page);
                    Xamarin.Forms.Platform.iOS.Platform.SetRenderer(page, renderer);
                }
                var vc = renderer.ViewController;

                var rightButtomItems = vc?.ParentViewController?.NavigationItem?.RightBarButtonItems;
                var idx = page.ToolbarItems.IndexOf(item);
                if (rightButtomItems != null && rightButtomItems.Length > idx)
                {
                    var barItem = rightButtomItems[idx];
                    if (barItem != null)
                    {
                        barItem.UpdateBadge(value, backgroundColor.ToUIColor(), textColor.ToUIColor());
                    }
                }

            });
        }
    }
}