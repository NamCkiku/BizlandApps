using Bizland.Core;
using Bizland.Model;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Reflection;
using Xamarin.Forms;

namespace Bizland.ViewModels
{
    public class ProfilePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public ProfilePageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            this._navigationService = navigationService;
            Title = "Thông tin cá nhân";
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (UserToken == null)
            {
                UserToken = StaticSettings.User;
                UserToken.Avatar = ServerConfig.ApiEndpoint + StaticSettings.User.Avatar;
            }
            Sexs = new ObservableCollection<string>();

            Sexs.Add("Nam");

            Sexs.Add("Nữ");

            Sexs.Add("Chưa xác định");

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


        private ObservableCollection<string> _sex;

        public ObservableCollection<string> Sexs

        {

            get
            {
                return _sex;
            }
            set
            {
                _sex = value;
                RaisePropertyChanged(() => Sexs);
            }

        }



        public Command OkButtonClicked
        {
            get
            {
                return new Command<string>((arg) =>
                {
                    try
                    {
                        if (arg != null)
                        {

                        }

                    }
                    catch (Exception ex)
                    {
                        Logger.WriteError(MethodInfo.GetCurrentMethod().Name, ex);
                    }
                });
            }
        }

        public Command PusuSelectDateCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        await _navigationService.NavigateAsync("SelectDatetimePage");
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteError(MethodInfo.GetCurrentMethod().Name, ex);
                    }
                });
            }
        }

    }
}
