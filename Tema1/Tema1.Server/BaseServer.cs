using Tema1.Core.Utilities;

namespace Tema1.Server
{
    public abstract class BaseServer
    {
        public virtual void Startup() {}
        public virtual Utils.ConnectionType Type => Utils.ConnectionType.TCP;
        protected string HostName { get; }
        protected int Port { get; }
        protected BaseServer(string name, int port)
        {
            HostName = name;
            Port = port;
        }
    }
}