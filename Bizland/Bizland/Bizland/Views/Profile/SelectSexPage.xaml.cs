using Rg.Plugins.Popup.Pages;
using System.Collections.Generic;

namespace Bizland.Views
{
    public partial class SelectSexPage : PopupPage
    {
        public SelectSexPage()
        {
            InitializeComponent();

            var lstSex = new List<string>();

            lstSex.Add("Nam");

            lstSex.Add("Nữ");

            lstSex.Add("Chưa xác định");

            listviewSex.ItemsSource = lstSex;
        }
    }
}
