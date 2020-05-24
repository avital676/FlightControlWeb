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
        public JsonResult Get()
        {
            try
            {
                return new JsonResult(externalSer.GetAllServers());
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                Response.WriteAsync("Error getting servers");
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
                // return 200 OK:
                Response.StatusCode = 200;
                Response.WriteAsync("FlightPlan added");
                return new JsonResult("OK");
            }
            catch (Exception)
            {
                Response.StatusCode = 400;
                Response.WriteAsync("Couldn't add server");
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
                // return 200 OK:
                Response.StatusCode = 200;
                Response.WriteAsync("Server deleted");
                return new JsonResult("OK");
            }
            catch (Exception e)
            {
                Response.StatusCode = 400;
                Response.WriteAsync(e.Message);
                return null;
            }
        }
    }
}
