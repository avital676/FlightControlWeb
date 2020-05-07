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

        private static List<Flight> myFlights = new List<Flight>();
        public static List<Segment> allSegments = new List<Segment>()
        {
            new Segment{  longitude=90.0911, latitude = -70.0911,  timespan_seconds =0 },
            new Segment{  longitude=80.0911, latitude = -65.0911,  timespan_seconds =0 },
            new Segment{  longitude=70.0911, latitude = -60.0911,  timespan_seconds =0 },
            new Segment{  longitude=60.0911, latitude = -55.0911,  timespan_seconds =0 },

        };
        public void addFlight(FlightPlan flightPlan)
        {
            Flight flight = new Flight(flightPlan);
            myFlights.Add(flight);
        }

        public void addFlight(Flight flight)
        {
            myFlights.Add(flight);   
        }

        public IEnumerable<Flight> getAllFlights()
        {
            return myFlights;
        }

        /**public IEnumerable<string> getPlansId()
        {
            return idPlanList;
        }*/

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
        public void AddRandomFlights()
        {
            Flight flight1 = new Flight(new FlightPlan { Passengers = 220, CompanyName = "ELAL", Latitude = -15.707663, Longitude = -49.427532, DateTime = "10", Segments = allSegments });
            myFlights.Add(flight1);
            Flight flight2 = new Flight(new FlightPlan { Passengers = 220, CompanyName = "AirIndia", Latitude = 20.593683, Longitude = 78.962883, DateTime = "5/7/2020" });
            myFlights.Add(flight2);
            Flight flight3 = new Flight(new FlightPlan { Passengers = 220, CompanyName = "Lufthansa", Latitude = 70, Longitude = 20, DateTime = "5/7/2020" });
            myFlights.Add(flight3);
            Flight flight4 = new Flight(new FlightPlan { Passengers = 220, CompanyName = "Ethiopian", Latitude = 50, Longitude = 60, DateTime = "5/7/2020" });
            myFlights.Add(flight4);
        }
    }
}