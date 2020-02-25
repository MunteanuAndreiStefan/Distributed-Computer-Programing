using Tema1.Core.Utilities;
using Tema1.Core.Format;

namespace Tema1.Core.Senders
{
    public abstract class BaseDataSender
    {
        public virtual void Send(DataFile dataFile) { }
        public virtual void SendBatched(System.Collections.Generic.IEnumerable<DataFile> dataFiles) { }
        protected string Name { get; }
        protected int Port { get; }
        protected int MaxDataSize { get; }
        public DataFileResult Results { get; set; }
        public virtual Utils.ConnectionType Type => Utils.ConnectionType.TCP;
        protected BaseDataSender(string hostName, int port, int maxDataSize)
        {
            Name = hostName;
            Port = port;
            MaxDataSize = maxDataSize;
            Results = new DataFileResult();
        }

        public virtual string GetResultsMessage =>
            $"\n{Type.ToString()}\n{Utils.GetBytesSentNumber(Results.BytesSent)}\n{Utils.GetMessagesSentNumber(Results.DataNumber)}";
    }
}