using Naxam.Controls.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bizland.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectAddressPage : TopTabbedPage
    {
        public SelectAddressPage()
        {
            InitializeComponent();
            btnClose.Clicked += CloseModal;

            //Children.Add(new GoogleAutocomplete());
            //Children.Add(new SelectAddressMapPage());
        }

        private async void CloseModal(object sender, EventArgs e)
        {
            if(Navigation.ModalStack.Count > 0)
            {
                await Navigation.PopModalAsync(true);
            }  
        }
    }
}