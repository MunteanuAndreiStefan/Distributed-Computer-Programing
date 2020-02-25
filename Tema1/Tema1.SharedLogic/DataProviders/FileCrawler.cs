using System;
using System.Collections.Generic;
using System.IO;
using Tema1.Core.Format;
using Tema1.Core.Utilities;

namespace Tema1.Core.Database
{
    public class FileCrawler//FileExplorer?
    {
        public IEnumerable<DataFile> GetDataFiles()
        {
            var dataFiles = new List<DataFile>();
            var files = Directory.GetFiles(Utils.Path);

            foreach (var file in files)
            {
                try
                {
                    var fileName = Path.GetFileName(file);
                    var format = fileName.Split('.')[1];
                    var name = fileName.Split('.')[0];

                    dataFiles.Add(new DataFile
                    {
                        Name = name,
                        Format = format,
                        Data = File.ReadAllBytesAsync($"{Utils.Path}{name}.{format}").Result
                    });
                }
                catch (Exception e)
                {
                    Console.WriteLine($"\n{e}");
                }
            }
            return dataFiles;
        }
    }
}