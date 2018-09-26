using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizlandApiService.Service
{
    public class GeocodeViewport
    {
        [JsonProperty("northeast")]
        public GeocodeNortheast northeast { get; set; }

        [JsonProperty("southwest")]
        public GeocodeSouthwest southwest { get; set; }
    }
}
