using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlightControlWeb.Models;

namespace FlightControlWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private IFlightManager flightMan;

        public FlightsController(IFlightManager f)
        {
            flightMan = f;
        }

        // GET: api/Flights
        [HttpGet]
        public IEnumerable<Flight> GetAllFlights(string relative_to)
        {
            flightMan.AddRandomFlights();
            var syncAll = Request.Query["sync_all"].ToList();
            if (syncAll.Count != 0)
            {
                //returns myflight , servers flights
                IEnumerable <Flight> flights = flightMan.getAllFlightsSync("2020-12-26T23:56:03Z");
                return flights;
            } else
            {
                return flightMan.getAllFlights(relative_to);
            }
        }

        // POST: api/Flights
        [HttpPost]
        public FlightPlan Post(FlightPlan value)
        {
            flightMan.addFlight(value);
            return value;
        }

        // DELETE: api/Flights/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            flightMan.DeleteFlight(id);
        }
    }
}
