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
            new Segment{  Longitude=45, Latitude = 45,  Timespan_seconds = 10 },
            new Segment{  Longitude=30, Latitude = 30,  Timespan_seconds = 10 },
            new Segment{  Longitude=20, Latitude = 20,  Timespan_seconds = 10 },
            new Segment{  Longitude=10, Latitude = 10,  Timespan_seconds = 10 },

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

        public static List<Segment> indiaSeg = new List<Segment>()
        {
            new Segment{  Longitude=13.948911, Latitude = 41.749457,  Timespan_seconds = 25 },
            new Segment{  Longitude=78.962883, Latitude = 20.593683,  Timespan_seconds = 25 }
        };

        public static List<Segment> nySeg = new List<Segment>()
        {
            new Segment{  Longitude=-80.289, Latitude = 25.6968,  Timespan_seconds = 25 },
            new Segment{  Longitude=-58.455, Latitude = -34.75,  Timespan_seconds = 25 }
        };

        public static List<Segment> londonSeg = new List<Segment>()
        {
            new Segment{  Longitude=4.895, Latitude = 52.37,  Timespan_seconds = 25 },
            new Segment{  Longitude=-102.55, Latitude = 23.634,  Timespan_seconds = 25 }
        };

        private int i = 0;
        public void AddRandomFlights()
        {
            if (i == 0)
            {
                myFlights.Clear();
                InitialLocation loc1 = new InitialLocation { Latitude = 40.7611, Longitude = -73.946668, Date_Time = "2020-12-26T23:56:03Z" };
                Flight flight1 = new Flight(new FlightPlan { Passengers = 420, Company_Name = "New York Airlines", Initial_Location = loc1, Segments = nySeg });
                myFlights.Add(flight1);

                InitialLocation loc2 = new InitialLocation { Latitude = 51.507, Longitude = -0.127, Date_Time = "2020-12-26T23:56:03Z" };
                Flight flight2 = new Flight(new FlightPlan { Passengers = 420, Company_Name = "British Airways", Initial_Location = loc2, Segments = londonSeg });
                myFlights.Add(flight2);

                InitialLocation loc5 = new InitialLocation { Latitude = 31.912154, Longitude = 35.114953, Date_Time = "2020-12-26T23:56:03Z" };
                Flight flight5 = new Flight(new FlightPlan { Passengers = 220, Company_Name = "Air India", Initial_Location = loc5, Segments = indiaSeg });
                myFlights.Add(flight5);

                i++;
            }
        }
    }
}
