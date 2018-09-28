using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BigTed;
using Bizland.Interfaces;
using Bizland.iOS.DependencyService;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(HUDService))]
namespace Bizland.iOS.DependencyService
{
    public class HUDService : IHUDProvider
    {
        public async void DisplayProgress(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                BTProgressHUD.Show(null, -1, ProgressHUD.MaskType.Black);
            }
            else
            {
                BTProgressHUD.Show(message, -1, ProgressHUD.MaskType.Black);
            }
        }

        public void DisplaySuccess(string message)
        {
            BTProgressHUD.ShowSuccessWithStatus(message);
        }

        public void DisplayError(string message)
        {
            BTProgressHUD.ShowErrorWithStatus(message);
        }

        public void Dismiss()
        {
            BTProgressHUD.Dismiss();
        }

        public void ShowToast(string message, double time)
        {
            BTProgressHUD.ShowToast(message, toastPosition: ProgressHUD.ToastPosition.Center, timeoutMs: time);
        }
    }
}