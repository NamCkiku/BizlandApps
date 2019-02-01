using System;
using System.Collections.Generic;
using System.Text;

namespace Bizland.Core
{
    public interface IDisplayMessage
    {
        void ShowMessageInfo(string message, double time = 3000);
        void ShowMessageWarning(string message, double time = 3000);
        void ShowMessageError(string message, double time = 3000);
        void ShowMessageSuccess(string message, double time = 3000);
        void ShowToast(string message, double time = 3000);

    }
}
