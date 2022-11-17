using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
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
            Dictionary<string, Output> output = new Dictionary<string, Output>();

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
                    

                }
                linecount = 1;
                FileInfo fileInfo = new FileInfo(fileList[i]);
                files.Add(new FilesRead(fileInfo ,lines.ToList()));
            }
            doubleCheck(files, out output);
            Console.ReadLine();
        }
        static void doubleCheck(List<FilesRead> files, out Dictionary<string, Output> output)
        {
            output = new Dictionary<string, Output>();
            for (int i = 0; i < files.Count - 1; i++)
            {
                for (int j = 0; j < files[i].FileText.Count; j++)
                {

                    if (!output.ContainsKey(files[i].FileText[j].ToString()))
                    {
                        for (int k = 0; k < files[i + 1].FileText.Count; k++)
                        {
                            if (files[i].FileText[j] == files[i + 1].FileText[k])
                            {

                            }
                        }
                    }
                   
                }

            }
        }
    }
}
