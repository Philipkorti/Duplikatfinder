using ClassFiles.Classes;
using System;
using System.Collections.Generic;
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
