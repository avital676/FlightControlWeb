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
            if (f == null)
            {
                return false;
            }
            if (f.Passengers < 0)
                return false;
            // invalid initial location:
            if (f.InitialLocation.Longitude > 180 || f.InitialLocation.Longitude < -180)
                return false;
            if (f.InitialLocation.Latitude > 90 || f.InitialLocation.Latitude < -90)
                return false;
            // invalid initial location:
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
            // day <= 31
            if (dateTime[8] > '3')
            {
                return false;
            }
            if ((dateTime[8] == '3') && (dateTime[9] > '1'))
            {
                return false;
            }
            // month <= 12
            if (dateTime[5] > '1')
            {
                return false;
            }
            if ((dateTime[5] == '1') && (dateTime[6] > '2'))
            {
                return false;
            }
            // hour <= 23
            if (dateTime[11] > '2')
            {
                return false;
            }
            if ((dateTime[11] == '2') && (dateTime[12] > '3'))
            {
                return false;
            }
            // minutes <= 59
            if (dateTime[14] > '5')
            {
                return false;
            }
            if ((dateTime[14] == '5') && (dateTime[15] > '9'))
            {
                return false;
            }
            // seconds <= 59
            if (dateTime[17] > '5')
            {
                return false;
            }
            if ((dateTime[17] == '5') && (dateTime[18] > '9'))
            {
                return false;
            }
            // seperators:
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
            if (dateTime[19] != 'Z')
            {
                return false;
            }
            return true;

        }

    }
}