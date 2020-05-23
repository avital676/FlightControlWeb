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
        private IFlightManager flightManager;

        public FlightsController(IFlightManager flightManager)
        {
            this.flightManager = flightManager;
        }

        // GET: api/Flights
        [HttpGet]
        public JsonResult GetAllFlights(string relative_to)
        {
            flightManager.AddRandomFlights();
            try
            {
                var syncAll = Request.Query["sync_all"].ToList();
                if (syncAll.Count != 0)
                {
                    //returns myflight , servers flights
                    IEnumerable <Flight> flights = flightManager.getAllFlightsSync("2020-12-26T23:56:03Z");
                    return new JsonResult(flights);
                } else
                {
                    return new JsonResult(flightManager.getAllFlights(relative_to));
                }
            } catch (Exception)
            {
                // Response.StatusCode = 500;
                Response.WriteAsync("Couldn't get Flights list");
                return null;
            }

        }

        // POST: api/Flights
        [HttpPost]
        public FlightPlan Post(FlightPlan value)
        {
            flightManager.addFlight(value);
            return value;
        }

        // DELETE: api/Flights/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            flightManager.DeleteFlight(id);
        }
    }
}
