using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FlightControlWeb.Models;
using Newtonsoft.Json;

namespace FlightControlWeb.Models
{
    public class FlightManager : IFlightManager
    {
        private ConcurrentDictionary<string, Flight> myFlights = new ConcurrentDictionary<string, Flight>();
        Servers servers = new Servers();

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
            myFlights.TryAdd(flight.FlightId, flight);
        }

        public void addFlight(Flight flight)
        {
            myFlights.TryAdd(flight.FlightId, flight);
        }

        public void DeleteFlight(string id)
        {
            Flight flight = myFlights[id];
            if (flight == null)
                throw new Exception("Flight not found");
            myFlights.TryRemove(id, out flight);
        }

        public IEnumerable<Flight> getAllFlights()
        {
            List<Flight> temp = new List<Flight>();
            foreach (KeyValuePair<string, Flight> entry in myFlights)
            {
                temp.Add(entry.Value);
            }
            return temp;
        }

        public IEnumerable<Flight> getAllFlights(string relativeTo)
        {
            DateTime clientDT = DateTime.Parse(relativeTo);
            return GetRelevantFlights(clientDT);
        }
        public IEnumerable<Flight> getAllFlightsSync(string relativeTo)
        {
           IEnumerable<Flight> allFlights = new List<Flight>();
            DateTime clientDT = DateTime.Parse(relativeTo);
            allFlights = GetRelevantFlights(clientDT);
            List<Server> allServers = this.servers.GetAllServers();
            if(allServers.Count == 0)
            {
                return allFlights;
            }
                Server ser = allServers[i];
                string url = "http://" + "ronyut2.atwebpages.com/ap2/";
               Task<FlightPlan> Taskflight = GetFlightURLID(url, "YIZI34");
                FlightPlan flightp = Taskflight.Result;
                myFlights.TryAdd("YIZI34", new Flight(flightp));
                
                //string flightsOfServer =await response.Result.Content.ReadAsStringAsync();
            return allFlights;
        }
        private async Task<FlightPlan> GetFlightURLID(string url, string id)
        {
            string URL = String.Format(url + "/api/FlightPlan/" + id);
            WebRequest req = WebRequest.Create(URL);
            req.Method = "GET";
            HttpWebResponse resp = null;
            resp = (HttpWebResponse)(await req.GetResponseAsync());
            string result = null;
            FlightPlan fpAsJs;
            using(Stream str = resp.GetResponseStream())
            {
                StreamReader strRead = new StreamReader(str);
                result = strRead.ReadToEnd();
                fpAsJs = JsonConvert.DeserializeObject<FlightPlan>(result);
                strRead.Close();
            }
            return fpAsJs;
        }
      
        public Flight GetFlightById(string id)
        {
            Flight flight = myFlights[id];
            if (flight == null)
                throw new Exception("Flight not found");
            return flight;
        }

        public IEnumerable<Flight> GetRelevantFlights(DateTime relativeTo)
        {
            List<Flight> relevantFlights = new List<Flight>();
            foreach (KeyValuePair<string, Flight> entry in myFlights)
            {
                Flight flight = entry.Value;
                if (DateTime.Compare(relativeTo, DateTime.Parse(flight.DateTimee)) < 0)
                {
                    //Flight didnt start yet
                    continue;
                }
                else if (String.Compare(flight.UpdateLocation(relativeTo), "Ended") == 0)
                {
                    myFlights.TryRemove(flight.FlightId, out flight);
                    // myFlights.Remove(flight);
                }
                else
                {
                    relevantFlights.Add(flight);
                }
            }
            return relevantFlights;
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
                myFlights.TryAdd(flight1.FlightId, flight1);

                InitialLocation loc2 = new InitialLocation { Latitude = 51.507, Longitude = -0.127, Date_Time = "2020-12-26T23:56:03Z" };
                Flight flight2 = new Flight(new FlightPlan { Passengers = 420, Company_Name = "British Airways", Initial_Location = loc2, Segments = londonSeg });
                myFlights.TryAdd(flight2.FlightId, flight2);

                InitialLocation loc5 = new InitialLocation { Latitude = 31.912154, Longitude = 35.114953, Date_Time = "2020-12-26T23:56:03Z" };
                Flight flight5 = new Flight(new FlightPlan { Passengers = 220, Company_Name = "Air India", Initial_Location = loc5, Segments = indiaSeg });
                myFlights.TryAdd(flight5.FlightId, flight5);

                i++;
            }
        }
    }
}
   

