using System.Collections.Generic;
using System.Net;
using Tema1.Core.Utilities;

namespace Tema1.Server
{
    public class ServersPool
    {
        private readonly Dictionary<Utils.ConnectionType, BaseServer> _serverDict;

        public ServersPool() => _serverDict = new Dictionary<Utils.ConnectionType, BaseServer>
            {
                { Utils.ConnectionType.TCP, new HostTCP(IPAddress.Any.ToString(), Utils.ConnectionData.PortTCP) },
                { Utils.ConnectionType.UDP, new HostUDP(IPAddress.Any.ToString(), Utils.ConnectionData.PortUDP) }
            };

        public void Start(Utils.ConnectionType type)
        {
            if (_serverDict.ContainsKey(type))
                _serverDict[type].Startup();
        }
    }
}