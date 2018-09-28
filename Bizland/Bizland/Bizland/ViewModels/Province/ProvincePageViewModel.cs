using Bizland.ApiService;
using Bizland.Core;
using Bizland.Events;
using Bizland.Helpers;
using Bizland.Model;
using Prism.Events;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace Bizland.ViewModels
{
    public class ProvincePageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IProvinceService _iProvinceService;
        public ProvincePageViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IProvinceService iProvinceService)
             : base(navigationService)
        {
            Title = "Chọn tỉnh thành";
            _eventAggregator = eventAggregator;
            _navigationService = navigationService;
            this._iProvinceService = iProvinceService;
            GetAllProvince.Execute(null);
        }
        static List<Province> _listProvinceModel { get; set; }
        public static List<Province> ListProvince
        {
            get
            {
                if (_listProvinceModel != null)
                {
                    return _listProvinceModel;
                }
                else
                {
                    return new List<Province>();
                }
            }
        }
        private ObservableCollection<Province> provinceCollection;
        public ObservableCollection<Province> ProvinceCollection
        {
            get
            {

                return provinceCollection;
            }
            set
            {
                provinceCollection = value;
                RaisePropertyChanged(() => ProvinceCollection);
            }
        }
        private ObservableCollection<Grouping<string, Province>> provinceCollectionSearched;
        public ObservableCollection<Grouping<string, Province>> ProvinceCollectionSearched
        {
            get
            {

                return provinceCollectionSearched;
            }
            set
            {
                provinceCollectionSearched = value;
                RaisePropertyChanged(() => ProvinceCollectionSearched);
            }
        }

        public Command GetAllProvince
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        if (_listProvinceModel != null && _listProvinceModel.Count > 0)
                        {
                            ProvinceCollection = new ObservableCollection<Province>(_listProvinceModel);

                            var groupedData =
                                   _listProvinceModel.OrderBy(p => p.Name)
                                       .GroupBy(p => p.NameSort)
                                       .Select(p => new Grouping<string, Province>(p))
                                       .ToList();
                            ProvinceCollectionSearched = new ObservableCollection<Grouping<string, Province>>(groupedData);
                        }
                        else
                        {
                            if (ProvinceCollectionSearched == null
                            || ProvinceCollection == null
                            || ProvinceCollectionSearched.Count == 0
                            || ProvinceCollection.Count == 0)
                            {
                                var lst = await _iProvinceService.GetProvince();
                                if (lst != null && lst.Count > 0)
                                {
                                    _listProvinceModel = lst;
                                    var groupedData =
                                       lst.OrderBy(p => p.Name)
                                           .GroupBy(p => p.NameSort)
                                           .Select(p => new Grouping<string, Province>(p))
                                           .ToList();

                                    ProvinceCollection = new ObservableCollection<Province>(lst);
                                    ProvinceCollectionSearched = new ObservableCollection<Grouping<string, Province>>(groupedData);
                                }
                            }
                            else
                            {
                                var groupedData =
                                      ProvinceCollection.OrderBy(p => p.Name)
                                          .GroupBy(p => p.NameSort)
                                          .Select(p => new Grouping<string, Province>(p))
                                          .ToList();
                                ProvinceCollectionSearched = new ObservableCollection<Grouping<string, Province>>(groupedData);
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


        public Command SearchProvinceCommmand
        {
            get
            {
                return new Command<string>((arg) =>
                {
                    if (arg != null)
                    {
                        try
                        {
                            if (ProvinceCollection.Count > 0)
                            {
                                var lst = new List<Province>();

                                foreach (var item in ProvinceCollection)
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
                                            .Select(p => new Grouping<string, Province>(p))
                                            .ToList();

                                ProvinceCollectionSearched = new ObservableCollection<Grouping<string, Province>>(groupedData);
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



        public Command SelectedCommand
        {
            get
            {
                return new Command<Province>(async (item) =>
                {
                    if (item != null)
                    {
                        try
                        {
                            _eventAggregator.GetEvent<SelectProvinceEvent>().Publish(item);
                            await _navigationService.GoBackAsync(useModalNavigation: true);
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
