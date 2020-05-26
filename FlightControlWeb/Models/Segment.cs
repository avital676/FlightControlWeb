using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FlightControlWeb.Models
{
    public struct Location
    {
        public Location(double lat, double lon)
        {
            Lat = lat;
            Lon = lon;
        }

        public double Lat { get; }
        public double Lon { get; }

        public override string ToString() => $"({Lat}, {Lon})";
    }
    public class Segment
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        [JsonPropertyName("timespan_seconds")]

        public double Timespanseconds { get; set; }

        public int SegmentPartsNum { get; set; }

        public Location MovementForSec(double startLat, double startLon)
        {
            double latDistanceForSec = (Latitude - startLat) / Timespanseconds;
            double lonDistanceForSec = (Longitude - startLon) / Timespanseconds;
            Location loc = new Location(latDistanceForSec, lonDistanceForSec);
            return loc;
        }
    }
}