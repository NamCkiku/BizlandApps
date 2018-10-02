using Bizland.Core;
using Bizland.Events;
using Bizland.Model;
using Prism.Events;
using Prism.Navigation;
using System;
using System.Reflection;
using Xamarin.Forms;

namespace Bizland.ViewModels
{
    public class ProfilePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public ProfilePageViewModel(INavigationService navigationService, IEventAggregator eventAggregator)
            : base(navigationService)
        {
            this._navigationService = navigationService;
            Title = "Thông tin cá nhân";

            eventAggregator.GetEvent<SelectDateTimeEvent>().Subscribe(UpdateBirthday);

            eventAggregator.GetEvent<SelectSexEvent>().Subscribe(UpdateSex);
        }

        public void UpdateBirthday(DateTime param)
        {
            BirthDay = param.ToString("dd/MM/yyyy");
        }
        public void UpdateSex(string param)
        {
            Sex = param;
        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (UserToken == null)
            {
                UserToken = StaticSettings.User;
                //UserToken.Avatar = ServerConfig.ApiEndpoint + StaticSettings.User.Avatar;
            }
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

        private string _birthDay;
        public string BirthDay
        {
            get
            {

                return _birthDay;
            }
            set
            {
                _birthDay = value;
                RaisePropertyChanged(() => BirthDay);
            }
        }

        private string _sex;
        public string Sex
        {
            get
            {

                return _sex;
            }
            set
            {
                _sex = value;
                RaisePropertyChanged(() => Sex);
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
                    if (IsBusy)
                    {
                        return;
                    }
                    IsBusy = true;
                    try
                    {
                        await _navigationService.NavigateAsync("SelectDatetimePage");
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteError(MethodInfo.GetCurrentMethod().Name, ex);
                    }
                    finally
                    {
                        IsBusy = false;
                    }
                });
            }
        }

        public Command PusuSelectSexCommand
        {
            get
            {
                return new Command(async () =>
                {
                    if (IsBusy)
                    {
                        return;
                    }
                    IsBusy = true;
                    try
                    {
                        await _navigationService.NavigateAsync("SelectSexPage");
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteError(MethodInfo.GetCurrentMethod().Name, ex);
                    }
                    finally
                    {
                        IsBusy = false;
                    }
                });
            }
        }

    }
}
