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
            this.Longitude = flightPlan.Longitude;
            this.Latitude = flightPlan.Latitude;
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
            get { return flightPlan.DateTime; }
            set
            {
                flightPlan.DateTime = value;
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
            get { return flightPlan.CompanyName; }
            set
            {
                flightPlan.CompanyName = value;
            }
        }

        private String CreateID()
        {
            //return System.Guid.NewGuid().ToString().Substring(0, 6);
            StringBuilder _builder = new StringBuilder();
            Enumerable
                .Range(65, 26)
                .Select(e => ((char)e).ToString())
                .Concat(Enumerable.Range(97, 26).Select(e => ((char)e).ToString()))
                .Concat(Enumerable.Range(0, 10).Select(e => e.ToString()))
                .OrderBy(e => Guid.NewGuid())
                .Take(9)
                .ToList().ForEach(e => _builder.Append(e));
            return _builder.ToString();
        }
        public FlightPlan getFlightPlan()
        {
            return this.flightPlan;
        }
        public string UpdateLocation(DateTime relativeTo)
        {
            Location startingLoc = new Location(0,0);
            Location latLonToMove;
            DateTime startingTime = DateTime.Parse(DateTimee);
            double secGap = (relativeTo - startingTime).TotalSeconds;
            Info currentSegment = getCurrentSegmentNum(relativeTo);
            int currentSegmentNum = currentSegment.SegmentNum;
            if (currentSegmentNum == -1) {
                return "Ended";
            } else if(currentSegmentNum == 0) {
                startingLoc = new Location(flightPlan.Latitude, flightPlan.Longitude);
            } else {
                startingLoc = new Location(flightPlan.Segments[currentSegmentNum - 1].Latitude,
                    flightPlan.Segments[currentSegmentNum - 1].Longitude);
            }
            latLonToMove = flightPlan.Segments[currentSegmentNum].MovementForSec(startingLoc.Lat, startingLoc.Lon);
            Latitude = flightPlan.Segments[currentSegmentNum - 1].Latitude + (secGap - currentSegment.SecTillSegment) * latLonToMove.Lat;
            Longitude = flightPlan.Segments[currentSegmentNum - 1].Longitude + (secGap - currentSegment.SecTillSegment) * latLonToMove.Lon;
            return "Success";
        }

        private Info getCurrentSegmentNum(DateTime relativeTo)
        {
            double timePassed = 0;
            int segmentNum = 0;
            DateTime startingTime = DateTime.Parse(DateTimee);
            double secGap = (relativeTo - startingTime).TotalSeconds;
            for (segmentNum =0; segmentNum < flightPlan.Segments.Count; segmentNum++)
            {
                timePassed += flightPlan.Segments[segmentNum].TimeSpanSec;
                if (secGap < timePassed)
                {
                    return new Info(segmentNum, timePassed);
                }
            }
            return new Info(-1, timePassed); ;
        }
    }
}
