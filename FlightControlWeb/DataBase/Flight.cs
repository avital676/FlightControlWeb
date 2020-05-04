using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.DataBase
{
    public class Flight
    {
        public string flight_id { get; set; }
        public double longitude { get; set; }
        public double latitude { get; set; }
        public  int passengers { get; set; }
        public string company_name { get; set; }
        public string date_time { get; set; }
        public bool is_external { get; set; }
 /*       public Flight(string flight_id, double longitude, double latitude, int passengers, string company_name, string date_time, bool is_external)
        {
            flight_id = flight_id;
            longitude = longitude;
            latitude = latitude;
            passengers = passengers;
            company_name = company_name;
            date_time = date_time;
            is_external = is_external;
        }/
        */

    }

}
