using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightControlWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightControlWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightPlanController : ControllerBase
    {
        private IFlightManager flightMan;

        public FlightPlanController(IFlightManager f)
        {
            flightMan = f;
        }

        //GET: api/FlightPlan/5
        [HttpGet("{id}", Name = "Get")]
        public FlightPlan Plan(string id)
        {
            // return the flight with this id
            return flightMan.GetFlightById(id).getFlightPlan();
        }
    
        // POST: api/FlightPlan
        [HttpPost]
        public void Post([FromBody] FlightPlan value)
        {
            flightMan.addFlight(value);
        }
    }
}
