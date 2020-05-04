using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightControlWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        // GET: api/Flights
        [HttpGet]
        public IEnumerable<string> GetInternal()
        {
            //get relative;
            return new string[] { "value1", "value2" };
        }

        // GET: api/Flights/5
        [HttpGet]
        public IEnumerable<string> GetAll()
        {
            //get relative;
            return new string[] { "value1", "value2" };
        }


        // Delete: api/Flights/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            //delete flight with this id
        }

    }
}
