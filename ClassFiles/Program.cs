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
            string currentProjectPath = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\";
            List<string> fileList = new List<string>();
            List<FilesRead> files = new List<FilesRead>();
            List<Text> lines = new List<Text>();
            _ = new List<string>();

            string ignorefile = IgnoreFileCreate(currentProjectPath);

            try
            {
                fileList.AddRange(Directory.GetFiles(currentProjectPath, "*.cs", SearchOption.AllDirectories));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            

            for (int i = 0; i < fileList.Count; i++)
            {
                Console.WriteLine();
                Console.WriteLine("File: " + fileList[i]);
                Console.WriteLine();

                lines.Clear();

                List<string> text = Lukas(fileList[i], ignorefile, out List<int> linecount);

                for (int j = 0; j < text.Count(); j++)
                {
                    try
                    {
                        lines.Add(new Text(text[j], linecount[j]));
                        Console.WriteLine("Line " + linecount[j] + ": " + text[j]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                FileInfo fileInfo = new FileInfo(fileList[i]);
                files.Add(new FilesRead(fileInfo, lines.ToList()));
                
            }
            Console.ReadLine();
        }

        static string IgnoreFileCreate(string sCurrentDirectory)
        {
            
            string ignorefile = Path.Combine(sCurrentDirectory, @"..\..\..\ignore.txt");
            string ignorefilePath = Path.GetFullPath(ignorefile);

            if (!File.Exists(ignorefile))
            {
                using (StreamWriter sw = File.CreateText(ignorefilePath))
                {
                    sw.WriteLine("// * = removes line if this symbol is the only thing in the line");
                    sw.WriteLine("*{");
                    sw.WriteLine("*}");

                    sw.WriteLine("// removes the line if the line contains this");
                    sw.WriteLine("using system");
                    sw.WriteLine("using file");
                }
            }
            return ignorefile;
        }

        static List<string> Lukas(string csFile, string ignorefile, out List<int> linecount)
        {
            List<string> lines = new List<string>();
            List<string> ignorelines = new List<string>();
            ignorelines.AddRange(File.ReadAllLines(ignorefile));
            ignorelines.Remove("");
            int count = 0;
            linecount = new List<int>();

            string codeline;
            bool goodline = true;
            using (StreamReader reader = new StreamReader(csFile))
            {
                while (!reader.EndOfStream)
                {
                    count++;
                    codeline = reader.ReadLine().TrimStart().TrimEnd().ToLower();

                    for (int i = 0; i < ignorelines.Count(); i++)
                    {
                        ignorelines[i] = ignorelines[i].ToLower();

                        if (ignorelines[i].StartsWith("*"))
                        {
                            if (codeline == ignorelines[i].TrimStart().TrimEnd().Remove(0, 1))
                            {
                                goodline = false;
                                break;
                            }
                        }
                        else
                        {
                            if (codeline.Contains(ignorelines[i]))
                            {
                                goodline = false;
                                break;
                            }
                        }
                    }
                    if (goodline)
                    {
                        if (codeline.Length != 0)
                        {
                            lines.Add(codeline);
                            linecount.Add(count);
                        }
                    }
                    goodline = true;
                }
            }
            return lines;
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
