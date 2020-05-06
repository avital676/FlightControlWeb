using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.DataBase
{
    public class IdPlan
    {
        public string flight_id { get; set; }
        public FlightPlan plan { get; set; }
    }
}
