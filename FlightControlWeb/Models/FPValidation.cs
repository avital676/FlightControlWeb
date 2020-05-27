using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class FPValidation
    {
        public static bool validate(FlightPlan f)
        {
            //// check all isnt null
            ///

            if (f.Passengers < 0)
                return false;
            // invalid initial location:
            if (f.InitialLocation.Longitude > 180 || f.InitialLocation.Longitude < -180)
                return false;
            if (f.InitialLocation.Latitude > 90 || f.InitialLocation.Latitude < -90)
                return false;
            ////// // CHECK DATETIME INITIAL LOCATION
            ///
            bool check = CheckTime(f.InitialLocation.DateTime);
            if (!check)
            {
                return false;
            }
            // no segments in flight plan:
            if (f.Segments.Count == 0)
                return false;
            foreach (var seg in f.Segments)
            {
                // invalide locatoin:
                if (seg.Longitude > 180 || seg.Longitude < -180)
                    return false;
                if (seg.Latitude > 90 || seg.Latitude < -90)
                    return false;
                // invalid timespan:
                if (seg.Timespanseconds < 0)
                    return false;
            }
            // all parameters are valid:
            return true;
        }

        private static bool CheckTime(string dateTime)
        {
            //hour is no bigger then 24
            if ((dateTime[12] > 4) && (dateTime[11] >= 2))
            {
                return false;
            }
            //minute is no bigger then 60
            if ((dateTime[15] > 6) && (dateTime[16] > 0))
            {
                return false;
            }
            if (dateTime[4] != '-')
            {
                return false;
            }
            if (dateTime[7] != '-')
            {
                return false;
            }
            if (dateTime[10] != 'T')
            {
                return false;
            }
            if (dateTime[18] != 'Z')
            {
                return false;
            }
            return true;

        }

    }
}