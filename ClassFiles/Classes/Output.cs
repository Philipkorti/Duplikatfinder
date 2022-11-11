using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassFiles.Classes
{
    public class Output
    {
        #region ----- Fields ------

        private string text;

        private string fileName;

        public int duplicateCount;

        #endregion

        #region ------ Properties ------

        public string Text
        {
            get { return text; }

            set { text = value; }
        }

        public string FileName
        {
            get { return fileName; }

            set { fileName = value; }
        }

        public int DuplicateCount
        {
            get { return duplicateCount; }

            set { duplicateCount = value; }
        }

        #endregion

        #region ------ Constructor ------

        public Output(string text, string fileName, int duplicateCount)
        {
            this.Text = text;
            this.FileName = fileName;
            this.DuplicateCount = duplicateCount;
        }

        #endregion
    }
}
