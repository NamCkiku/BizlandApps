using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bizland.Core;
using Bizland.iOS.Providers;
using Foundation;
using MessageBar;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(ToastNotifier))]
namespace Bizland.iOS.Providers
{
    public class ToastNotifier : IToastNotifier
    {
        public static void Init()
        {
            //_styleSheet = new MessageBarStyleSheet();
        }

        public Task<bool> Notify(ToastNotificationType type, string title, string description, double duration, object context = null)
        {
            MessageType msgType = MessageType.Info;

            switch (type)
            {
                case ToastNotificationType.Error:
                case ToastNotificationType.Warning:
                    msgType = MessageType.Error;
                    break;

                case ToastNotificationType.Success:
                    msgType = MessageType.Success;
                    break;
            }

            var taskCompletionSource = new TaskCompletionSource<bool>();
            MessageBarManager.SharedInstance.ShowMessage(title, description, msgType, duration, b => taskCompletionSource.TrySetResult(b));
            return taskCompletionSource.Task;
        }

        public void HideAll()
        {
            MessageBarManager.SharedInstance.HideAll();
        }
    }
}