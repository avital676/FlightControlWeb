using FlightControlWeb.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;


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

        public Flight(FlightPlan flightPlan)
        {
            this.flightPlan = flightPlan;
            this.Longitude = flightPlan.Initial_Location.Longitude;
            this.Latitude = flightPlan.Initial_Location.Latitude;
            IsExternal = false;
            this.FlightId = CreateID();
        }
        public string FlightId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public bool IsExternal { get; set; }
        //public FlightPlan flightPlan { get; set; }

        public string DateTimee
        {
            get { return flightPlan.Initial_Location.Date_Time; }
            set
            {
                flightPlan.Initial_Location.Date_Time = value;
            }
        }
        public int Passengers
        {
            get { return flightPlan.Passengers; }
            set
            {
                flightPlan.Passengers = value;
            }
        }
        public string CompanyName
        {
            get { return flightPlan.Company_Name; }
            set
            {
                flightPlan.Company_Name = value;
            }
        }

        private String CreateID()
        {
            string id = "";
            Random random = new Random();
            string[] Alphabet = new string[26] { "A", "B", "C", "D", "E", "F", "G",
                "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R",
                "S", "T", "U", "V", "W", "X", "Y", "Z" };
            for (int i = 0; i < 3; i++)
            {
                int randomNum = random.Next(0, 99);
                int randomChar = random.Next(0, 25);
                id += randomNum.ToString() + Alphabet[randomChar];
            }
            return id;
            /**StringBuilder _builder = new StringBuilder();
            Enumerable
                .Range(65, 26)
                .Select(e => ((char)e).ToString())
                .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
                .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
                .OrderBy(e => Guid.NewGuid())
                .Take(9)
                .ToList().ForEach(e => _builder.Append(e));
            return _builder.ToString();*/
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
                startingLoc = new Location(flightPlan.Initial_Location.Latitude, flightPlan.Initial_Location.Longitude);
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
                timePassed += flightPlan.Segments[segmentNum].Timespan_seconds;
                if (secGap < timePassed)
                {
                    timePassed -= flightPlan.Segments[segmentNum].Timespan_seconds;
                    return new Info(segmentNum, timePassed);
                }
            }
            return new Info(-1, timePassed); ;
        }
    }
}
