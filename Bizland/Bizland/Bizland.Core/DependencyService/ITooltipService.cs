using System;
using System.Collections.Generic;
using System.Text;

namespace Bizland.Core
{
    public interface ITooltipService
    {
        void ShowToast(string message, Xamarin.Forms.View view);
    }
}
