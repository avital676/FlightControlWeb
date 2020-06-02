using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightControlWeb.Models;
using FlightControlWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Newtonsoft.Json;

namespace FlightControlWeb.Models.Tests
{
    [TestClass()]
    public class FlightControlWebTests
    {
        [TestMethod()]
        public void AddingMulitpleFlightsTest()
        {
            FlightManager flightManager = new FlightManager();
            FlightPlanController flightPlanController = new FlightPlanController(flightManager);
            string cuurent = Directory.GetCurrentDirectory();
            string path = Directory.GetCurrentDirectory();
            string[] path2 = path.Split("bin");
            string path3 = (path2[0] + "Resources");
            path3 = path3 + "\\";
            path3 = path3 + "FlightPlanJsons.json";
            string json = File.ReadAllText(path3);
            List<FlightPlan> flightPlanList = JsonConvert.DeserializeObject<List<FlightPlan>>(json);
            for (int i = 0; i < flightPlanList.Count; i++)
            {
                flightPlanController.Post(flightPlanList[i]);
            }
            List<Flight> allFlights = (List<Flight>) flightManager.getAllFlights();
            Assert.AreEqual(flightPlanList.Count, allFlights.Count);
        }

        [TestMethod()]
        public void GetNotExistFlightReturnsNull()
        {
            FlightManager flightManager = new FlightManager();
            FlightPlanController flightPlanController = new FlightPlanController(flightManager);
            JsonResult jsonResult = flightPlanController.Plan("aa1");
            Assert.IsNull(jsonResult);
        }

        [TestMethod()]
        public void DeleteNotExistFlightReturnsNull()
        {
            FlightManager flightManager = new FlightManager();
            FlightsController flightsController = new FlightsController(flightManager);
            JsonResult jsonResult = flightsController.Delete("aa1");
            Assert.IsNull(jsonResult);
        }

        [TestMethod()]
        public void AddAndDeleteFlightTest()
        {
            FlightManager flightManager = new FlightManager();
            FlightsController flightsController = new FlightsController(flightManager);
            FlightPlanController flightPlanController = new FlightPlanController(flightManager);
            List<Segment> nySeg = new List<Segment>()
            {
                new Segment{  Longitude=-80.289, Latitude = 25.6968,  Timespanseconds = 25 },
                new Segment{  Longitude=-58.455, Latitude = -34.75,  Timespanseconds = 25 }
            };
            InitialLocation loc1 = new InitialLocation { Latitude = 40.7611, Longitude = -73.946668, DateTime = "1990-12-26T23:56:03Z" };
            FlightPlan flightplan = new FlightPlan { Passengers = 420, CompanyName = "New York Airlines", InitialLocation = loc1, Segments = nySeg };
            flightPlanController.Post(flightplan);
            List<Flight> flights = (List<Flight>)flightManager.getAllFlights("1990-12-26T23:56:03Z");
            if (flights.Count == 0)
            {
                Assert.Fail();
            }
            JsonResult jsonResult = flightsController.Delete(flights[0].FlightId);
            Assert.IsNotNull(jsonResult);
        }

        [TestMethod()]
        public void TryToGetFlightThatEnded1SecAgo()
        {
            FlightManager flightManager = new FlightManager();
            FlightsController flightsController = new FlightsController(flightManager);
            FlightPlanController flightPlanController = new FlightPlanController(flightManager);
            List<Segment> nySeg = new List<Segment>()
            {
                new Segment{  Longitude=-80.289, Latitude = 25.6968,  Timespanseconds = 25 },
                new Segment{  Longitude=-58.455, Latitude = -34.75,  Timespanseconds = 25 }
            };
            InitialLocation loc1 = new InitialLocation { Latitude = 40.7611, Longitude = -73.946668, DateTime = "1995-12-26T23:56:03Z" };
            FlightPlan flightplan = new FlightPlan { Passengers = 420, CompanyName = "New York Airlines", InitialLocation = loc1, Segments = nySeg };
            flightPlanController.Post(flightplan);
            List<Flight> flights = (List<Flight>)flightManager.getAllFlights("1995-12-26T23:56:54Z");
            Assert.AreEqual(0, flights.Count);
        }
    }
}