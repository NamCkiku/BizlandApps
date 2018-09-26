using Bizland.Core;
using Bizland.Events;
using Bizland.Model;
using BizlandApiService.Service;
using Prism.Events;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace Bizland.ViewModels
{
    public class GoogleAutocompleteViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IPlacesGeocode _placesGeocode;
        private readonly IPlacesAutocomplete _placesAutocomplete;
        public GoogleAutocompleteViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IPlacesGeocode placesGeocode, IPlacesAutocomplete placesAutocomplete)
            : base(navigationService)
        {
            Title = "Google";
            _eventAggregator = eventAggregator;
            _navigationService = navigationService;
            _placesGeocode = placesGeocode;
            _placesAutocomplete = placesAutocomplete;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            LstPlace = new ObservableCollection<Prediction>();
            SearchText = string.Empty;
        }

        private ObservableCollection<Prediction> lstPlace = new ObservableCollection<Prediction>();
        public ObservableCollection<Prediction> LstPlace
        {
            get { return lstPlace; }
            set
            {
                lstPlace = value;
                RaisePropertyChanged(() => LstPlace);
            }
        }
        private string searchText;
        public string SearchText
        {
            get
            {
                return searchText;
            }
            set
            {
                searchText = value;
                RaisePropertyChanged(() => SearchText);
            }
        }

        public Command SearchLoactionCommmand
        {
            get
            {
                return new Command<string>(async (arg) =>
                {
                    try
                    {
                        if (Device.RuntimePlatform == Device.Android)
                        {
                            if (!string.IsNullOrEmpty(arg))
                            {
                                Predictions predictions = await _placesAutocomplete.GetAutocomplete(arg.Trim());
                                if (predictions != null && predictions.predictions != null && predictions.predictions.Count > 0)
                                {
                                    LstPlace = predictions.predictions.ToObservableCollection();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteError(MethodInfo.GetCurrentMethod().Name, ex);
                    }
                });
            }
        }
        public Command SearchLoactioniOSCommmand
        {
            get
            {
                return new Command<string>(async (arg) =>
                {
                    try
                    {
                        if (Device.RuntimePlatform == Device.iOS)
                        {
                            if (!string.IsNullOrEmpty(arg))
                            {
                                Predictions predictions = await _placesAutocomplete.GetAutocomplete(arg.Trim());
                                if (predictions != null && predictions.predictions != null && predictions.predictions.Count > 0)
                                {
                                    LstPlace = new ObservableCollection<Prediction>(predictions.predictions);
                                }

                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        Logger.WriteError(MethodInfo.GetCurrentMethod().Name, ex);
                    }
                });
            }
        }

        public async Task<Position> GetPositionsForAddress(string address)
        {
            var result = new Position();
            try
            {
                if (!string.IsNullOrEmpty(address))
                {

                    Geocode response = await _placesGeocode.GetGeocode(address);
                    if (response != null && response.results != null && response.results.Count > 0)
                    {
                        var location = response.results[0].geometry.location;
                        if (location != null)
                        {
                            result = new Position(location.lat, location.lng);
                        }
                    }
                    else
                    {
                        "Không tìm thấy địa chỉ của bạn".ToToast(ToastNotificationType.Info, null, 10);
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.WriteError(MethodInfo.GetCurrentMethod().Name, ex);
            }
            return result;
        }
        public Command PlaceSelectedCommand
        {
            get
            {
                return new Command<Prediction>(async (item) =>
                {
                    try
                    {
                        if (item != null)
                        {
                            var positon = await GetPositionsForAddress(item.Description);
                            if (positon.Latitude > 0 && positon.Longitude > 0)
                            {
                                var data = new SelectAddress
                                {
                                    Address = item.Description,
                                    Position = positon
                                };
                                _eventAggregator.GetEvent<SelectMapAddressEvent>().Publish(data);
                                await _navigationService.GoBackAsync(useModalNavigation: true);
                            }
                        }
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
