using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public double Timespan_seconds { get; set; }

        public int SegmentPartsNum { get; set; }

        public Location MovementForSec(double startLat, double startLon)
        {
            double latDistanceForSec = (Latitude - startLat) / Timespan_seconds;
            double lonDistanceForSec = (Longitude - startLon) / Timespan_seconds;
            Location loc = new Location(latDistanceForSec, lonDistanceForSec);
            return loc;
        }
    }
}


