using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FlightControlWeb.Models
{
    public class InitialLocation
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        [JsonProperty(propertyName: "Date_Time")]

        public string DateTime { get; set; }
    }
}
