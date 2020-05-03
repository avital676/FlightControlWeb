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
            MyFlights myFlights = MyFlights.Instance;
        }

        [HttpGet]
       /* public HttpResponseMessage Get()
        {
            var fileContents = HttpContent(System.IO.File.ReadAllText("wwwroot/index.html"));
            var response = new HttpResponseMessage();
            response.Content = fileContents;
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
            return response;
        }*/


        public HttpResponseMessage Get()
        {
            //IndexClass ic = new IndexClass();
            //var fileContents = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Content/HelloWorld.html"));

            StreamReader reader = new StreamReader("wwwroot/index.html");
             string readFile = reader.ReadToEnd();
             //  var fileContents = File.ReadAllText(HttpContext.Current.Server.MapPath("~/Content/HelloWorld.html"));
               var fileContents = readFile;
               var response = new HttpResponseMessage();
               response.Content = new StringContent(fileContents);
               response.Content.Headers.ContentType = new MediaTypeHeaderValue("text/html");
               return response;

        }
        /*public string Get()
        {
            //'C:\Users\avita\source\repos\FlightControl\FlightControlWeb\testhtml.htm'

           // StreamReader reader = new StreamReader(("testhtml.html"));
           // string readFile = reader.ReadToEnd();
          //  return readFile;
            return "This is a test";
        }*/

        public List<string> Get(int Id)
        {
            return new List<string> {
                "Data1",
                "Data2"
            };
        }
    }
}