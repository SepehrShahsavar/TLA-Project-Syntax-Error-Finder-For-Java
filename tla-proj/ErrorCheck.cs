using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tla_proj
{
    public class ErrorCheck
    {
        private string FilePath { get; set; }
        private int Line = 0;
        private int totalErrors = 0;


        private Label Errorlabel { get; set; }
        private RichTextBox errorTextBox { get; set; } 

        public ErrorCheck(string filepath,Label errors , RichTextBox box)
        {
            FilePath = filepath;
            errorTextBox = box;
            Errorlabel = errors;
        }


        public void startScan()
        {
             var fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
             var sr = new StreamReader(fs, Encoding.UTF8);

            string line = String.Empty;

            while ((line = sr.ReadLine()) != null)
            {
                addTotalErrors();
                Line++;
                errorTextBox.Text += line;
                errorTextBox.Text += "\n";

            }
        }


        void addTotalErrors()
        {
            totalErrors++;
            Errorlabel.Text = totalErrors.ToString();
        }
    }
}
