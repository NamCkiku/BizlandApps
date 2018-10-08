using Prism.Navigation;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Bizland.ViewModels
{
    public class RoomDetailPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        public RoomDetailPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            this._navigationService = navigationService;

            var list = new List<string>();
            list.Add("http://media.phongtot.vn/xc5tx4cj/cho-thue-phong-moi-son-khu-van-phong-mat-tien-quan-tan-binh-thumb-resized-bf7mv.jpg");
            list.Add("http://media.phongtot.vn/xc5tx4cj/chung-cu-my-son-tower-thumb-2pvfc.jpg");
            list.Add("http://media.phongtot.vn/xc5tx4cj/chung-cu-my-son-tower-thumb-2pvfc.jpg");
            list.Add("http://media.phongtot.vn/xc5tx4cj/cho-thue-can-ho-o-620-lac-long-quan-thumb-resized-70pzw.jpg");
            ImageSlider = new ObservableCollection<string>(list);

        }
        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
           
        }

        public override void OnNavigatingTo(INavigationParameters parameters)
        {
        }
        private ObservableCollection<string> _ImageSlider;
        public ObservableCollection<string> ImageSlider
        {
            get { return _ImageSlider; }
            set
            {
                _ImageSlider = value;
                RaisePropertyChanged(() => ImageSlider);
            }
        }

        private int _position;
        public int Position { get { return _position; } set { _position = value; RaisePropertyChanged(() => Position); } }
    }
}
