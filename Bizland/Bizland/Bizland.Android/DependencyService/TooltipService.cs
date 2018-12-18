using Bizland.Core;
using Bizland.Droid.DependencyService;
using IT.Sephiroth.Android.Library.Tooltip;
using Xamarin.Forms;

[assembly: Dependency(typeof(TooltipService))]
namespace Bizland.Droid.DependencyService
{
    public class TooltipService : ITooltipService
    {

        public void ShowToast(string message, Xamarin.Forms.Point point)
        {
            //var viewnative = Bizland.Droid.Extensions.ViewExtensions.GetNativeView(view);
            Tooltip.Make(
                Forms.Context,
#pragma warning disable CS0618 // Type or member is obsolete
                new Tooltip.Builder(101)
                    .Anchor(new Android.Graphics.Point((int)point.X, (int)point.Y), Tooltip.Gravity.Top)
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