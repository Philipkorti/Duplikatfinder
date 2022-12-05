using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassFiles.Classes;

namespace ClassFiles.Services
{
    public class FilesAdd
    {
        public static void Files(List<string> fileList, string ignorefile, out List<FilesRead> files)
        {
            files = new List<FilesRead>();
            for (int i = 0; i < fileList.Count; i++)
            {
                FileInfo fileInfo = new FileInfo(fileList[i]);
                // writes all the lines that are not ignored into the text list
                List<string> text = IgnoreFile.IgnoreLines(fileList[i], ignorefile, out List<int> filelinecount);
                LinesAdd.Lines(filelinecount, text, out List<Text> lines);
                files.Add(new FilesRead(fileInfo, lines.ToList()));
                lines.Clear();
            }
        }
    }
}
