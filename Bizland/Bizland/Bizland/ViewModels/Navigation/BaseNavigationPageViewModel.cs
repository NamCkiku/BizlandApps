using Prism.Navigation;

namespace Bizland.ViewModels
{
    public class BaseNavigationPageViewModel : ViewModelBase
    {
        public BaseNavigationPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
        }
    }
}
