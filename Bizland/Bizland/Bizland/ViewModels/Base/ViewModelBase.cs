using Prism.Navigation;
using Xamarin.Forms;

namespace Bizland.ViewModels
{
    public class ViewModelBase : ExtendedBindableObject, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool isBusy = false;

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                RaisePropertyChanged(() => IsBusy);
            }
        }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public virtual void Destroy()
        {

        }
        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatingTo(INavigationParameters parameters)
        {
        }

        public Command ClosePageCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await NavigationService.GoBackAsync(useModalNavigation: true);
                });
            }
        }
    }
}
