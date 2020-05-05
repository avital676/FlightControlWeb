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
    public class FlightsController : ControllerBase
    {
        private MyFlights myFlights = new MyFlights();

        // GET: api/Flights
        [HttpGet]
        public IEnumerable<Flight> GetAllFlights()
        {
            return myFlights.gatAllFlights();
            
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
