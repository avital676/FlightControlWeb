using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FlightControlWeb.Models
{
    public class FlightPlan
    {
        public int Passengers { get; set; }
        [JsonPropertyName("company_name")]
        [JsonProperty(PropertyName = "company_name")]
        public string CompanyName { get; set; }
        [JsonPropertyName("initial_location")]
        [JsonProperty(PropertyName = "initial_location")]
        public InitialLocation InitialLocation { get; set; }
        public List<Segment> Segments { get; set; }
    }
}