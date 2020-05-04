using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.DataBase
{
    public class MyFlights
    {
        // Flight f = new Flight("1234", 98, 0, 90, "elal", "04.05.20", true);
        // private static List<Flight> allFlights = new List<Flight>();
        private static List<Flight> allFlights = new List<Flight>()
        {
             new Flight{ flight_id="1234",longitude= 98,latitude = 0,passengers= 90, company_name ="elal", date_time="04.05.20",is_external = true } 
        };
    


        //public MyFlights() {
            //private static readonly MyFlights instance = new MyFlights();

            // Explicit static constructor to tell C# compiler
            // not to mark type as beforefieldinit
            //     static MyFlights()
            //    { }
            //allFlights.Add(f);
        
  //  }
       
       
            
        public IEnumerable<Flight> gatAllFlights(){
         //   Console.WriteLine(allFlights);
        return allFlights;
        }


    //    public static MyFlights Instance
    //    {
   //         get { return instance; }
     //   }
    }
}
