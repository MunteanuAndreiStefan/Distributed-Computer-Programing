using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Tema1.Core.Format;
using Tema1.Core.Utilities;

namespace Tema1.Core.Senders
{
    public class DataSenderTCP : BaseDataSender
    {
        public DataSenderTCP(string name, int port, int maxMessageSize) : base(name, port, maxMessageSize) { }

        public override Utils.ConnectionType Type => Utils.ConnectionType.TCP;

        public override void Send(DataFile dataFile)
        {
            var client = new TcpClient(this.Name, this.Port);
            var stream = client.GetStream();
            var byteArray = dataFile.ToByte();
            var dataSize = BitConverter.GetBytes(byteArray.Length);

            stream.Write(dataSize, 0, dataSize.Length);
            byteArray.Split(MaxDataSize).ToList().ForEach(package => SendPackage(stream, package));
            stream.Close();
            client.Close();

            Console.WriteLine(GetResultsMessage);
        }

        public override void SendBatched(IEnumerable<DataFile> dataFiles)
        {
            var client = new TcpClient(this.Name, this.Port);
            var stream = client.GetStream();

            foreach (var dataFile in dataFiles)
            {
                var byteArray = dataFile.ToByte();
                var dataSize = BitConverter.GetBytes(byteArray.Length);

                stream.Write(dataSize, 0, dataSize.Length);
                byteArray.Split(this.MaxDataSize).ToList().ForEach(package => SendPackage(stream, package));
                System.Threading.Thread.Sleep(50);
            }
            stream.Close();
            client.Close();

            Console.WriteLine(GetResultsMessage);
        }

        private void SendPackage(NetworkStream stream, byte[] package)
        {
            stream.Write(package, 0, package.Length);
            Results.DataNumber++;
            Results.BytesSent += package.Length;
        }
    }
}