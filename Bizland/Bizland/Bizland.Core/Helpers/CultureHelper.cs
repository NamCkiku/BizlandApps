using Bizland.Core.Resources;
using Plugin.Multilingual;
using System.Globalization;

namespace Bizland.Core.Helpers
{
    public static class CultureHelper
    {

        public static void SetCulture()
        {
            CrossMultilingual.Current.CurrentCultureInfo = new CultureInfo("vi-VN");
            AppResource.Culture = CrossMultilingual.Current.CurrentCultureInfo;
            CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("vi-VN");
        }
    }
}
