using System;
using System.Collections;
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
        private int totalLines = 0;
        private int totalErrors = 0;


        private Label Errorlabel { get; set; }
        private RichTextBox errorTextBox { get; set; }

        public ErrorCheck(string filepath, Label errors, RichTextBox box)
        {
            FilePath = filepath;
            errorTextBox = box;
            Errorlabel = errors;
        }


        public void startScan()
        {
            var fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            var sr = new StreamReader(fs, Encoding.UTF8);

            //string line = String.Empty;

            string[] lines = File.ReadAllLines(FilePath, Encoding.UTF8);

            foreach (string line in lines)
            {
                totalLines++;
                if (line.Contains("System"))
                {
                    checkSout(line);
                }
            }

            //while ((line = sr.ReadLine()) != null)
            //{
            //    totalLines++;
            //    if (line.Contains("System"))
            //    {
            //        checkSout(line);
            //    }
            //}
        }


        private void addTotalErrors()
        {
            totalErrors++;
            Errorlabel.Text = totalErrors.ToString();
        }

        private Boolean checkSout(string line)
        {
            string sout = "System.out.println";
            string t = "System.out.println";
            int i = 0;
            while (line[i] != 'S') { i++; }

            for (int j = 0; j < sout.Length; j++)
            {
                if (t[j] != line[i + j])
                {
                    errorTextBox.Text += "error in Line " + totalLines + " : missing " + sout[j] + " in system.out.println(";
                    addTotalErrors();
                    return false;
                }
            }

            Stack temp = new Stack();
            while (i < line.Length)
            {
                if (line[i] == '(') { temp.Push(line[i]); }
                if (line[i] == '"') { temp.Push(line[i]); }

                if (line[i] == ')') { temp.Pop(); }
                if (line[i] == '"') { temp.Pop(); }
                i++;
            }

            if (temp.Count > 0)
            {
                errorTextBox.Text += "error in Line " + totalLines + " : missing parenthesses in system.out.println(";
                addTotalErrors();
                return false;
            }

            if (line[line.Length - 2] != ';')
            {
                errorTextBox.Text += "error in Line " + totalLines + " : missing ; ";
                addTotalErrors();
                return true;
            }

            return true;
        }

        private Boolean checkStringWriter(string line)
        {
            string sw = "StringWriter";
            string newString = "new";
            int i = 0;
            while (line[i] != 'S') { i++; }

            for (int j = 0; j < sw.Length; j++)
            {
                if (line[i + j] != sw[j])
                {
                    return false;
                }
            }
            i += sw.Length;
            if (!line.Contains('='))
            {
                return false;
            }

            while (line[i] != 'n') { i++; }

            for (int j = 0; j < newString.Length; j++)
            {
                if (line[i + j] != newString[j])
                {
                    return false;
                }
            }
            i += newString.Length;
            while (line[i] != 'S') { i++; }

            for (int j = 0; j < sw.Length; j++)
            {
                if (line[i + j] != sw[j])
                {
                    return false;
                }
            }
            i += sw.Length;
            if (!line.Contains(';'))
            {
                return false;
            }
            return true;
        }
    }

}
