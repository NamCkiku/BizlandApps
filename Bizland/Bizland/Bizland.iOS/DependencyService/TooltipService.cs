using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bizland.Core;
using Foundation;
using UIKit;

namespace Bizland.iOS.DependencyService
{
    public class TooltipService : ITooltipService
    {


        public void ShowToast(string message, Xamarin.Forms.View view)
        {
            var etv = new EasyTipView.EasyTipView();
            etv.Text = new Foundation.NSString(message);
            etv.ArrowPosition = EasyTipView.ArrowPosition.Top;
            etv.CornerRadius = 5;

            etv.Show(new UIView(), new UIView(), true);
        }
    }
}