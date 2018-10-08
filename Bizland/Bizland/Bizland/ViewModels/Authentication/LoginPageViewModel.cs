using Bizland.ApiService;
using Bizland.Core;
using Newtonsoft.Json;
using Prism.Navigation;
using System;
using System.Reflection;
using Xamarin.Forms;

namespace Bizland.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IAuthenticationService _authenticationService;
        public LoginPageViewModel(INavigationService navigationService, IAuthenticationService authenticationService)
            : base(navigationService)
        {
            Title = "Đăng Nhập";
            this._navigationService = navigationService;
            this._authenticationService = authenticationService;

            _userName = new ValidatableObject<string>();
            _password = new ValidatableObject<string>();

            AddValidations();
        }
        private ValidatableObject<string> _userName;
        private ValidatableObject<string> _password;

        public ValidatableObject<string> UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        public ValidatableObject<string> Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }
        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                RaisePropertyChanged(() => Message);
            }
        }

        private bool Validate()
        {
            bool isValidUser = ValidateUserName();

            bool isValidPassword = ValidatePassword();

            return isValidUser && isValidPassword;
        }

        private bool ValidateUserName()
        {
            return _userName.Validate();
        }

        private bool ValidatePassword()
        {
            return _password.Validate();
        }
        private void AddValidations()
        {
            _userName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Vvui lòng nhập tài khoản." });
            _password.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "Vui lòng nhập mật khẩu." });
        }

        public Command LoginCommand
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
                        bool isValid = Validate();
                        if (isValid)
                        {
                            // Lấy thông tin token
                            var token = await _authenticationService.GetTokenAsync(UserName.Value, Password.Value);
                            if (token != null && !string.IsNullOrEmpty(token.AccessToken))
                            {
                                Settings.AuthAccessToken = token.AccessToken;
                                Settings.UserInfomation = JsonConvert.SerializeObject(token);
                                StaticSettings.User = token;
                                await _navigationService.NavigateAsync("/RootPage/TransparentNavigation/HomePage");
                            }
                            else
                            {
                                Message = "Tài khoản hoặc mật khẩu không chính xác";
                            }
                        }
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
