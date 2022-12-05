using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ClassFiles.Classes
{
    public class GetFileList
    {

        public List<string> GetFileNames(string fileending, string currentprpath)
        {

            
            List<string> fileList = new List<string>();


            try
            {
                fileList.AddRange(Directory.GetFiles(currentprpath, fileending, SearchOption.AllDirectories));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return fileList;

        }


    }
}
