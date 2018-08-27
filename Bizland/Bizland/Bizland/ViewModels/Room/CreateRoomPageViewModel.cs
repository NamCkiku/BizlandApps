using Prism.Navigation;

namespace Bizland.ViewModels
{
    public class CreateRoomPageViewModel : ViewModelBase
    {
        public CreateRoomPageViewModel(INavigationService navigationService)
             : base(navigationService)
        {
            Title = "Trang chủ";

        }
    }
}
