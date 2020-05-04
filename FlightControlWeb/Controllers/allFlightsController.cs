using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightControlWeb.DataBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightControlWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class allFlightsController : ControllerBase
    {
        private MyFlights myFlights = new MyFlights();
        // GET: api/allFlights
        [HttpGet]
        public IEnumerable<Flight> GetAllFlights()
        {
            return myFlights.gatAllFlights();
        }
    }
}
