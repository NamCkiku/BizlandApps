using Bizland.Core;
using Bizland.Events;
using Prism.Events;
using Prism.Navigation;
using System;
using System.Reflection;
using Xamarin.Forms;

namespace Bizland.ViewModels
{
    public class SelectSexPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;
        public SelectSexPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator)
             : base(navigationService)
        {
            this._navigationService = navigationService;
            this._eventAggregator = eventAggregator;
        }


        public Command SelectedCommand
        {
            get
            {
                return new Command<string>(async (item) =>
                {
                    if (item != null)
                    {
                        try
                        {
                            _eventAggregator.GetEvent<SelectSexEvent>().Publish(item);
                            await _navigationService.GoBackAsync();
                        }
                        catch (Exception ex)
                        {
                            Logger.WriteError(MethodInfo.GetCurrentMethod().Name, ex);
                        }
                    }
                });
            }
        }
    }
}
