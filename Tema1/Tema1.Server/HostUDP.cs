using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Tema1.Core.Format;
using Tema1.Core.Utilities;

namespace Tema1.Server
{
    public class HostUDP : BaseServer
    {
        private UdpClient _currentSocket;
        public HostUDP(string serverName, int serverPort) : base(serverName, serverPort){}
        public override Utils.ConnectionType Type => Utils.ConnectionType.UDP;

        public override void Startup()
        {
            ServerSetupSocket();
            TransferData();
        }

        private void ServerSetupSocket()
        {
            Console.WriteLine(Utils.GetMessageStartedUDP);
            _currentSocket = new UdpClient(new IPEndPoint(IPAddress.Any, this.Port));
        }

        private void TransferData()
        {
            while (true)
            {
                byte[] data = new byte[Utils.BufferSize];
                var sender = new IPEndPoint(IPAddress.Any, 0);
                data = _currentSocket.Receive(ref sender);
                var file = data.ByteArrayToObject() as DataFile;
                new FileTransfer().TrasferRecvDataInfo(Guid.NewGuid().ToString(), file);
                data = Encoding.ASCII.GetBytes(Utils.ETAG);
                _currentSocket.Send(data, data.Length, sender);
            }
        }
    }
}