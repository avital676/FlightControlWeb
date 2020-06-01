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
            try
            {
                var syncAll = Request.Query["sync_all"].ToList();
                if (syncAll.Count != 0)
                {
                    //returns myflight , servers flights
                    IEnumerable <Flight> flights = flightManager.getAllFlightsSync(relative_to);
                    return new JsonResult(flights);
                } else
                {
                    return new JsonResult(flightManager.getAllFlights(relative_to));
                }
            } catch (Exception)
            {
                return null;
            }
        }

        // DELETE: api/Flights/5
        [HttpDelete("{id}")]
        public JsonResult Delete(string id)
        {
            try
            {
                flightManager.DeleteFlight(id);
                return new JsonResult("Flight deleted");
            } catch (Exception)
            {
                return null;
            }
        }
    }
}
