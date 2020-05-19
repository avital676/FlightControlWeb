using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class Servers
    {
        private ConcurrentDictionary<string, Server> myServers = new ConcurrentDictionary<string, Server>();

        public void DeleteServer(string id)
        {
            Server ser = myServers[id];
            if (ser == null)
                throw new Exception("Server not found");
            myServers.TryRemove(id, out ser);
        }

        public void AddServer(Server server) 
        {
            this.myServers.TryAdd(server.ServerId, server);
        }

        public List<Server> GetAllServers()
        {
            List<Server> list = new List<Server>();
            foreach (KeyValuePair<string, Server> entry in myServers)
            {
                list.Add(entry.Value);
            }
            return list;
        }
        public Server GetServerById(string id)
        {
            Server ser = myServers[id];
            if (ser == null)
                throw new Exception("Server not found");
            return ser;
        }
    }
}
