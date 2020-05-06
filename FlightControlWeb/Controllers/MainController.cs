using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FlightControlWeb.DataBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FlightControlWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MainController : ControllerBase
    {
        private readonly ILogger<MainController> _logger;

        public MainController(ILogger<MainController> logger)
        {
            _logger = logger;
            //MyFlights myFlights = MyFlights.Instance;
        }
    }
}

        /* public HttpResponseMessage Get()
         {
             var fileContents = HttpContent(System.IO.File.ReadAllText("wwwroot/index.html"));
             var response = new HttpResponseMessage();
             response.Content = fileContents;
             response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
             return response;
         }*/


    
