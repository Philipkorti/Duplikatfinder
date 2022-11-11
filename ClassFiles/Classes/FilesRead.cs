using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ClassFiles.Classes;

namespace ClassFiles
{
    public class FilesRead
    {
        #region ------ Fields ------

        private string fileName;

        private string filePath;

        private string fileTyp;

        private List<Text> fileText;

        #endregion

        #region ------ Properties-----

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }

        public string FileTyp
        {
            get { return fileTyp; }
            set { fileTyp = value;}
        }

        public List<Text> FileText
        {
            get { return fileText; }
            set { fileText = value; }
        }

        #endregion

        public FilesRead(string fileName, string filePath, string fileTyp, List<Text> filetext)
        {
            this.FileName = fileName;
            this.FilePath = filePath;
            this.FileTyp = fileTyp;
            this.FileText = filetext;
        }
    }
}
