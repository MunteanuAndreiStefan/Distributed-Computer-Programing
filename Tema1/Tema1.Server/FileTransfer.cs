using System;
using System.IO;
using Tema1.Core.Format;
using Tema1.Core.Utilities;

namespace Tema1.Server
{
    public class FileTransfer
    {
        public void TrasferRecvDataInfo(string clientGuid, DataFile dataFile)
        {
            try
            {
                File.WriteAllBytes(Utils.GetTestPathDir(dataFile), dataFile.Data);
                Console.WriteLine(Utils.GetMessageSaveStatus(dataFile, clientGuid));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}