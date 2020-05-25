using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightControlWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using FlightControlWeb.Models;

namespace FlightControlWeb.Controllers.Tests
{
    [TestClass()]
    public class FlightsControllerTests
    {
        private IFlightManager flightMan = new FlightManager();
        [TestMethod()]
        public void GetAllFlightsTest()
        {
            flightMan.AddRandomFlights();
            FlightsController fc = new FlightsController(flightMan);
            FlightPlanController fpc = new FlightPlanController(flightMan);
            fc.Delete("23");
    
            Assert.Fail();
        }
    }
}