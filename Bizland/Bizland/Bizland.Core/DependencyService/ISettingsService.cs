﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Bizland.Core.DependencyService
{
    /// <summary>
    /// Service dùng cho Settings
    /// https://stackoverflow.com/questions/43608067/how-to-open-setting-from-our-application-in-xamarin-forms
    /// </summary>
    /// <Modified>
    /// Name     Date         Comments
    /// TrungTQ  12/4/2017   created
    /// </Modified>
    public interface ISettingsService
    {
        /// <summary>
        /// Mở form bật 
        /// </summary>
        /// <Modified>
        /// Name     Date         Comments
        /// TrungTQ  12/4/2017   created
        /// </Modified>
        bool OpenLocationSettings();

        bool OpenWifiSettings();
    }
}