using ClassFiles.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassFiles.Services
{
    public class DuplicateCheck
    {
        /// <summary>
        /// This method checks if there is a duplicate line
        /// </summary>
        /// <param name="files">A list of files</param>
        /// <param name="output">The output</param>
        public static void DoubleCheck(List<FilesRead> files, out Dictionary<string, Output> output)
        {
            List<FileInfo> file = new List<FileInfo>();
            List<int> linenumber = new List<int>();
            output = new Dictionary<string, Output>();
            for (int i = 0; i < files.Count - 1; i++)
            {
                for (int y = 0; y < files[i].FileText.Count; y++)
                {
                    if (!output.ContainsKey(files[i].FileText[y].FileText))
                    {
                        for (int k = 0; k < files[i + 1].FileText.Count; k++)
                        {
                            if (files[i].FileText[y].FileText == files[i + 1].FileText[k].FileText)
                            {
                                if (!output.ContainsKey(files[i].FileText[y].FileText))
                                {
                                    string path = files[i].FileInfo.DirectoryName;
                                    output.Add(files[i].FileText[y].FileText, new Output(new List<FileInfo>() { new FileInfo(path) }, 1, new List<int>() { files[i].FileText[y].LineNumber }));
                                }
                                else
                                {
                                    output[files[i].FileText[y].FileText].GetUPDuplicate();
                                    output[files[i].FileText[y].FileText].FileName.Add(files[i].FileInfo);
                                    output[files[i].FileText[y].FileText].LineNumber.Add(files[i].FileText[y].LineNumber);
                                }
                                
                            }
                        }
                    }
                    else
                    {
                        output[files[i].FileText[y].FileText].GetUPDuplicate();
                        output[files[i].FileText[y].FileText].FileName.Add(files[i].FileInfo);
                        output[files[i].FileText[y].FileText].LineNumber.Add(files[i].FileText[y].LineNumber);
                    }
                }
            }
        }
    }
}
