using System;
using Xamarin.Forms;

namespace Bizland.Views
{
    public partial class MenuPage : ContentPage
    {
        public event EventHandler Button_ClickedOnMenu;
        public MenuPage()
        {
            InitializeComponent();
        }

        private void lvMenuList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Button_ClickedOnMenu?.Invoke(sender, EventArgs.Empty);
        }
    }
}
