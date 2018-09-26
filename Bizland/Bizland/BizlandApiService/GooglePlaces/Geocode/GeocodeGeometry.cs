using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizlandApiService.Service
{
    public class GeocodeGeometry
    {
        [JsonProperty("location")]
        public GeocodeLocation location { get; set; }

        [JsonProperty("location_type")]
        public string location_type { get; set; }

        [JsonProperty("viewport")]
        public GeocodeViewport viewport { get; set; }
    }
}
