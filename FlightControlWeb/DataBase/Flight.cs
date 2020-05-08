using FlightControlWeb.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;


namespace FlightControlWeb.Models
{
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
            this.DateTimee = DateTime.Now.ToString();
        }
        public string FlightId { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string DateTimee { get; set; }
        public bool IsExternal { get; set; }
        //public FlightPlan flightPlan { get; set; }


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
    }

}
