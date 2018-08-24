using Prism.Navigation;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace Bizland.ViewModels
{
    public class MenuPageViewModel : ViewModelBase
    {
        private ObservableCollection<MenuItem> menuItems = new ObservableCollection<MenuItem>();
        public MenuPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            InitMenuItems();
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            InitMenuItems();
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {

        }
        public Command NavigateCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await NavigationService.NavigateAsync("BaseNavigationPage/MainPage", null, useModalNavigation: true);
                });
            }
        }
        public ObservableCollection<MenuItem> MenuItems
        {
            get
            {
                return menuItems;
            }
            set
            {
                menuItems = value;
                RaisePropertyChanged(() => MenuItems);
            }
        }
        private void InitMenuItems()
        {
            MenuItems.Add(new MenuItem
            {
                Title = "Khách chờ đón",
                Icon = "ic_Search.png"
            });

            MenuItems.Add(new MenuItem
            {
                Title = "Danh sách cuốc đăng",
                Icon = "ic_user.png"
            });
            MenuItems.Add(new MenuItem
            {
                Title = "Danh sách cuốc đăng",
                Icon = "ic_pass.png"
            });
            MenuItems.Add(new MenuItem
            {
                Title = "Khách chờ đón",
                Icon = "ic_Search.png"
            });

            MenuItems.Add(new MenuItem
            {
                Title = "Danh sách cuốc đăng",
                Icon = "ic_user.png"
            });
            MenuItems.Add(new MenuItem
            {
                Title = "Danh sách cuốc đăng",
                Icon = "ic_pass.png"
            });
        }
    }

    public class MenuItem : ExtendedBindableObject
    {
        private string _title;

        public string Title
        {
            get
            {
                return _title;
            }

            set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        private string _icon;

        public string Icon
        {
            get
            {
                return _icon;
            }

            set
            {
                _icon = value;
                RaisePropertyChanged(() => Icon);
            }
        }
    }
}
