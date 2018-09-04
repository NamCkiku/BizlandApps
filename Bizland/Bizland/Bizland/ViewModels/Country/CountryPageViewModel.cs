using Bizland.Core;
using Bizland.Helpers;
using Bizland.Model;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace Bizland.ViewModels
{
    public class CountryPageViewModel : ViewModelBase
    {
        private readonly IPageDialogService _dialogService;
        public CountryPageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            Title = "Trang chủ";
            _dialogService = dialogService;
            GetAllCountry.Execute(null);
        }


        private ObservableCollection<Country> _countryCollection;
        public ObservableCollection<Country> CountryCollection
        {
            get
            {

                return _countryCollection;
            }
            set
            {
                _countryCollection = value;
                RaisePropertyChanged(() => CountryCollection);
            }
        }
        private ObservableCollection<Grouping<string, Country>> _countryCollectionSearched;


        public ObservableCollection<Grouping<string, Country>> CountryCollectionSearched
        {
            get
            {

                return _countryCollectionSearched;
            }
            set
            {
                _countryCollectionSearched = value;
                RaisePropertyChanged(() => CountryCollectionSearched);
            }
        }

        public Command GetAllCountry
        {
            get
            {
                return new Command(() =>
                {
                    try
                    {
                        if (CountryCollectionSearched == null
                           || CountryCollection == null
                           || CountryCollectionSearched.Count == 0
                           || CountryCollection.Count == 0)
                        {
                            var lst = Country.All;
                            if (lst != null && lst.Count > 0)
                            {
                                var groupedData =
                                   lst.OrderBy(p => p.Name)
                                       .GroupBy(p => p.NameSort)
                                       .Select(p => new Grouping<string, Country>(p))
                                       .ToList();

                                CountryCollection = new ObservableCollection<Country>(lst);
                                CountryCollectionSearched = new ObservableCollection<Grouping<string, Country>>(groupedData);
                            }
                        }
                        else
                        {
                            var groupedData =
                                  CountryCollection.OrderBy(p => p.Name)
                                      .GroupBy(p => p.NameSort)
                                      .Select(p => new Grouping<string, Country>(p))
                                      .ToList();
                            CountryCollectionSearched = new ObservableCollection<Grouping<string, Country>>(groupedData);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.WriteError("GetSummaryPartnerTripByDriverID", ex.Message);
                    }
                });
            }
        }


        public Command SearchCountryCommmand
        {
            get
            {
                return new Command<string>((arg) =>
                {
                    if (arg != null)
                    {
                        try
                        {
                            if (CountryCollection.Count > 0)
                            {
                                var lst = new List<Country>();

                                foreach (var item in CountryCollection)
                                {
                                    string itemName = item.Name.Trim().ToUpper();
                                    string searchText = arg.Trim().ToUpper();

                                    if (itemName.Contains(searchText))
                                    {
                                        lst.Add(item);
                                    }
                                }
                                var groupedData =
                                        lst.OrderBy(p => p.Name)
                                            .GroupBy(p => p.NameSort)
                                            .Select(p => new Grouping<string, Country>(p))
                                            .ToList();

                                CountryCollectionSearched = new ObservableCollection<Grouping<string, Country>>(groupedData);
                            }
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
