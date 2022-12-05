using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using ClassFiles.Classes;
using System.ComponentModel;

namespace ClassFiles
{
    /// <summary>
    /// Represents the entry point
    /// </summary>
    internal class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// This is the entry point to this project
        /// </summary>
        /// <param name="args">The arguments for main</param>
        private static void Main(string[] args)
        {
            // temporary directory
            string currentProjectPath = AppDomain.CurrentDomain.BaseDirectory + @"\\..\\..";

            // a list of all files
            List<string> fileList = new List<string>();
            List<FilesRead> files = new List<FilesRead>();
            List<Text> lines = new List<Text>();

            // temporary string for the file endings
            string fileending = "*.cs";

            string ignorefile = CreateIgnoreFile(currentProjectPath);
            log.Info("IgnoreFile Created!");
            try
            {
                fileList.AddRange(Directory.GetFiles(currentProjectPath, fileending, SearchOption.AllDirectories));
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

                // writes all the lines that are not ignored into the text list
                List<string> text = IgnoreLines(fileList[i], ignorefile, out List<int> filelinecount);

                for (int j = 0; j < text.Count(); j++)
                {
                    try
                    {
                        lines.Add(new Text(text[j], filelinecount[j]));
                        Console.WriteLine("Line " + filelinecount[j] + ": " + text[j]);
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
        private static string CreateIgnoreFile(string currentDirectory)
        {
            string ignorefile = Path.Combine(currentDirectory, @"ignore.txt");
            string ignorefilePath = Path.GetFullPath(ignorefile);

            if (!File.Exists(ignorefile))
            {
                using (StreamWriter sw = File.CreateText(ignorefilePath))
                {
                    WriteIntoIgnoreFile(sw);
                }
            }

            return ignorefile;
        }

        /// <summary>
        /// This method writes these standard lines into the ignore file.
        /// </summary>
        /// <param name="sw">This is the stream writer.</param>
        private static void WriteIntoIgnoreFile(StreamWriter sw)
        {
            sw.WriteLine("// * = removes line if this symbol is the only thing in the line");
            sw.WriteLine("*{");
            sw.WriteLine("*}");

            sw.WriteLine("// removes the line if the line contains this");
            sw.WriteLine("using system");
            sw.WriteLine("using file");
        }

        /// <summary>
        /// This method reads the lines from the cs file and compares them to the lines in the ignore file
        /// </summary>
        /// <param name="file">the path of the code file</param>
        /// <param name="ignorefile">the path of the ignore file</param>
        /// <param name="filelinecount">a list of the line numbers</param>
        /// <returns>
        /// Returns a list with not ignored lines of code
        /// </returns>
        private static List<string> IgnoreLines(string file, string ignorefile,out List<int> filelinecount)
        {
            List<string> lines = new List<string>();
            List<string> ignorelines = new List<string>();
            ignorelines.AddRange(File.ReadAllLines(ignorefile));
            ignorelines.Remove(string.Empty);
            int count = 0;
            filelinecount = new List<int>();

            string fileline;
            bool goodline = true;
            using (StreamReader reader = new StreamReader(file))
            {
                while (!reader.EndOfStream)
                {
                    count++;
                    fileline = reader.ReadLine().TrimStart().TrimEnd().ToLower();

                    for (int i = 0; i < ignorelines.Count(); i++)
                    {
                        ignorelines[i] = ignorelines[i].ToLower();

                        if (ignorelines[i].StartsWith("*"))
                        {
                            if (fileline == ignorelines[i].TrimStart().TrimEnd().Remove(0, 1))
                            {
                                goodline = false;
                                break;
                            }
                        }
                        else
                        {
                            if (fileline.Contains(ignorelines[i]))
                            {
                                goodline = false;
                                break;
                            }
                        }
                    }

                    if (goodline)
                    {
                        if (fileline.Length != 0)
                        {
                            lines.Add(fileline);
                            filelinecount.Add(count);
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
