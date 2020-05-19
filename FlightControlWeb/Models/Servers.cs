using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class Servers
    {
        List<Server> externalServers;
        
        public void DeleteServer(string id)
        {
            Server server = externalServers.Where(x => x.ServerId == id).FirstOrDefault();
            if (server == null)
                throw new Exception("Server not found");
            externalServers.Remove(server);
        }

        public void AddServer(Server server) 
        {
            this.externalServers.Add(server);
        }

        public List<Server> GetAllServers()
        {
            return this.externalServers;
        }
    }
}
