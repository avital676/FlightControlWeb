using Microsoft.VisualStudio.TestTools.UnitTesting;
using FlightControlWeb;
using FlightControlWeb.Models;
namespace FlightControlerTest
{
    [TestClass]
    public class test1
    {
        [TestMethod]
        public void TestMethod1()
        {
            FlightManager f = new FlightManager();
            f.AddRandomFlights();
           

        }
    }
}
