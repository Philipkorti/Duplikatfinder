using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using ClassFiles.Classes;

namespace ClassFiles
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = @"D:\_TEMP_\";
            int linecount = 1;
            List<string> fileList = new List<string>();
            List<FilesRead> files = new List<FilesRead>();
            List<Text> lines = new List<Text>();
            try
            {
                fileList.AddRange(Directory.GetFiles(path, "*.txt*", SearchOption.AllDirectories));
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            

            for (int i = 0; i < fileList.Count; i++)
            {
                lines.Clear();
                using(StreamReader sr = new StreamReader(fileList[i]))
                {
                    while (!sr.EndOfStream)
                    {
                        try
                        {
                            lines.Add(new Text(sr.ReadLine(), linecount));
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        linecount++;
                    }
                    
                }
                linecount = 1;
                FileInfo fileInfo = new FileInfo(fileList[i]);
                files.Add(new FilesRead(fileInfo.Name, fileInfo.FullName, fileInfo.Extension, lines.ToList()));
            }

            Console.ReadLine();
        }
    }
}
