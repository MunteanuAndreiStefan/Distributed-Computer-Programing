using Tema1.Core.Utilities;

namespace Tema1.Server
{
    public class ServerStart
    {
        public static void Main(string[] args) => new ServersPool().Start(Utils.STARTUP);
    }
}

