using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Bizland.CustomControl
{
    public class CustomSearchBar : SearchBar
    {
        // Use Bindable properties to maintain XAML binding compatibility

        public static BindableProperty BarTintProperty = BindableProperty.Create(nameof(CustomSearchBar), typeof(Color), typeof(CustomSearchBar), Color.Accent);
        public Color? BarTint
        {
            get { return (Color?)GetValue(BarTintProperty); }
            set { SetValue(BarTintProperty, value); }
        }
        public static BindableProperty SearchStyleProperty = BindableProperty.Create(nameof(CustomSearchBar), typeof(string), typeof(CustomSearchBar), "Default");
        public string SearchStyle
        {
            get { return (string)GetValue(SearchStyleProperty); }
            set { SetValue(SearchStyleProperty, value); }
        }
        public static BindableProperty BarStyleProperty = BindableProperty.Create(nameof(CustomSearchBar), typeof(string), typeof(CustomSearchBar), "Default");
        public string BarStyle
        {
            get { return (string)GetValue(BarStyleProperty); }
            set { SetValue(BarStyleProperty, value); }
        }
        public static BindableProperty CancelButtonIsVisibleProperty = BindableProperty.Create(nameof(CustomSearchBar), typeof(bool), typeof(CustomSearchBar), true);
        public bool CancelButtonIsVisible
        {
            get { return (bool)GetValue(CancelButtonIsVisibleProperty); }
            set { SetValue(CancelButtonIsVisibleProperty, value); }
        }
    }
}
