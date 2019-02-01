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
using Bizland.Droid.DependencyService;
using Xamarin.Forms;

[assembly: Dependency(typeof(DisplayMessageService))]
namespace Bizland.Droid.DependencyService
{
    public class DisplayMessageService : IDisplayMessage
    {
        public void ShowMessageError(string message, double time)
        {
            AndroidHUD.AndHUD.Shared.ShowToast(Forms.Context, message, maskType: AndroidHUD.MaskType.None, timeout: TimeSpan.FromMilliseconds(time));
        }

        public void ShowMessageInfo(string message, double time)
        {
            AndroidHUD.AndHUD.Shared.ShowToast(Forms.Context, message, maskType: AndroidHUD.MaskType.None, timeout: TimeSpan.FromMilliseconds(time));
        }

        public void ShowMessageWarning(string message, double time)
        {
            AndroidHUD.AndHUD.Shared.ShowToast(Forms.Context, message, maskType: AndroidHUD.MaskType.None, timeout: TimeSpan.FromMilliseconds(time));
        }

        public void ShowMessageSuccess(string message, double time)
        {
            AndroidHUD.AndHUD.Shared.ShowToast(Forms.Context, message, maskType: AndroidHUD.MaskType.None, timeout: TimeSpan.FromMilliseconds(time));
        }

        public void ShowToast(string message, double time)
        {
            AndroidHUD.AndHUD.Shared.ShowToast(Forms.Context, message, maskType: AndroidHUD.MaskType.None, timeout: TimeSpan.FromMilliseconds(time));
        }
    }
}