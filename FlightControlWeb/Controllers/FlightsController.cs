﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlightControlWeb.DataBase;
using FlightControlWeb.Models;

namespace FlightControlWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {

        // GET: api/Flights
        [HttpGet]
        public IEnumerable<Flight> GetAllFlights()
        {
            return MyFlights.Instance.getAllFlights();
        }

        // GET: api/Flights/5
        [HttpGet("{id}", Name = "Get")]
        public Flight Get(string id)
        {
           return MyFlights.Instance.GetFlightById(id);
        }

        // POST: api/Flights
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            MyFlights.Instance.DeleteFlight(id);
        }
    }
}
