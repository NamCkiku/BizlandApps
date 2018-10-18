using Bizland.Core;
using Bizland.Core.Helpers;
using Bizland.Events;
using Bizland.Model;
using Prism.Events;
using Prism.Navigation;
using System;
using System.Reflection;
using Xamarin.Forms;

namespace Bizland.ViewModels
{
    public class CreateRoomPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;
        public CreateRoomPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator)
             : base(navigationService)
        {
            this._navigationService = navigationService;
            this._eventAggregator = eventAggregator;
            Title = "Đăng phòng";

            eventAggregator.GetEvent<SelectProvinceEvent>().Subscribe(UpdateProvince);
            eventAggregator.GetEvent<SelectDistrictEvent>().Subscribe(UpdateDistrict);
        }
        private Province _province;
        public void UpdateProvince(Province param)
        {
            _province = param;
        }
        public void UpdateDistrict(District param)
        {

        }
        public Command PushToProvincePage
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
                        await _navigationService.NavigateAsync("BaseNavigationPage/ProvincePage", null, useModalNavigation: true);
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


        public Command PushToDistrictPage
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
                        NavigationParameters navigationParameters = new NavigationParameters();
                        navigationParameters.Add("provinceID", _province.Id);
                        await _navigationService.NavigateAsync("BaseNavigationPage/ProvincePage", navigationParameters, useModalNavigation: true);
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

        public Command UploadImageRoom
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
                        string localPath = await Core.Helpers.UploadImageHelper.UploadImage(ImageType.Room);
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
