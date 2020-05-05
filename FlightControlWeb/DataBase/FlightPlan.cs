﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.DataBase
{
    public class FlightPlan
    {
        public int passengers { get; set; }
        public string company_name { get; set; }
        public double longitude { get; set; }
        public double latitude { get; set; }
        public string date_time { get; set; }
        public List<Segment> segments { get; set; }
    }
}