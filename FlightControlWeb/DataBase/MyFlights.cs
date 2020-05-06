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
             new Flight{ flight_id="1234",longitude= 98,latitude = 0,passengers= 90, company_name ="elal", date_time="04.05.20",is_external = true },
             new Flight{ flight_id="5678",longitude= 98,latitude = 0,passengers= 100, company_name ="arkia", date_time="09.10.20",is_external = true },
            new Flight{ flight_id="9012",longitude= 98,latitude = 0,passengers= 80, company_name ="Tailand", date_time="18.09.20",is_external = true } 
        };
    
        public IEnumerable<Flight> getAllFlights(){
            return myFlights;
        }

        public void addFlight(Flight f)
        {
           // myFlights.Add(f);
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
