using Android.Content.PM;
using Bizland.Core;
using Bizland.Droid.DependencyService;

[assembly: Xamarin.Forms.Dependency(typeof(AppVersionService))]
namespace Bizland.Droid.DependencyService
{
    public class AppVersionService : IAppVersionService
    {
        public string GetVersion()
        {
            var context = global::Android.App.Application.Context;

            PackageManager manager = context.PackageManager;
            PackageInfo info = manager.GetPackageInfo(context.PackageName, 0);

            return info.VersionName;
        }

        public string GetBuild()
        {
            var context = global::Android.App.Application.Context;
            PackageManager manager = context.PackageManager;
            PackageInfo info = manager.GetPackageInfo(context.PackageName, 0);

            return info.VersionCode.ToString();
        }
    }
}