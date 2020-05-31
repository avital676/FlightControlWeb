using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace FlightControlWeb.Models
{
    public struct Info
    {
        public Info(int segmentNum, double secTillSegment)
        {
            SegmentNum = segmentNum;
            SecTillSegment = secTillSegment;
        }

        public int SegmentNum { get; }
        public double SecTillSegment { get; }
    }

    public class Flight
    {
        public FlightPlan flightPlan { get; set; }

        public Flight() { }

        public Flight(FlightPlan flightPlan)
        {
            this.flightPlan = flightPlan;
            Longitude = flightPlan.InitialLocation.Longitude;
            Latitude = flightPlan.InitialLocation.Latitude;
            Passengers = flightPlan.Passengers;
            DateTimee = flightPlan.InitialLocation.DateTime;
            CompanyName = flightPlan.CompanyName;
            IsExternal = false;
            FlightId = CreateID();
        }

        [JsonProperty(PropertyName = "flight_id")]
        public string FlightId { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        [JsonProperty(PropertyName = "is_external")]
        public bool IsExternal { get; set; }

        [JsonProperty(PropertyName = "date_time")]
        public string DateTimee { get; set; }

        public int Passengers { get; set; }

        [JsonProperty(PropertyName = "company_name")]
        public string CompanyName { get; set; }

        private String CreateID()
        {
            string id = "";
            Random random = new Random();
            string[] Alphabet = new string[26] { "A", "B", "C", "D", "E", "F", "G",
                "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R",
                "S", "T", "U", "V", "W", "X", "Y", "Z" };
            for (int i = 0; i < 3; i++)
            {
                int randomNum = random.Next(10, 99);
                int randomChar = random.Next(0, 25);
                id += randomNum.ToString() + Alphabet[randomChar];
            }
            return id;
        }

        public FlightPlan getFlightPlan()
        {
            return this.flightPlan;
        }

        public string UpdateLocation(DateTime relativeTo)
        {
            Location startingLoc = new Location(0, 0);
            Location latLonToMove;
            DateTime startingTime = DateTime.Parse(DateTimee);
            double secGap = (relativeTo - startingTime).TotalSeconds;
            Info currentSegment = getCurrentSegmentNum(relativeTo);
            int currentSegmentNum = currentSegment.SegmentNum;
            if (currentSegmentNum == -1)
            {
                return "Ended";
            }
            else if (currentSegmentNum == 0)
            {
                startingLoc = new Location(flightPlan.InitialLocation.Latitude, flightPlan.InitialLocation.Longitude);
                latLonToMove = flightPlan.Segments[currentSegmentNum].MovementForSec(startingLoc.Lat, startingLoc.Lon);
                //Latitude = startingLoc.Lat + secGap * latLonToMove.Lat;
                //Longitude = startingLoc.Lon + secGap * latLonToMove.Lon;
            }
            else
            {
                startingLoc = new Location(flightPlan.Segments[currentSegmentNum - 1].Latitude,
                    flightPlan.Segments[currentSegmentNum - 1].Longitude);
            }
            latLonToMove = flightPlan.Segments[currentSegmentNum].MovementForSec(startingLoc.Lat, startingLoc.Lon);
            Latitude = startingLoc.Lat + (secGap - currentSegment.SecTillSegment) * latLonToMove.Lat;
            Longitude = startingLoc.Lon + (secGap - currentSegment.SecTillSegment) * latLonToMove.Lon;
            return "Success";
        }

        private Info getCurrentSegmentNum(DateTime relativeTo)
        {
            double timePassed = 0;
            int segmentNum = 0;
            DateTime startingTime = DateTime.Parse(DateTimee);
            double secGap = (relativeTo - startingTime).TotalSeconds;
            for (segmentNum = 0; segmentNum < flightPlan.Segments.Count; segmentNum++)
            {
                timePassed += flightPlan.Segments[segmentNum].Timespanseconds;
                if (secGap < timePassed)
                {
                    timePassed -= flightPlan.Segments[segmentNum].Timespanseconds;
                    return new Info(segmentNum, timePassed);
                }
            }
            return new Info(-1, timePassed); ;
        }
    }
}
