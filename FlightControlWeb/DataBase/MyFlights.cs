using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.DataBase
{
    public sealed class MyFlights
    {
        private static MyFlights instance = null;
        private static readonly object padlock = new object();

        MyFlights() { }

        public static MyFlights Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new MyFlights();
                    }
                }
                return instance;
            }
        }

        private static List<Flight> myFlights = new List<Flight>()
        {
             new Flight{ flight_id="1234",longitude= 98,latitude = 70,passengers= 90, company_name ="elal", date_time="04.05.20",is_external = true },
             new Flight{ flight_id="5678",longitude= 93,latitude = 60,passengers= 100, company_name ="arkia", date_time="09.10.20",is_external = true },
            new Flight{ flight_id="9012",longitude= 90,latitude = 50,passengers= 80, company_name ="Tailand", date_time="18.09.20",is_external = true }
        };

        private static List<KeyValuePair<string, FlightPlan>> IdPlanList = new List<KeyValuePair<string, FlightPlan>>() { };
        private static List<FlightPlan> flightPlans = new List<FlightPlan>() { };

        public IEnumerable<Flight> getAllFlights()
        {
            return myFlights;
        }

        public IEnumerable<FlightPlan> getPlans()
        {
            return flightPlans;
        }

        public void addFlight(Flight f)
        {
            myFlights.Add(f);
        }

        public void addFlightPlan(FlightPlan fp)
        {
            flightPlans.Add(fp);
            Flight f = PlanConverter(fp);
            myFlights.Add(f);
            IdPlanList.Add(new KeyValuePair<string, FlightPlan>(f.flight_id, fp));
        }

        private Flight PlanConverter(FlightPlan fp)
        {
            Flight f = new Flight();
            f.flight_id = createID();
            f.longitude = fp.longitude;
            f.latitude = fp.latitude;
            f.passengers = fp.passengers;
            f.company_name = fp.company_name;
            f.date_time = fp.date_time;
            f.is_external = false;
            return f;
        }

        private int i = 0;
        private String createID()
        {
            return (++i).ToString();
        }

        public void deleteFlight(string id)
        {

        }



    }
}