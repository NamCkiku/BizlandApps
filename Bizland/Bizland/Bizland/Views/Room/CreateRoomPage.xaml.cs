using Xamarin.Forms;
using Xamarin.RangeSlider.Common;

namespace Bizland.Views
{
    public partial class CreateRoomPage : ContentPage
    {
        public CreateRoomPage()
        {
            InitializeComponent();
            RangeSlider.FormatLabel = FormaLabel;
        }
        private string FormaLabel(Thumb thumb, float val)
        {
            return val + " triệu";
        }
    }
}
