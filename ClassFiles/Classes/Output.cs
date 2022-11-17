using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ClassFiles.Classes
{
    public class Output
    {
        #region ----- Fields ------

        private string line;
        private int duplicatenumber;
        private List<int> lineNumber;
        private List<string> fileName;

        #endregion

        #region ------ Properties ------

        public string Line
        {
            get { return line; }
            set { line = value; }
        }
        public int Duplicatenumber
        {
            get { return duplicatenumber;}
            set { duplicatenumber = value;}
        }

        public List<int> LineNumber
        {
            get { return lineNumber; }
            set { lineNumber = value; }
        }

        public List<string> FileName
        {
            get { return fileName; }

            set { fileName = value; }
        }


        #endregion

        #region ------ Constructor ------

        public Output(string line, List<string> fileName, int duplicatenumber, List<int> lineNumbers)
        {
            this.LineNumber = lineNumbers;
            this.FileName = fileName;
            this.Line = line;
            this.Duplicatenumber = duplicatenumber;
        }

        #endregion
    }
}
