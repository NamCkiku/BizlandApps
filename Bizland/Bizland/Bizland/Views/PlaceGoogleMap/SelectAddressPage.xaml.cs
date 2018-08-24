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

            //Children.Add(new GoogleAutocomplete());
            //Children.Add(new SelectAddressMapPage());
        }
    }
}