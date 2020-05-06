﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlightControlWeb.DataBase;

namespace FlightControlWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {


        // GET: api/Flights
        [HttpGet]
        public IEnumerable<FlightPlan> GetAllFlights()
        {
            return MyFlights.Instance.getPlans();
            //return MyFlights.Instance.getAllFlights();
        }

        // GET: api/Flights/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Flights
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        public void add(FlightPlan f)
        {

        }
    }
}
