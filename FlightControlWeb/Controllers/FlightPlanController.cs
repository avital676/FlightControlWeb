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
                Response.WriteAsync("Flight Plan not found");
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
                // return 200 OK:
                Response.StatusCode = 200;
                Response.WriteAsync("FlightPlan added");
                return new JsonResult("OK");
            }
            catch (Exception)
            {
                Response.StatusCode = 400;
                Response.WriteAsync("Couldn't add FlightPlan");
                return null;
            }
        }
    }
}
