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


            CurrentHtml = "<p>Tôi có nhà chính chủ, cho thuê tạiTầng 3 C5 Tập thể Nghĩa Tân. Nhà khép kín, diện tích 31.6m2, đã sửa, thuận tiệncho gia đình có con nhỏ, nhà tắm rộng rãi.Điện nước trả theo giá quy định của nhànước.Nhà gần Nhà trẻ Hoa Hồng, Trường tiểu học Nghĩa Tân, chợ Nghĩa Tân, cóbãi để oto gần nhà.Vào ở ngay từ 1 / 11 / 2018, cho thuê lâu dài. Hợp đồng ký 6tháng hoặc 1 năm.Thanh toán 3 tháng hoặc 6 tháng tùy theo hợp đồng.Giá thuê:4.200.000 đ / tháng.Liên hệ xem nhà: Chị Hồng0989134006/ Chị Hương 0904474795 </p><p style='text-align: center; '><img alt='' src='http://media.phongtot.vn/xc5tx4cj/ngoi-nha-46m&amp;sup2;-tuyet-dep-co-chi-phi-hoan-thien-480-trieu-dong-chang-trai-8x-xay-tang-vo_573edef00d8cd.jpg'></p>";
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


        private string _currentHtml;
        public string CurrentHtml
        {
            get
            {
                return _currentHtml;
            }
            set
            {
                _currentHtml = value;
                RaisePropertyChanged(() => CurrentHtml);

            }
        }

        private int _position;
        public int Position { get { return _position; } set { _position = value; RaisePropertyChanged(() => Position); } }
    }
}
