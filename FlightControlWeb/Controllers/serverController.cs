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
    public class serverController : ControllerBase
    {
        // GET: api/server
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST: api/server
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // Delete: api/server/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

    }
}
