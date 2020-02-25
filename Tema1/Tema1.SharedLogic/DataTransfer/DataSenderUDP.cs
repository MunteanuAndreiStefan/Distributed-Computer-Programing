using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Tema1.Core.Format;
using Tema1.Core.Utilities;

namespace Tema1.Core.Senders
{
    public class DataSenderUDP : BaseDataSender
    {
        public DataSenderUDP(string hostName, int port, int maxDataSize, Watcher watcher) : base(hostName, port, maxDataSize, watcher){}

        public override Utils.ConnectionType Type => Utils.ConnectionType.UDP;

        public override void Send(DataFile dataFile)
        {
            var client = new UdpClient();
            client.Connect(Name, Port);

            try
            {
                SendData(dataFile.ToByte(), client);
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Watcher.NewErrorAdded();
            }
        }

        public override void SendBatched(IEnumerable<DataFile> dataFiles)
        {
            var client = new UdpClient();
            client.Connect(Name, Port);

            foreach (var dataFile in dataFiles)
            {
                try
                {
                    SendData(dataFile.ToByte(), client);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    Watcher.NewErrorAdded();
                }
            }
            
            client.Close();
        }

        private void SendData(byte[] data, UdpClient client)
        {
            client.Send(data, data.Length);

            Results.BytesSent = data.Length;
            Results.DataNumber = 1;

            var remoteEndpoint = new IPEndPoint(IPAddress.Any, Port);
            var recvBytes = client.Receive(ref remoteEndpoint);
            var recvData = Encoding.ASCII.GetString(recvBytes);

            Console.WriteLine(GetResultsMessage, Utils.GetMessageFromServer(recvData));
        }
    }
}