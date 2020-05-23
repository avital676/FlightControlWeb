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
        public JsonResult Plan(string id)
        {
            try
            {
                // return the flight plan with this id:
                return new JsonResult(flightMan.GetFlightById(id).getFlightPlan());
            } catch (Exception)
            {
               // Response.StatusCode = 500;
                Response.WriteAsync("Couldn't get FlightPlan");
                return null;
            }
            //return flightMan.GetFlightById(id).getFlightPlan();
        }

        // POST: api/FlightPlan
        [HttpPost]
        public void Post([FromBody] FlightPlan value)
        {
            // WHAT HAPPENS IF VALUE ISNT A FLIGHT PLAN?????????
            flightMan.addFlight(value);
        }
    }
}
