﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightControlWeb.DataBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightControlWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class allFlightsController : ControllerBase
    {
        private MyFlights myFlights = new MyFlights();
        // GET: api/allFlights
        [HttpGet]
        public IEnumerable<Flight> GetAllFlights()
        {
            return myFlights.gatAllFlights();
        }

        // GET: api/allFlights/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/allFlights
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/allFlights/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}