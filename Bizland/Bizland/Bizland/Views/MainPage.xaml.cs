using Bizland.CustomControl;
using Bizland.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Bizland.Views
{
    public partial class MainPage : LogoPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void buttonLoadingPage1Timer_OnClicked(object sender, EventArgs e)
        {
            // show the loading page...
            DependencyService.Get<ILoadingPageService>().InitLoadingPage(new LoadingIndicatorPage());
            DependencyService.Get<ILoadingPageService>().ShowLoadingPage();

            // just to showcase a delay...
            await Task.Delay(5000);

            // close the loading page...
            DependencyService.Get<ILoadingPageService>().HideLoadingPage();
        }

        private async void buttonLoadingPage2Timer_OnClicked(object sender, EventArgs e)
        {
            // show the loading page...
            DependencyService.Get<ILoadingPageService>().InitLoadingPage(new LoadingIndicatorPage());
            DependencyService.Get<ILoadingPageService>().ShowLoadingPage();

            // just to showcase a delay...
            await Task.Delay(5000);

            // close the loading page...
            DependencyService.Get<ILoadingPageService>().HideLoadingPage();
        }
    }
}