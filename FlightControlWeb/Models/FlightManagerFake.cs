using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class FlightManagerFake : IFlightManager
    {
        private ConcurrentDictionary<string, Flight> myFlights = new ConcurrentDictionary<string, Flight>();
        Servers servers = new Servers();
        public void addFlight(FlightPlan flightPlan) {
          
        }

        public void addFlight(Flight flight) {
            Flight f = new Flight();
            myFlights.TryAdd("FakeNullFlight", f);
        }

        public IEnumerable<Flight> getAllFlights() {
            return null;
        }

        public IEnumerable<Flight> getAllFlights(string relativeTo) {
            List<Flight> relevantFlights = new List<Flight>();
            return relevantFlights;
        }

        public IEnumerable<Flight> getAllFlightsSync(string relativeTo) {
            throw new System.ArgumentException("faakkee", "fake");
        }

        public IEnumerable<Flight> GetRelevantFlights(DateTime relativeTo) {
            throw new IndexOutOfRangeException();
        }

        public void DeleteFlight(string id) {
            myFlights.Clear();
        }

        public Flight GetFlightById(string id) {
            return null;
        }


        public static List<Segment> indiaSeg = new List<Segment>()
        {
            new Segment{  Longitude=13.948911, Latitude = 41.749457,  Timespanseconds = 10 },
            new Segment{   Longitude=13.948911, Latitude = 41.749457,  Timespanseconds = 10 }
        };

        public static List<Segment> nySeg = new List<Segment>()
        {
            new Segment{  Longitude=-80.289, Latitude = 25.6968,  Timespanseconds = 0 },
            new Segment{  Longitude=-58.455, Latitude = -34.75,  Timespanseconds = 0 }
        };

        public static List<Segment> londonSeg = new List<Segment>()
        {
        };

        private int i = 0;
        public void AddRandomFlights()
        {
            if (i == 0)
            {
                myFlights.Clear();
                InitialLocation loc1 = new InitialLocation { Latitude = 40.7611, Longitude = -73.946668, DateTime = "2020-12-26T23:56:03Z" };
                Flight flight1 = new Flight(new FlightPlan { Passengers = 420, CompanyName = "New York Airlines", InitialLocation = loc1, Segments = nySeg });
                myFlights.TryAdd(flight1.FlightId, flight1);

                InitialLocation loc2 = new InitialLocation { Latitude = 51.507, Longitude = -0.127, DateTime = "2020-12-26T23:56:03Z" };
                Flight flight2 = new Flight(new FlightPlan { Passengers = 420, CompanyName = "British Airways", InitialLocation = loc2, Segments = londonSeg });
                myFlights.TryAdd(flight2.FlightId, flight2);

                InitialLocation loc5 = new InitialLocation { Latitude = 31.912154, Longitude = 35.114953, DateTime = "2020-12-26T23:56:03Z" };
                Flight flight5 = new Flight(new FlightPlan { Passengers = 200, CompanyName = "Air India", InitialLocation = loc5, Segments = indiaSeg });
                myFlights.TryAdd(flight5.FlightId, flight5);

                i++;
            }
        }

        void IFlightManager.addFlight(FlightPlan flightPlan)
        {
            throw new NotImplementedException();
        }

        void IFlightManager.addFlight(Flight flight)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Flight> IFlightManager.getAllFlights()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Flight> IFlightManager.getAllFlights(string relativeTo)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Flight> IFlightManager.getAllFlightsSync(string relativeTo)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Flight> IFlightManager.GetRelevantFlights(DateTime relativeTo)
        {
            throw new NotImplementedException();
        }

        void IFlightManager.DeleteFlight(string id)
        {
            throw new NotImplementedException();
        }

        Flight IFlightManager.GetFlightById(string id)
        {
            throw new NotImplementedException();
        }

        void IFlightManager.AddRandomFlights()
        {
            throw new NotImplementedException();
        }

        Servers IFlightManager.GetServers()
        {
            throw new NotImplementedException();
        }
    }

    
}

