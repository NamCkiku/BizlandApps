using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Bizland.ViewModels
{
    public class SelectDatetimePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public SelectDatetimePageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            this._navigationService = navigationService;

            ObservableCollection<object> todaycollection = new ObservableCollection<object>();

            //Select today dates
            todaycollection.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Date.Month).Substring(0, 3));
            if (DateTime.Now.Date.Day < 10)
                todaycollection.Add("0" + DateTime.Now.Date.Day);
            else
                todaycollection.Add(DateTime.Now.Date.Day.ToString());
            todaycollection.Add(DateTime.Now.Date.Year.ToString());

            this.StartDate = todaycollection;
        }

        private ObservableCollection<object> _startdate;

        public ObservableCollection<object> StartDate

        {

            get
            {
                return _startdate;
            }
            set
            {
                _startdate = value;
                RaisePropertyChanged(() => StartDate);
            }

        }

    }
}
