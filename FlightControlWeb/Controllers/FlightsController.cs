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
        // GET: api/Flights
        [HttpGet]
        public IEnumerable<Flight> GetAllFlights(string relative_to)
        {
            MyFlights.Instance.AddRandomFlights();
            return MyFlights.Instance.getAllFlights(relative_to);
            //return MyFlights.Instance.getAllFlights();
        }

        /** // GET: api/Flights/5
         [HttpGet("{id}", Name = "Get")]
         public Flight flight(string id)
         {
            return MyFlights.Instance.GetFlightById(id);
         }*/

        // POST: api/Flights
        [HttpPost]
        public FlightPlan Post(FlightPlan value)
        {
            MyFlights.Instance.addFlight(value);
            return value;
        }

        // DELETE: api/Flights/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            MyFlights.Instance.DeleteFlight(id);
        }
    }
}
