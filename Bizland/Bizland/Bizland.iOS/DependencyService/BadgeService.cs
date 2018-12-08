using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bizland.Core;
using Bizland.iOS.DependencyService;
using Foundation;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(BadgeService))]
namespace Bizland.iOS.DependencyService
{
    /// <summary>
    /// Implementation of Badge for iOS
    /// </summary>
    public class BadgeService : IBadge
    {
        /// <summary>
        /// Clears the badge.
        /// </summary>
        public void ClearBadge()
        {
            UIApplication.SharedApplication.ApplicationIconBadgeNumber = 0;
        }

        /// <summary>
        /// Sets the badge.
        /// </summary>
        /// <param name="badgeNumber">The badge number.</param>
        /// <param name="title">The title. Used only by Android</param>
        public void SetBadge(int badgeNumber, string title = null)
        {
            UIApplication.SharedApplication.ApplicationIconBadgeNumber = badgeNumber;
        }
    }
}