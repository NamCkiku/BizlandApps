using Bizland.ApiService;
using Bizland.Core;
using Bizland.Events;
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
    public class DistrictPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IProvinceService _iProvinceService;
        public DistrictPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator, IProvinceService iProvinceService)
             : base(navigationService)
        {
            Title = "Chọn tỉnh thành";
            _eventAggregator = eventAggregator;
            _navigationService = navigationService;
            this._iProvinceService = iProvinceService;
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            _eventAggregator.GetEvent<SelectRoomTypeEvent>().Unsubscribe(null);

            _eventAggregator.GetEvent<SelectMapAddressEvent>().Unsubscribe(null);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            if (parameters.ContainsKey("provinceID"))
            {
                var provinceID = (int)parameters["provinceID"];
                GetAllDistrict.Execute(provinceID);
            }
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {

        }

        static List<District> _listDistrictModel { get; set; }
        public static List<District> ListDistrict
        {
            get
            {
                if (_listDistrictModel != null)
                {
                    return _listDistrictModel;
                }
                else
                {
                    return new List<District>();
                }
            }
        }
        private ObservableCollection<District> districtCollection;
        public ObservableCollection<District> DistrictCollection
        {
            get
            {

                return districtCollection;
            }
            set
            {
                districtCollection = value;
                RaisePropertyChanged(() => DistrictCollection);
            }
        }


        public Command GetAllDistrict
        {
            get
            {
                return new Command<int>(async (provinceID) =>
                {
                    try
                    {
                        if (provinceID > 0)
                        {
                            if (_listDistrictModel != null && _listDistrictModel.Count > 0)
                            {
                                DistrictCollection = new ObservableCollection<District>(_listDistrictModel.Where(x => x.ProvinceId == provinceID));
                            }
                            else
                            {
                                if (DistrictCollection == null
                                || DistrictCollection.Count == 0)
                                {
                                    var lst = await _iProvinceService.GetAllDistrict();
                                    if (lst != null && lst.Count > 0)
                                    {
                                        _listDistrictModel = lst;

                                        DistrictCollection = new ObservableCollection<District>(lst.Where(x => x.ProvinceId == provinceID));
                                    }
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


        public Command SelectedCommand
        {
            get
            {
                return new Command<District>(async (item) =>
                {
                    if (item != null)
                    {
                        try
                        {
                            _eventAggregator.GetEvent<SelectDistrictEvent>().Publish(item);
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
