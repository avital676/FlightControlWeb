using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlightControlWeb.DataBase;
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
            if (relative_to.Contains("sync_all"))
            {
                //returns myflight , servers flights
                flightMan.AddRandomFlights();
                return flightMan.getAllFlights(relative_to);
                //MyFlights.Instance.AddRandomFlights();
                //return MyFlights.Instance.getAllFlights(relative_to);
            } else
            {
                flightMan.AddRandomFlights();
                return flightMan.getAllFlights(relative_to);
            }
            //return MyFlights.Instance.getAllFlights();
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
