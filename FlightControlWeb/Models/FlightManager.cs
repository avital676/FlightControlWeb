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
        private ConcurrentDictionary<string, Flight> myFlights;
        private List<Flight> externalFlights;
        private Servers servers;

        public FlightManager()
        {
            this.myFlights = new ConcurrentDictionary<string, Flight>();
            this.servers = new Servers();
            this.externalFlights = new List<Flight>();
            AddRandomFlights();
        }

        public void addFlight(FlightPlan flightPlan)
        {
            if (FPValidation.validate(flightPlan))
            {
                // flight plan parameters are valid, add flight:
                Flight flight = new Flight(flightPlan);
                myFlights.TryAdd(flight.FlightId, flight);
            } else
            {
                throw new Exception();
            }
            
        }

        public void addFlight(Flight flight)
        {
            if (FPValidation.validate(flight.flightPlan))
            {
                // flight plan parameters are valid, add flight:
                myFlights.TryAdd(flight.FlightId, flight);
            }
            else
            {
                throw new Exception();
            }
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
            List<Flight> internalFlights = new List<Flight>();
            foreach (KeyValuePair<string, Flight> entry in myFlights)
            {
                internalFlights.Add(entry.Value);
            }
            return internalFlights;
        }

        public IEnumerable<Flight> getAllFlights(string relativeTo)
        {
            DateTime clientDT = DateTime.Parse(relativeTo);
            return GetRelevantFlights(clientDT);
        }
        
        public IEnumerable<Flight> getAllFlightsSync(string relativeTo)
        {
            //externalFlights.Clear();
            List<Flight> allFlights = new List<Flight>();
            DateTime clientDT = DateTime.Parse(relativeTo);
            allFlights = (List<Flight>) GetRelevantFlights(clientDT);
            List<Server> allServers = this.servers.GetAllServers();
            if (allServers.Count == 0)
            {
                return allFlights;
            }
            for (int i = 0; i < servers.GetAllServers().Count; i++)
            {
                Server ser = allServers[i];
                Task<List<Flight>> taskflights = GetFlightURLID(ser.ServerURL, relativeTo);
                List<Flight> exFlights = taskflights.Result;
                SetAsExternal(exFlights, ser.ServerURL);
                //externalFlights.AddRange(exFlights);
                allFlights.AddRange(exFlights);
            }
            return allFlights;
        }

        private async Task<List<Flight>> GetFlightURLID(string url, string relativeTo)
        {
            string URL = String.Format(url + "/api/Flights?relative_to=" + relativeTo);
            WebRequest req = WebRequest.Create(URL);
            req.Method = "GET";
            HttpWebResponse resp = null;
            resp = (HttpWebResponse)(await req.GetResponseAsync());
            string result = null;
            List<Flight> externalFlights;
            using (Stream str = resp.GetResponseStream())
            {
                StreamReader strRead = new StreamReader(str);
                result = strRead.ReadToEnd();
                externalFlights = JsonConvert.DeserializeObject<List<Flight>>(result);
                strRead.Close();
            }
            return externalFlights;
        }

        private async Task<FlightPlan> getFlightPlanExternalServer(string url, string id)
        {
            string URL = String.Format(url + "/api/FlightPlan/" + id);
            WebRequest req = WebRequest.Create(URL);
            req.Method = "GET";
            HttpWebResponse resp = null;
            resp = (HttpWebResponse)(await req.GetResponseAsync());
            string result = null;
            FlightPlan flightPlan;
            using (Stream str = resp.GetResponseStream())
            {
                StreamReader strRead = new StreamReader(str);
                result = strRead.ReadToEnd();
                flightPlan = JsonConvert.DeserializeObject<FlightPlan>(result);
                strRead.Close();
            }
            return flightPlan;
        }

        private void SetAsExternal(List<Flight> flights, string url)
        {
            FlightPlan flightPlan;
            int i;
            for (i = 0; i < flights.Count; i++)
            {
                flights[i].IsExternal = true;
                Task<FlightPlan> task = getFlightPlanExternalServer(url, flights[i].FlightId);
                flightPlan = task.Result;
                Flight flight = new Flight(flightPlan);
                flight.FlightId = flights[i].FlightId;
                if (externalFlights.Where(x => x.FlightId == flight.FlightId).FirstOrDefault() == null)
                {
                    this.externalFlights.Add(flight);
                }
            }
        }
      
        public Flight GetFlightById(string id)
        {
            Flight flight;
            try
            {
                flight = myFlights[id];
            } catch (Exception)
            {
                flight = externalFlights.Where(x => x.FlightId == id).FirstOrDefault();
                if (flight == null)
                    throw new Exception("Flight not found");
            }
            /**if (flight == null)
            {
                flight = externalFlights.Where(x => x.FlightId == id).FirstOrDefault();
                if (flight == null)
                    throw new Exception("Flight not found");
            }*/
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
                }
                else
                {
                    relevantFlights.Add(flight);
                }
            }
            return relevantFlights;
        }

        public Servers GetServers()
        {
            return this.servers;
        }

        public static List<Segment> indiaSeg = new List<Segment>()
        {
            new Segment{  Longitude=13.948911, Latitude = 41.749457,  Timespanseconds = 25 },
            new Segment{  Longitude=78.962883, Latitude = 20.593683,  Timespanseconds = 25 }
        };

        public static List<Segment> nySeg = new List<Segment>()
        {
            new Segment{  Longitude=-80.289, Latitude = 25.6968,  Timespanseconds = 25 },
            new Segment{  Longitude=-58.455, Latitude = -34.75,  Timespanseconds = 25 }
        };

        public static List<Segment> londonSeg = new List<Segment>()
        {
            new Segment{  Longitude=4.895, Latitude = 52.37,  Timespanseconds = 25 },
            new Segment{  Longitude=-102.55, Latitude = 23.634,  Timespanseconds = 25 }
        };

        private int k = 0;
        public void AddRandomFlights()
        {
            if (k == 0)
            {
                myFlights.Clear();
                InitialLocation loc1 = new InitialLocation { Latitude = 40.7611, Longitude = -73.946668, DateTime = "2020-12-26T23:56:03Z" };
                Flight flight1 = new Flight(new FlightPlan { Passengers = 420, CompanyName = "New York Airlines", InitialLocation = loc1, Segments = nySeg });
                myFlights.TryAdd(flight1.FlightId, flight1);

                InitialLocation loc2 = new InitialLocation { Latitude = 51.507, Longitude = -0.127, DateTime = "2020-12-26T23:56:03Z" };
                Flight flight2 = new Flight(new FlightPlan { Passengers = 420, CompanyName = "British Airways", InitialLocation = loc2, Segments = londonSeg });
                myFlights.TryAdd(flight2.FlightId, flight2);

                InitialLocation loc5 = new InitialLocation { Latitude = 31.912154, Longitude = 35.114953, DateTime = "2020-12-26T23:56:03Z" };
                Flight flight5 = new Flight(new FlightPlan { Passengers = 220, CompanyName = "Air India", InitialLocation = loc5, Segments = indiaSeg });
                myFlights.TryAdd(flight5.FlightId, flight5);

                k++;
            }
        }
    }
}

