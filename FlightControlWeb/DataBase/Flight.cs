using FlightControlWeb.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class Flight
    {
        /*public Flight(FlightPlan flightPlan)
        {
            this.FlightPlan = flightPlan;
            this.Longitude = FlightPlan.Longitude;
            this.Latitude = FlightPlan.Latitude;
            IsExternal = false;
            //createId();
        }*/
        public string FlightId { get; set; }
        public int Passengers { get; set; }
        public string CompanyName { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string DateTime { get; set; }
        public bool IsExternal { get; set; }
      //  public FlightPlan FlightPlan { get; set; }

        /**public void createId()
        {
            //this.FlightId - uniqueId
        }*/
    }
}
