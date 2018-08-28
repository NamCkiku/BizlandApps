using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BizlandApiService.Service
{
    /// <summary>
    /// The Autocomplete Prediction Term
    /// </summary>
    public class PredictionTerm
    {
        [JsonProperty("offset")]
        public int Offset { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
