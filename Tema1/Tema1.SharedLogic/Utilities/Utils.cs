using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Tema1.Core.Format;

namespace Tema1.Core.Utilities
{
    public static class Utils
    {
        public static ConnectionType STARTUP => ConnectionType.UDP;

        #region Extensions
        public static object ByteArrayToObject(this byte[] arrBytes)
        {
            try
            {
                var memStream = new MemoryStream();
                var binForm = new BinaryFormatter();

                memStream.Write(arrBytes, 0, arrBytes.Length);
                memStream.Seek(0, SeekOrigin.Begin);

                var deserializedObject = binForm.Deserialize(memStream);

                return deserializedObject;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public static IEnumerable<byte[]> Split(this byte[] value, int bufferLength)
        {
            var countOfArray = value.Length / bufferLength;

            if (value.Length % bufferLength > 0)
            {
                countOfArray++;
            }

            for (var i = 0; i < countOfArray; i++)
            {
                yield return value.Skip(i * bufferLength).Take(bufferLength).ToArray();
            }
        }
        #endregion

        #region Configuration

        private static (string, int , int) _connectionData = ("localhost", 8881, 9991);
        public static (string ServerHost, int PortUDP, int PortTCP) ConnectionData => _connectionData;

        public const int BufferSize = 204800; // 200MB
        public const int DataBytesNumber = 4;
        public enum ConnectionType
        {
            TCP,
            UDP
        }
        #endregion

        #region Dialogs
        public static string Path => @"C:\Users\amunteanu\Desktop\Tema1\Test\";
        public static string GetTestPathDir(DataFile file) => @"C:\Users\amunteanu\Desktop\Tema1\Test\" + $"/{file.Name}.{file.Format}";
        public static string GetMessageSaveStatus(DataFile file, string client) => $"{file.Name}.{file.Format} from {client} was saved.";
        public static string GetMessageReciving => "Receiving messages, wait for finish";
        public static string GetMessageStartedTCP => "TCP host: Waiting for clients!";
        public static string GetMessageStartedUDP => "UDP host: Waiting for clients!";

        public static string GetDataRecvPackageNumberTCP(int number) => $"Number of packages received: {number}";
        public static string GetDataReadBytesNumberTCP(int number) => $"Number of bytes read: {number}";

        public static string GetMessagesSentNumber(int number) => $"Number of messages sent: {number}";
        public static string GetBytesSentNumber(int number) => $"Bytes sent: {number}";
        public static string GetClientMessage(double number) => $"\nTotal Transfer time: {number}\n";
        public static string GetMessageFromServer(string msg) => $"\nMessage from server: {msg}\n";

        public static string GetNumberOfErrors(int number) => $"\nNumber of errors: {number}\n";
        public static string ETAG => "File received.";
        #endregion
    }
}
