
using Android.App;
using Android.OS;

namespace Bizland.Droid.Helper
{
    public static class BizlandSetup
    {
        public static Activity Activity;

        public static void Initialize(Activity _Activity, Bundle bundle)
        {
            Activity = _Activity;
        }
    }
}