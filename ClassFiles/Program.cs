﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using ClassFiles.Classes;
using System.ComponentModel;
using ClassFiles.Services;

namespace ClassFiles
{
    /// <summary>
    /// Represents the entry point
    /// </summary>
    internal class Program
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        /// <summary>
        /// This is the entry point to this project
        /// </summary>
        /// <param name="args">The arguments for main</param>
        private static void Main(string[] args)
        {
            // temporary directory
            string currentProjectPath = AppDomain.CurrentDomain.BaseDirectory + @"\\..\\..";

            // temporary string for the file endings
            string fileending = "*.cs";

            string ignorefile = IgnoreFile.CreateIgnoreFile(currentProjectPath);

            //Get all files in the directory
            GetFileList.GetFileNames(currentProjectPath, fileending, out List<string> fileList);
            FilesAdd.Files(fileList, ignorefile, out List<FilesRead> files);

            Console.ReadLine();
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
                                //List<int> a = new List<int>();
                                //a.Add(files[i].FileText[j].LineNumber);
                                Console.WriteLine("Linenumber (left): {0}", files[i].FileText[j].LineNumber);
                                Console.WriteLine("Linenumber (right): {0}", files[i + 1].FileText[k].LineNumber);
                            }
                        }
                    }
                }
            }
        }
    }
}
