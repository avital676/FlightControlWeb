using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.DataBase
{
    public class MyFlights
    {
        private static readonly MyFlights instance = new MyFlights();

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static MyFlights()
        { }

        private MyFlights()
        {
            List<Flight> allFlights;
        }

        public static MyFlights Instance
        {
            get { return instance; }
        }
    }
}
