using FlightControlWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.DataBase
{
    public sealed class MyFlights
    {
        private static MyFlights instance = null;
        private static readonly object padlock = new object();

        MyFlights() { }

        public static MyFlights Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new MyFlights();
                    }
                }
                return instance;
            }
        }

        private static List<Flight> myFlights = new List<Flight>()
        {

              new Flight {FlightId = "1", Passengers = 220, CompanyName = "ELAL", Latitude = -15.707663, Longitude = -49.427532, DateTime = "10", IsExternal = false },
                new Flight {FlightId = "2", Passengers = 240, CompanyName = "AirIndia", Latitude = 20.593683, Longitude = 78.962883, DateTime = "10", IsExternal = false},
                new Flight {FlightId = "3", Passengers = 220, CompanyName = "Lufthansa", Latitude = 50, Longitude = 60, DateTime = "10", IsExternal = false },
                new Flight {FlightId = "4", Passengers = 240, CompanyName = "ELAL", Latitude = 70, Longitude = 20, DateTime = "10", IsExternal = false}
        };

        internal void addFlightPlan(FlightPlan value)
        {
            throw new NotImplementedException();
        }

        private static List<IdPlan> idPlanList = new List<IdPlan>() { };
        
        public IEnumerable<Flight> getAllFlights()
        {
            createID();
            return myFlights;
        }

        public IEnumerable<IdPlan> getPlans()
        {
            return idPlanList;
        }

        public void addFlight(Flight f)
        {
            myFlights.Add(f);
        }

        public void AddFlightPlan(FlightPlan fp)
        {
            Flight f = PlanConverter(fp);
            myFlights.Add(f);
         //   idPlanList.Add(new IdPlan { flight_id = f.FlightId, plan = fp });
        }

        private Flight PlanConverter(FlightPlan fp)
        {
            Flight f = new Flight();
            f.FlightId = createID();
            f.Longitude = fp.Longitude;
            f.Latitude = fp.Latitude;
            f.Passengers = fp.Passengers;
            f.CompanyName = fp.CompanyName;
            f.DateTime = fp.DateTime;
            f.IsExternal = false;
            return f;
        }

        private String createID()
        {
            return System.Guid.NewGuid().ToString().Substring(0, 6);
        }

        public void DeleteFlight(string id)
        {
            Flight flight = MyFlights.Instance.getAllFlights().Where(x => x.FlightId == id).FirstOrDefault();
            if (flight == null)
                throw new Exception("Flight not found");
            MyFlights.Instance.getAllFlights().ToList().Remove(flight);        }


        public Flight GetFlightById(string id)
        {
            Flight flight = getAllFlights().Where(x => x.FlightId == id).FirstOrDefault();
            if (flight == null)
                throw new Exception("Flight not found");
            return flight;
        }


    }
}