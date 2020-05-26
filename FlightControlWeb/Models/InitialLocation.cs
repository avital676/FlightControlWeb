using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FlightControlWeb.Models
{
    public class InitialLocation
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        [JsonPropertyName("date_time")]
        [JsonProperty(PropertyName = "date_time")]
        public string DateTime { get; set; }
    }
}