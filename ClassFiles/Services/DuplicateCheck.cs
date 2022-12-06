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
                foreach (var t in files[i].FileText)
                {
                    if (!output.ContainsKey(t.ToString()))
                    {
                        for (int k = 0; k < files[i + 1].FileText.Count; k++)
                        {
                            if (t == files[i + 1].FileText[k])
                            {

                            }
                        }
                    }
                }
            }
        }
    }
}
