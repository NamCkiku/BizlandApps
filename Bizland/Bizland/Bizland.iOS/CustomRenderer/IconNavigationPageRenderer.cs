//using Bizland.iOS.CustomRenderer;
//using Bizland.Views;
//using CoreGraphics;
//using UIKit;
//using Xamarin.Forms;
//using Xamarin.Forms.Platform.iOS;

//[assembly: ExportRenderer(typeof(RootPage), typeof(IconNavigationPageRenderer))]
//namespace Bizland.iOS.CustomRenderer
//{
//    public class IconNavigationPageRenderer : PhoneMasterDetailRenderer
//    {
//        public override void ViewDidLayoutSubviews()
//        {
//            base.ViewDidLayoutSubviews();
//            if (!(Element is RootPage mdp)) return;
//            if (!(Platform.GetRenderer(mdp.Detail) is UINavigationController nc)) return;

//            UIButton btn = new UIButton(UIButtonType.Custom);
//            btn.Frame = new CGRect(0, 0, 50, 44);
//            var img = UIImage.FromFile("hamburger.png");
//            btn.SetTitle(string.Empty, UIControlState.Normal);
//            btn.SetImage(img, UIControlState.Normal);
//            btn.TouchUpInside += (sender, e) => mdp.IsPresented = true;
//            nc.NavigationBar.TitleTextAttributes = new UIStringAttributes()
//            {
//                ForegroundColor = UIColor.White
//            };

//            nc.NavigationBar.BarTintColor = Color.FromHex("#037cb3").ToUIColor();

//            var lbbi = new UIBarButtonItem(btn);
//            nc.NavigationBar.TopItem.LeftBarButtonItem = lbbi;
//        }
//    }
//}