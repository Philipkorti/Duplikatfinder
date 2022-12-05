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

        public static List<string> GetFileNames(string currentprpath, string fileending)
        {

            // a list of all files
            List<string> fileList = new List<string>();
            foreach (var file in Directory.GetFiles(currentprpath, fileending, SearchOption.AllDirectories))
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
            return fileList;

        }


    }
}
