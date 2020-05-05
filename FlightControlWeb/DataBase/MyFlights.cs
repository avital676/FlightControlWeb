using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.DataBase
{
    public class MyFlights
    {
        private static List<Flight> myFlights = new List<Flight>()
        {
             new Flight{ flight_id="1234",longitude= 98,latitude = 0,passengers= 90, company_name ="elal", date_time="04.05.20",is_external = true } 
        };
    
        public IEnumerable<Flight> gatAllFlights(){
           return myFlights;
        }

        public void addFlight(Flight f)
        {
            myFlights.Add(f);
        }

        public void deleteFlight(string id)
        {

        }



    //    public static MyFlights Instance
    //    {
   //         get { return instance; }
     //   }
    }
}
