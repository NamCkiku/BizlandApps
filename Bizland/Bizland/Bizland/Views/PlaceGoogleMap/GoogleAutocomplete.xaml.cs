using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bizland.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GoogleAutocomplete : ContentPage
    {
        public GoogleAutocomplete()
        {
            InitializeComponent();

            Appearing += async (object sender, EventArgs e) =>
            {
                if (Device.RuntimePlatform == Device.Android)
                {
                    await Task.Delay(600);
                    searchbarAndroid.Focus();
                }
                if (Device.RuntimePlatform == Device.iOS)
                {
                    await Task.Delay(300);
                    searchBar.Focus();
                }

            };

        }
    }
}