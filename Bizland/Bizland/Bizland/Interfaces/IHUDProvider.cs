using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Bizland.Interfaces
{
    public interface IHUDProvider
    {
        void DisplayProgress(string message);

        void DisplaySuccess(string message);

        void DisplayError(string message);

        void Dismiss();
    }

    public class HUD : IDisposable
    {
        public HUD(string message)
        {
            StartHUD(message);
        }

        async void StartHUD(string message)
        {
            await Task.Delay(100);
            App.Instance.Hud.DisplayProgress(message);
        }

        public void Dispose()
        {
            App.Instance.Hud.Dismiss();
        }
    }
}
