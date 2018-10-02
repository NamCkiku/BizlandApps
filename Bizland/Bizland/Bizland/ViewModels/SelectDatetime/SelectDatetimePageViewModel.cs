using Bizland.Core;
using Bizland.Events;
using Prism.Events;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using Xamarin.Forms;

namespace Bizland.ViewModels
{
    public class SelectDatetimePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;
        public SelectDatetimePageViewModel(INavigationService navigationService, IEventAggregator eventAggregator)
            : base(navigationService)
        {
            this._navigationService = navigationService;
            this._eventAggregator = eventAggregator;

            ObservableCollection<object> todaycollection = new ObservableCollection<object>();

            //Select today dates
            todaycollection.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Date.Month));
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


        public Command ClosePagePopupCommand
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
                        await _navigationService.GoBackAsync();
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

        public Command IgreeSelectedDateCommand
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
                        if (StartDate != null)
                        {
                            int month = DateTime.ParseExact(StartDate[0].ToString(), "MMMM", CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("vi-VN")).Month;
                            var day = StartDate[1];
                            var year = StartDate[2];
                            var date = day + "/" + month + "/" + year;
                            DateTime result = DateTime.Parse(date);
                            _eventAggregator.GetEvent<SelectDateTimeEvent>().Publish(result);
                            await _navigationService.GoBackAsync();
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
