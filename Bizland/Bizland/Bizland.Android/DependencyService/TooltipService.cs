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
using static IT.Sephiroth.Android.Library.Tooltip.Tooltip;

namespace Bizland.Droid.DependencyService
{
    public class TooltipService : ITooltipService
    {

        public void ShowToast(string message, Xamarin.Forms.View view)
        {
            //var viewnative = Bizland.Droid.Extensions.ViewExtensions.GetNativeView(view);
            Tooltip.Make(
                Forms.Context,
#pragma warning disable CS0618 // Type or member is obsolete
                new Tooltip.Builder(101)
                    .Anchor(new Android.Graphics.Point(350, 250), Tooltip.Gravity.Center)
                    .ClosePolicy(Tooltip.ClosePolicy.TouchAnywhereNoConsume, 3000)
                    .Text(message)
                    .FadeDuration(1000)
                    .ActivateDelay(2000)
                    .FitToScreen(false)
                    .MaxWidth(400)
                    .ShowDelay(200)
                    .ToggleArrow(true)
                    .FloatingAnimation(Tooltip.AnimationBuilder.Slow)
                    .WithOverlay(true)
                    .WithArrow(true)
#pragma warning restore CS0618 // Type or member is obsolete
                    .Build()
            ).Show();
        }
    }
}