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
    public class FlightPlanController : ControllerBase
    { 
/*
        // GET: api/FlightPlan/5
        [HttpGet("{id}", Name = "Get")]
        public string GetPlan(int id)
        {
            // return the flight with this id
            return "value";
        }
*/
        // POST: api/FlightPlan
        [HttpPost]
        public void Post([FromBody] FlightPlan value)
        {
            MyFlights.Instance.addFlightPlan(value);
        }

        
    }
}
