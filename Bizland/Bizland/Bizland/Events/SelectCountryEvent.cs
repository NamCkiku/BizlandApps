﻿using Bizland.Model;
using Prism.Events;

namespace Bizland.Events
{
    public class SelectCountryEvent : PubSubEvent<Country>
    {
    }
}
