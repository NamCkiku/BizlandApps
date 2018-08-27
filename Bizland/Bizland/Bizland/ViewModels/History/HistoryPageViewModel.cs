using Prism.Navigation;

namespace Bizland.ViewModels
{
    public class HistoryPageViewModel : ViewModelBase
    {
        public HistoryPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Lịch sử";
        }
    }
}
