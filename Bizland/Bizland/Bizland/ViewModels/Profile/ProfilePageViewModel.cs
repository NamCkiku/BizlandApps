using Bizland.Core;
using Bizland.Core.Helpers;
using Bizland.Model;
using Prism.Navigation;
using System;
using System.Reflection;
using Xamarin.Forms;

namespace Bizland.ViewModels
{
    public class ProfilePageViewModel : ViewModelBase
    {
        public ProfilePageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Thông tin cá nhân";
            UserToken = StaticSettings.User;
            UserToken.Avatar = ServerConfig.ApiEndpoint + StaticSettings.User.Avatar;
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




        public Command ChangeAvatarCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        string localPath = await UploadImageHelper.UploadImage(ImageType.Avatar);
                        UserToken.Avatar = localPath;
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
