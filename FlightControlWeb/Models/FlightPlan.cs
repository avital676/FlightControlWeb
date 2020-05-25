using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FlightControlWeb.Models
{
    public class FlightPlan
    {
        public int Passengers { get; set; }
        [JsonProperty(propertyName: "Company_Name")]
        public string CompanyName { get; set; }
        [JsonProperty(propertyName: "Initial_Location")]
        public InitialLocation InitialLocation { get; set;}
        public List<Segment> Segments { get; set; }
    }
}
