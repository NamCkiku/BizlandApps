using Bizland.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace Bizland.Core
{
    public static class Extensions
    {
        public static void ToToast(this string message, ToastNotificationType type = ToastNotificationType.Info, string title = null, double timespan = 3.0f)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var toaster = Xamarin.Forms.DependencyService.Get<IToastNotifier>();
                toaster?.Notify(type, title ?? type.ToString().ToUpper(), message, timespan);
            });
        }


        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> source)
        {
            ObservableCollection<T> collection = new ObservableCollection<T>();

            foreach (T item in source)
            {
                collection.Add(item);
            }

            return collection;

        }
    }
}
