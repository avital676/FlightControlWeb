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
                FlightPlan fp= flightMan.GetFlightById(id).getFlightPlan();
                // return the flight plan with this id:
                return new JsonResult(flightMan.GetFlightById(id).getFlightPlan());
            } catch (Exception)
            {
                return null;
            }
        }

        // POST: api/FlightPlan
        [HttpPost]
        public JsonResult Post([FromBody] FlightPlan value)
        {
            try
            {
                // add flight plan to flight manager:
                flightMan.addFlight(value);
                return new JsonResult("FlightPlan added");
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
