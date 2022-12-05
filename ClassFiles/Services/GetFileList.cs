using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Web.UI;

namespace ClassFiles.Classes
{
    public class GetFileList
    {
        /// <summary>
        /// This method lod all files with the same file endigs.
        /// </summary>
        /// <param name="currentprpath"></param>
        /// <param name="fileending"></param>
        /// <returns></returns>
        public static void GetFileNames(string currentprpath, string fileending, out List<string> fileList)
        {

            // List os filepaths
            fileList = new List<string>();

            // Get all the filepaths and save it into the fileList list
            foreach (string file in Directory.GetFiles(currentprpath, fileending, SearchOption.AllDirectories))
            {
                try
                {
                    fileList.Add(file);
                }
                catch (Exception ex)
                {
                    Program.log.Error($"The File {file} not load!");
                }
            }

        }


    }
}
