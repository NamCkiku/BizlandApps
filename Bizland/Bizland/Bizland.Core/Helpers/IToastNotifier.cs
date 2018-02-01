using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bizland.Core.Helpers
{
    public interface IToastNotifier
    {
        Task<bool> Notify(ToastNotificationType type, string title, string description, double duration, object context = null);

        void HideAll();
    }

    public enum ToastNotificationType
    {
        Info,
        Success,
        Error,
        Warning,
    }
}
