using System;
using System.Collections.Generic;
using Tema1.Core.Utilities;
using Tema1.Core.Format;

namespace Tema1.Core.Senders
{
    public class DataSenderPool
    {
        private readonly new Dictionary<Utils.ConnectionType, BaseDataSender> _senders;
        private readonly Watcher _watcher;
        public TimeSpan TransferTimeForMessage => _watcher.ElapsedTimePerAction;
        public TimeSpan TotalTransferTime => _watcher.TotalElapsedTime;

        public int NumberOfErrors => _watcher.NumberOfErrors;

        public DataSenderPool(string serverName, int serverPort, int maxMessageSize)
        {
            _watcher = new Watcher();
            _senders = new Dictionary<Utils.ConnectionType, BaseDataSender>
            {

                { Utils.ConnectionType.TCP, new DataSenderTCP(serverName, serverPort, maxMessageSize, _watcher) },
                { Utils.ConnectionType.UDP, new DataSenderUDP(serverName, serverPort, maxMessageSize, _watcher) }
            };
        }

        public void Send(DataFile fileMessage, Utils.ConnectionType type)
        {
            if (_senders.ContainsKey(type))
                _watcher.MeasureElapsedTime(() => { _senders[type].Send(fileMessage); });
        }

        public void SendBatched(IEnumerable<DataFile> fileMessages, Utils.ConnectionType type)
        {
            if (_senders.ContainsKey(type))
                _watcher.MeasureElapsedTime(() => { _senders[type].SendBatched(fileMessages); });
        }
    }

}