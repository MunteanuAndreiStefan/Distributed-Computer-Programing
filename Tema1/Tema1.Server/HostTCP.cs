using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Tema1.Core.Format;
using Tema1.Core.Utilities;

namespace Tema1.Server
{
    public class HostTCP : BaseServer
    {
        public TcpListener Server { get; private set; }

        public HostTCP(string hostName, int port) : base(hostName, port) => Server = new TcpListener(IPAddress.Parse(hostName), port);

        public override void Startup()
        {
            ServerSetupSocket();
            TreatNewClient();
        }

        private void ServerSetupSocket()
        {
            Server.Start();
            Console.WriteLine(Utils.GetMessageStartedTCP);
        }

        private void TreatCurrentSession(TcpClient client)
        {
            while (client.Connected)
            {
                var stream = client.GetStream();
                if (!ReadAvailable(stream)) continue;

                Console.WriteLine(Utils.GetMessageReciving);
                while (stream.DataAvailable)
                    ProcessDataAvailable(stream);
            }
        }

        private void TreatNewClient()
        {
            while (true)
            {
                try
                {
                    var client = Server.AcceptTcpClientAsync().Result;
                    client.NoDelay = true;
                    TreatCurrentSession(client);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        private bool ReadAvailable(NetworkStream stream) => stream.CanRead && stream.DataAvailable;
        private void PrintFinalStatus(int nrPackages, int nrBytes) => Console.WriteLine($"{Utils.GetDataRecvPackageNumberTCP(nrPackages)}\n{Utils.GetDataReadBytesNumberTCP(nrBytes)}");

        private void ProcessDataAvailable(NetworkStream stream)
        {
            try
            {
                var data = new List<byte>();
                var readBuffer = new byte[Utils.BufferSize];
                var dataBytesNumber = new byte[Utils.DataBytesNumber];
                var seq = 0;
                var size = stream.ReadAsync(dataBytesNumber, 0, 4).Result;
                var dataNumberOfBytesInt = BitConverter.ToInt32(dataBytesNumber, 0);

                while (data.Count < dataNumberOfBytesInt)
                {
                    var dataResult = stream.Read(readBuffer, 0, readBuffer.Length);
                    seq++;
                    data.AddRange(readBuffer);
                }

                var result = data.ToArray().ByteArrayToObject();

                if (result != null && result is DataFile fileData)
                    new Task(() => { new FileTransfer().TrasferRecvDataInfo(Guid.NewGuid().ToString(), fileData); }).Start();

                PrintFinalStatus(seq, data.Count);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}