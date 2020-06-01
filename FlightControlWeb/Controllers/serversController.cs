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
        public serversController(IFlightManager flightManager)
        {
            this.externalSer = flightManager.GetServers();
        }

        // GET: api/servers
        [HttpGet]
        public JsonResult Get()
        {
            try
            {
                return new JsonResult(externalSer.GetAllServers());
            }
            catch (Exception)
            {
                return null;
            }          
        }

        // POST: api/servers
        [HttpPost]
        public JsonResult Post(Server value)
        {
            try
            {
                externalSer.AddServer(value);
                return new JsonResult("Server added");
            }
            catch (Exception)
            {
                return null;
            }
        }

        // Delete: api/servers/5
        [HttpDelete("{id}")]
        public JsonResult Delete(string id)
        {
            try
            {
                externalSer.DeleteServer(id);
                return new JsonResult("Server deleted");
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
