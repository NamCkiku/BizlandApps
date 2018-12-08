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
    public partial class BindingPinView : Grid
    {
        private string _imagePin;
        public BindingPinView(string image)
        {
            InitializeComponent();
            _imagePin = image;
            BindingContext = this;
        }
        public string ImagePin
        {
            get { return _imagePin; }
        }
    }
}