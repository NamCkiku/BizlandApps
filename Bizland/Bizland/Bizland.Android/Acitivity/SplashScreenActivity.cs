
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Support.V7.App;

namespace Bizland.Droid
{
    [Activity(Label = "Bizland",
         Icon = "@mipmap/ic_launcher",
         MainLauncher = true,
         NoHistory = true,
         Theme = "@style/Theme.Splash",
         ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreenActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var intent = new Intent(this, typeof(MainActivity));

            if (Intent.Extras != null)
            {
                intent.PutExtras(Intent.Extras);
            }
            intent.SetFlags(ActivityFlags.SingleTop);

            StartActivity(intent);
            Finish();
        }
        protected override void OnResume()
        {
            base.OnResume();

        }
    }
}