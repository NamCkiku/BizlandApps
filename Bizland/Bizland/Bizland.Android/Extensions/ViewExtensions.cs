using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Bizland.Droid.Extensions
{
    using NativeView = global::Android.Views.View;
    /// <summary>
    /// Class ViewExtensions.
    /// </summary>
    public static class ViewExtensions
    {
        /// <summary>
        /// Finds the forms view from accessibility identifier.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="nativeView">The native view.</param>
        /// <returns>View.</returns>
        public static Xamarin.Forms.View FindFormsViewFromAccessibilityId(this Xamarin.Forms.View view, NativeView nativeView)
        {
            Xamarin.Forms.View formsView = null;

            var id = nativeView.ContentDescription;

            if (string.IsNullOrWhiteSpace(id))
            {
                formsView = null;
            }
            else if (view.StyleId == id)
            {
                formsView = view;
            }
            else
            {
                var d = view.GetInternalChildren();

                formsView = d == null ? null : d.OfType<Xamarin.Forms.View>().FirstOrDefault(a => a.StyleId == id);
            }

            return formsView;
        }
        /// <summary>
        /// Gets the internal children.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <returns>ObservableCollection&lt;Element&gt;.</returns>
        public static ObservableCollection<Element> GetInternalChildren(this Xamarin.Forms.View view)
        {
            var internalPropertyInfo = view.GetType().GetProperty("InternalChildren", BindingFlags.NonPublic | BindingFlags.Instance);

            return (internalPropertyInfo == null) ? null : internalPropertyInfo.GetValue(view) as ObservableCollection<Element>;
        }

        /// <summary>
        /// Gets the content of the native.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <returns>NativeView.</returns>
        public static NativeView GetNativeContent(this Xamarin.Forms.View view)
        {
            PropertyInfo controlProperty = view.GetType().GetProperty("Control", BindingFlags.Public | BindingFlags.Instance);

            return (controlProperty == null) ? null : controlProperty.GetValue(view) as NativeView;
        }

        public static Android.Views.View GetNativeView(this VisualElement view)
        {
            if (Platform.GetRenderer(view) == null)
                Platform.SetRenderer(view, Platform.CreateRendererWithContext(view, Forms.Context));
            var renderer = Platform.GetRenderer(view);
            renderer.Tracker.UpdateLayout();

            return renderer.ViewGroup;
        }
    }
}