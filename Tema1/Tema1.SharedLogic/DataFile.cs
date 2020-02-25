using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Tema1.Core.Format
{
    [Serializable]
    public class DataFile
    {
        public byte[] Data;
        public string Name;
        public string Format;

        public byte[] ToByte()
        {
            var binaryFormatter = new BinaryFormatter();
            var stream = new MemoryStream();
            binaryFormatter.Serialize(stream, this);
            return stream.ToArray();
        }
    }

    public class DataFileResult
    {
        public int BytesSent { get; set; }
        public int DataNumber { get; set; }

        public DataFileResult(int bytesSent = 0, int dataNumber = 0)
        {
            BytesSent = bytesSent;
            DataNumber = dataNumber;
        }
    }
}