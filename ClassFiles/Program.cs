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
    /// <summary>
    /// Represents the entry point
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// This is the Main
        /// </summary>
        /// <param name="args">Args of Main</param>
        private static void Main(string[] args)
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
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            for (int i = 0; i < fileList.Count; i++)
            {
                Console.WriteLine();
                Console.WriteLine("File: " + fileList[i]);
                Console.WriteLine();

                lines.Clear();

                List<string> text = IgnoreCode(fileList[i], ignorefile, out List<int> linecount);

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

        /// <summary>
        /// This method creates the ignore file and writes some lines into it if it does not exist
        /// </summary>
        /// <param name="currentDirectory">This is the current directory of the project</param>
        /// <returns>
        /// Returns the ignore file string
        /// </returns>
        private static string IgnoreFileCreate(string currentDirectory)
        {
            string ignorefile = Path.Combine(currentDirectory, @"ignore.txt");
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

        /// <summary>
        /// This method reads the lines from the cs file and compares them to the lines in the ignore file
        /// </summary>
        /// <param name="codeFile">the path of the code file</param>
        /// <param name="ignorefile">the path of the ignore file</param>
        /// <param name="linecount">a list of the line numbers</param>
        /// <returns>
        /// Returns a list with not ignored lines of code
        /// </returns>
        private static List<string> IgnoreCode(string codeFile, string ignorefile, out List<int> linecount)
        {
            List<string> lines = new List<string>();
            List<string> ignorelines = new List<string>();
            ignorelines.AddRange(File.ReadAllLines(ignorefile));
            ignorelines.Remove(string.Empty);
            int count = 0;
            linecount = new List<int>();

            string codeline;
            bool goodline = true;
            using (StreamReader reader = new StreamReader(codeFile))
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

        /// <summary>
        /// This method checks if there is a duplicate line
        /// </summary>
        /// <param name="files">A list of files</param>
        /// <param name="output">The output</param>
        private static void DoubleCheck(List<FilesRead> files, out Dictionary<string, Output> output)
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
                                // missing
                            }
                        }
                    }
                }
            }
        }
    }
}
