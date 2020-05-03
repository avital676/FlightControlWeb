using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.DataBase
{
    public class Flight
    {
        string flight_id;
        double longitude;
        double latitude;
        int passengers;
        string company_name;
        string date_time;
        bool is_external;

        public Flight() { }
    }

}
