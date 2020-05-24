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
    public class serversController : ControllerBase
    {
        private Servers externalSer;
        public serversController()
        {
            this.externalSer = new Servers();
        }

        // GET: api/servers
        [HttpGet]
        public IEnumerable<Server> Get()
        {
            return externalSer.GetAllServers();
        }

        // POST: api/servers
        [HttpPost]
        public Server Post(Server value)
        {
            externalSer.AddServer(value);
            return value;
        }

        // Delete: api/servers/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            externalSer.DeleteServer(id);
        }
    }
}
