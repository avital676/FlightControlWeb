using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public interface IFlightManager 
    {
        public void addFlight(FlightPlan flightPlan);

        public void addFlight(Flight flight);

        public IEnumerable<Flight> getAllFlights();

        public IEnumerable<Flight> getAllFlights(string relativeTo);

        public IEnumerable<Flight> GetRelevantFlights(DateTime relativeTo);

        public void DeleteFlight(string id);

        public Flight GetFlightById(string id);

        public void AddRandomFlights();

    }
}
