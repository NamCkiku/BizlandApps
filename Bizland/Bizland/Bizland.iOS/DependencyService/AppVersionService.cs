using Bizland.Core;
using Bizland.iOS.DependencyService;
using Foundation;

[assembly: Xamarin.Forms.Dependency(typeof(AppVersionService))]
namespace Bizland.iOS.DependencyService
{
    public class AppVersionService : IAppVersionService
    {
        public string GetVersion()
        {
            return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString();
        }
        public string GetBuild()
        {
            return NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleVersion").ToString();
        }
    }
}