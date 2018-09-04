using Prism.Navigation;

namespace Bizland.ViewModels
{
    public class ProvincePageViewModel : ViewModelBase
    {
        public ProvincePageViewModel(INavigationService navigationService)
             : base(navigationService)
        {
            Title = "Thêm phòng";
        }
    }
}
