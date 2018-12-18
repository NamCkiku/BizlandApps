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
            throw new System.NotImplementedException();
        }

        public void ShowMessageInfo(string message, double time)
        {
            throw new System.NotImplementedException();
        }

        public void ShowMessageWarning(string message, double time)
        {
            throw new System.NotImplementedException();
        }
    }
}