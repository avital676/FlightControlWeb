using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb
{
    public class IndexClass
    {
        public FileStream fs = File.Open("index.html", FileMode.Open, FileAccess.Write, FileShare.None);
    }
}
