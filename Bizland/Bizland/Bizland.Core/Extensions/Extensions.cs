using Bizland.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Bizland.Core.Extensions
{
    public static class Extensions
    {
        public static void ToToast(this string message, ToastNotificationType type = ToastNotificationType.Info, string title = null, double timespan = 3.0f)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var toaster = DependencyService.Get<IToastNotifier>();
                toaster?.Notify(type, title ?? type.ToString().ToUpper(), message, timespan);
            });
        }

    }
}
