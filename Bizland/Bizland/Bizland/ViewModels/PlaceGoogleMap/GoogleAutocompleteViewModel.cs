using Bizland.Core;
using BizlandApiService.Service;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using Xamarin.Forms;

namespace Bizland.ViewModels
{
    public class GoogleAutocompleteViewModel : ViewModelBase
    {

        public GoogleAutocompleteViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Google";

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
                        if (!string.IsNullOrEmpty(arg))
                        {
                            var lst = new List<Prediction>();
                            PlacesAutocomplete autocompleteObject = new PlacesAutocomplete();
                            Predictions predictions = await autocompleteObject.GetAutocomplete(arg.Trim());
                            if (predictions != null && predictions.predictions != null && predictions.predictions.Count > 0)
                            {
                                foreach (var prediction in predictions.predictions)
                                {
                                    lst.Add(prediction);
                                }
                            }
                            LstPlace = new ObservableCollection<Prediction>(lst);
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
                        if (!string.IsNullOrEmpty(arg))
                        {
                            var lst = new List<Prediction>();
                            PlacesAutocomplete autocompleteObject = new PlacesAutocomplete();
                            Predictions predictions = await autocompleteObject.GetAutocomplete(arg.Trim());
                            if (predictions != null && predictions.predictions != null && predictions.predictions.Count > 0)
                            {
                                foreach (var prediction in predictions.predictions)
                                {
                                    lst.Add(prediction);
                                }
                            }
                            LstPlace = new ObservableCollection<Prediction>(lst);
                        }

                    }
                    catch (Exception ex)
                    {
                        Logger.WriteError(MethodInfo.GetCurrentMethod().Name, ex);
                    }
                });
            }
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
                            //MessagingCenter.Send<GoogleAutocompleteViewModel, Prediction>(this, key, item);
                            await NavigationService.GoBackAsync(useModalNavigation: true);
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
