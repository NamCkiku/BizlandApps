using Bizland.Model;
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
            UserToken = StaticSettings.User;
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

        private UserToken _userToken;
        public UserToken UserToken
        {
            get
            {
                if (StaticSettings.User != null)
                {
                    _userToken = StaticSettings.User;
                }
                return _userToken;
            }
            set
            {
                _userToken = value;
                RaisePropertyChanged(() => UserToken);
            }
        }
        public Command NavigateCommand
        {
            get
            {
                return new Command<MenuItem>(async (item) =>
                {
                    if (item != null)
                    {
                        await NavigationService.NavigateAsync(item.Url, null, useModalNavigation: item.UseModalNavigation);
                    }
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
                Title = "Trang chủ",
                Icon = "ic_house.png",
                UseModalNavigation = true,
                Url = "BaseNavigationPage/HomePage"
            });

            MenuItems.Add(new MenuItem
            {
                Title = "Tôi cho thuê",
                Icon = "ic_create.png",
                UseModalNavigation = true,
                Url = "BaseNavigationPage/CreateRoomPage"
            });
            MenuItems.Add(new MenuItem
            {
                Title = "Lịch sử",
                Icon = "ic_time.png",
                UseModalNavigation = true,
                Url = "BaseNavigationPage/HistoryPage"
            });
            MenuItems.Add(new MenuItem
            {
                Title = "Thông tin cá nhân",
                Icon = "ic_house.png",
                UseModalNavigation = true,
                Url = "BaseNavigationPage/ProfilePage"
            });

            MenuItems.Add(new MenuItem
            {
                Title = "Quốc gia",
                Icon = "ic_create.png",
                UseModalNavigation = true,
                Url = "BaseNavigationPage/CountryPage"
            });
            MenuItems.Add(new MenuItem
            {
                Title = "Tỉnh thành",
                Icon = "ic_time.png",
                UseModalNavigation = true,
                Url = "BaseNavigationPage/ProvincePage"
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

        private string _Url;

        public string Url
        {
            get
            {
                return _Url;
            }

            set
            {
                _Url = value;
                RaisePropertyChanged(() => Url);
            }
        }
        private bool useModalNavigation;
        public bool UseModalNavigation
        {
            get
            {
                return useModalNavigation;
            }

            set
            {
                useModalNavigation = value;
                RaisePropertyChanged(() => UseModalNavigation);
            }
        }
    }
}
