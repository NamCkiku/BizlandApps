using System;
using System.Collections.Generic;
using System.Text;

namespace Bizland.Core
{
    public interface IDisplayMessage
    {
        void ShowMessageInfo(string message, double time);
        void ShowMessageWarning(string message, double time);
        void ShowMessageError(string message, double time);

    }
}
