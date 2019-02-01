namespace Bizland.Core
{
    /// <summary>
    /// https://forums.xamarin.com/discussion/104922/how-to-set-get-the-app-version-number-and-build-for-cross-platform
    /// </summary>
    /// <Modified>
    /// Name     Date         Comments
    /// TrungTQ  12/01/2018   created
    /// </Modified>
    public interface IAppVersionService
    {
        string GetVersion();

        string GetBuild();
    }
}
