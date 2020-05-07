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
        
             new Flight{ FlightId="1234",Longitude= 98,Latitude = 70,Passengers= 90, CompanyName ="elal", DateTime="04.05.20",IsExternal = true },
             new Flight{ FlightId="5678",Longitude= 93,Latitude = 60,Passengers= 100, CompanyName ="arkia", DateTime="09.10.20",IsExternal = true },
             new Flight{ FlightId="9012",Longitude= 90,Latitude = 50,Passengers= 80, CompanyName ="Tailand", DateTime="18.09.20",IsExternal = true }
        };

        internal void addFlightPlan(FlightPlan value)
        {
            throw new NotImplementedException();
        }

        private static List<IdPlan> idPlanList = new List<IdPlan>() { };
        
        public IEnumerable<Flight> getAllFlights()
        {
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

        private int i = 0;
        private String createID()
        {
            return (++i).ToString();
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