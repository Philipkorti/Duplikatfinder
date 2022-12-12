using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using ClassFiles.Classes;
using System.ComponentModel;
using System.Runtime.InteropServices;
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

            DuplicateCheck.DoubleCheck(files, out Dictionary<string, Output> output);
            foreach (KeyValuePair<string, Output> VARIABLE in output)
            {
                Console.WriteLine("Line: " + VARIABLE.Key);
                Console.WriteLine("Duplicate: "+VARIABLE.Value.Duplicatenumber);
                for (int i = 0; i < VARIABLE.Value.FileName.Count; i++)
                {
                    Console.WriteLine("FileName: "+ VARIABLE.Value.FileName[i].Name);
                    Console.WriteLine("Linenumber: "+VARIABLE.Value.LineNumber[i]);
                }

                Console.WriteLine("---------------------");
                Console.WriteLine();
            }
            Console.ReadLine();
        }
    }
}
