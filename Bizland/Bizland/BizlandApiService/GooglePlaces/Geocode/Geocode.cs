using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizlandApiService.Service
{
    public class Geocode
    {
        [JsonProperty("results")]
        public List<GeocodeResult> results { get; set; }

        [JsonProperty("status")]
        public string status { get; set; }
    }
}
