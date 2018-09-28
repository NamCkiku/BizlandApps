using Bizland.Model;
using Prism.Navigation;

namespace Bizland.ViewModels
{
    public class ProfilePageViewModel : ViewModelBase
    {
        public ProfilePageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Thông tin cá nhân";
            UserToken = StaticSettings.User;
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
    }
}
