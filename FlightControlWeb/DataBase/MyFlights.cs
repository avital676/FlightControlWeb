using FlightControlWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
            new Segment{  Longitude=40, Latitude = 40,  TimeSpanSec = 10 },
            new Segment{  Longitude=30, Latitude = 30,  TimeSpanSec = 10 },
            new Segment{  Longitude=20, Latitude = 20,  TimeSpanSec = 10 },
            new Segment{  Longitude=10, Latitude = 10,  TimeSpanSec = 10 },

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
            //getAllFlights("2020-12-26T23:56:41Z");
            return myFlights;
        }

        public IEnumerable<Flight> getAllFlights(string relativeTo)
        {
            
            DateTime clientDT = DateTime.Parse(relativeTo);
            DateTime serverDT = clientDT.AddHours(2);
            return GetRelevantFlights(clientDT);
        }

        public IEnumerable<Flight> GetRelevantFlights(DateTime relativeTo)
        {
            List<Flight> relevantFlights = new List<Flight>();
            for (int i = 0; i < myFlights.Count; i++)
            {
                Flight flight = myFlights[i];
                if (DateTime.Compare(relativeTo, DateTime.Parse(flight.DateTimee)) < 0)
                {
                    //Flight didnt start yet
                    continue;
                }
                else if (String.Compare(flight.UpdateLocation(relativeTo), "Ended") == 0)
                {
                    myFlights.Remove(flight);
                }
                else
                {
                    relevantFlights.Add(flight);
                }
            }
            return relevantFlights;
        }

        public void DeleteFlight(string id)
        {
            Flight flight = myFlights.Where(x => x.FlightId == id).FirstOrDefault();
            if (flight == null)
                throw new Exception("Flight not found");
            myFlights.Remove(flight);
        }


        public Flight GetFlightById(string id)
        {
            Flight flight = getAllFlights().Where(x => x.FlightId == id).FirstOrDefault();
            if (flight == null)
                throw new Exception("Flight not found");
            return flight;
        }
        private int i = 0;
        public void AddRandomFlights()
        {
            if (i == 0)
            {
                myFlights.Clear();
                Flight flight1 = new Flight(new FlightPlan { Passengers = 220, CompanyName = "ELAL", Latitude = 50, Longitude = 50, DateTime = "2020-12-26T23:56:21Z", Segments = allSegments });
                myFlights.Add(flight1);
                Flight flight2 = new Flight(new FlightPlan { Passengers = 220, CompanyName = "AirIndia", Latitude = 20.593683, Longitude = 78.962883, DateTime = "2020-12-26T23:56:21Z", Segments = allSegments });
                myFlights.Add(flight2);
                Flight flight3 = new Flight(new FlightPlan { Passengers = 220, CompanyName = "Lufthansa", Latitude = 70, Longitude = 20, DateTime = "2020-12-26T23:56:21Z", Segments = allSegments });
                myFlights.Add(flight3);
                Flight flight4 = new Flight(new FlightPlan { Passengers = 220, CompanyName = "Ethiopian", Latitude = 50, Longitude = 60, DateTime = "2020-12-26T23:56:21Z", Segments = allSegments });
                myFlights.Add(flight4);
                Flight flight5 = new Flight(new FlightPlan { Passengers = 220, CompanyName = "Ethiopian", Latitude = 50, Longitude = 60, DateTime = "2020-12-26T23:56:21Z", Segments = allSegments });
                myFlights.Add(flight4);
                Flight flight6 = new Flight(new FlightPlan { Passengers = 220, CompanyName = "Ethiopian", Latitude = 50, Longitude = 60, DateTime = "2020-12-26T23:56:21Z", Segments = allSegments });
                myFlights.Add(flight4);
                Flight flight7 = new Flight(new FlightPlan { Passengers = 220, CompanyName = "Ethiopian", Latitude = 50, Longitude = 60, DateTime = "2020-12-26T23:56:21Z", Segments = allSegments });
                myFlights.Add(flight4);
                Flight flight8 = new Flight(new FlightPlan { Passengers = 220, CompanyName = "Ethiopian", Latitude = 50, Longitude = 60, DateTime = "2020-12-26T23:56:21Z", Segments = allSegments });
                myFlights.Add(flight4);
                i++;
            }
        }
    }
}
