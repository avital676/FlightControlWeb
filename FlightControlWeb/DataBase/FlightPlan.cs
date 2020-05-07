using FlightControlWeb.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class FlightPlan
    {
        public int Passengers { get; set; }
        public string CompanyName { get; set; }
        //intialize location
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string DateTime { get; set; }
        public List<Segment> Segments { get; set; }
    }
}
