using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Bizland.Core;
using IT.Sephiroth.Android.Library.Tooltip;
using Xamarin.Forms;

namespace Bizland.Droid.DependencyService
{
    public class TooltipService : ITooltipService
    {

        public void ShowToast(string message, Xamarin.Forms.View view)
        {
            var viewnative = Bizland.Droid.Extensions.ViewExtensions.GetNativeView(view);
            Tooltip.Make(
                Forms.Context,
#pragma warning disable CS0618 // Type or member is obsolete
                new Tooltip.Builder(101)
                    .Anchor(viewnative, Tooltip.Gravity.Top)
                    .ClosePolicy(Tooltip.ClosePolicy.TouchAnywhereConsume, 3000)
                    .Text("Tooltip on a TabLayout child...")
                    .FadeDuration(200)
                    .FitToScreen(false)
                    .MaxWidth(400)
                    .ShowDelay(400)
                    .ToggleArrow(true)
#pragma warning restore CS0618 // Type or member is obsolete
                    .Build()
            ).Show();
        }
    }
}