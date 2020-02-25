using System;
using System.Linq;
using Tema1.Core.Database;
using Tema1.Core.Senders;
using Tema1.Core.Utilities;

namespace Tema1.Client
{
    public class ClientInit
    {
        private const int messageSize = ushort.MaxValue;

        public static void Main()
        {
            var dataSender = new DataSenderPool(Utils.ConnectionData.ServerHost, Utils.STARTUP == Utils.ConnectionType.UDP ? Utils.ConnectionData.PortUDP : Utils.ConnectionData.PortTCP, messageSize);
            var dataFiles = new FileCrawler().GetDataFiles().ToList();
            dataSender.SendBatched(dataFiles, Utils.STARTUP);
            Console.WriteLine(Utils.GetClientMessage(dataSender.TotalTransferTime.TotalSeconds));
            Console.WriteLine(Utils.GetNumberOfErrors(dataSender.NumberOfErrors));
            Console.Read();
        }
    }
}
