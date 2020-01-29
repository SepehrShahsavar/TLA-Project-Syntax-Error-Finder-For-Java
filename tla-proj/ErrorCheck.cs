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
        private string StringWriterVar = "";
        private string FileInputVar = "";
        private string FileOutputVar = "destFile";
        private string ByteArrayVar = "Array";
        private string ExceptionVar = "ex";

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

                if (line.Contains("StringWriter"))
                {
                    checkStringWriter(line);
                }

                if (line.Contains("FileInputStream"))
                {
                    checkFileStreams(line, "FileInputStream");
                }

                if (line.Contains("FileOutputStream"))
                {
                    checkFileStreams(line, "FileOutputStream");
                }

                if (line.Contains("write") && !FileOutputVar.Equals(""))
                {
                    checkDesFileWrite(line);
                }

               
            }
            fs.Close();
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
                    errorTextBox.Text += "error in Line " + totalLines + " : missing " + sout[j] + " in system.out.println(\n";
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
                errorTextBox.Text += "error in Line " + totalLines + " : missing parenthesses in system.out.println(\n";
                addTotalErrors();
                return false;
            }

            if (line[line.Length - 1] != ';')
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
                    errorTextBox.Text += "error in Line " + totalLines;
                    addTotalErrors();
                    return false;
                }
            }
            i += sw.Length;
            if (!line.Contains('='))
            {
                errorTextBox.Text += "error in Line " + totalLines;
                addTotalErrors();
                return false;
            }

            while (line[i] != '=') 
            {
                StringWriterVar += line[i];
                i++; 
            }

            StringWriterVar = StringWriterVar.Trim();

            while (line[i] != 'n') { i++; }

            for (int j = 0; j < newString.Length; j++)
            {
                if (line[i + j] != newString[j])
                {
                    errorTextBox.Text += "error in Line " + totalLines;
                    addTotalErrors();
                    return false;
                }
            }
            i += newString.Length;
            while (line[i] != 'S') { i++; }

            for (int j = 0; j < sw.Length; j++)
            {
                if (line[i + j] != sw[j])
                {
                    errorTextBox.Text += "error in Line " + totalLines;
                    addTotalErrors();
                    return false;
                }
            }
            i += sw.Length;

            if (line[line.Length - 1] != ';')
            {
                errorTextBox.Text += "error in Line " + totalLines + " : missing ; ";
                addTotalErrors();
                return false;
            }

            return true;
        }

        private Boolean checkFileStreams(string line, string error)
        {
            string fi = error;
            string nString = "new";
            int i = 0;
            while (line[i] != 'F') { i++; }

            for (int j = 0; j < fi.Length; j++)
            {
                if (line[i + j] != fi[j])
                {
                    errorTextBox.Text += "error in Line " + totalLines;
                    addTotalErrors();
                    return false;
                }
            }
            i += fi.Length;
            if (!line.Contains('='))
            {
                errorTextBox.Text += "error in Line " + totalLines;
                addTotalErrors();
                return false;
            }

            while (line[i] != '=') { i++; }

            while (line[i] != 'n') { i++; }

            for (int j = 0; j < nString.Length; j++)
            {
                if (line[i + j] != nString[j])
                {
                    errorTextBox.Text += "error in Line " + totalLines;
                    addTotalErrors();
                    return false;
                }
            }
            i += nString.Length;
            while (line[i] != 'F') { i++; }

            for (int j = 0; j < fi.Length; j++)
            {
                if (line[i + j] != fi[j])
                {
                    errorTextBox.Text += "error in Line " + totalLines;
                    addTotalErrors();
                    return false;
                }
            }
            i += fi.Length;

            Stack temp = new Stack();
            int counter = 0;
            while (i < line.Length)
            {
                if (line[i] == '(') { temp.Push(line[i]); }
                if (line[i] == '"') { counter++; }

                if (line[i] == ')') { temp.Pop(); }
                i++;
            }

            if (temp.Count > 0 || counter % 2 != 0)
            {
                errorTextBox.Text += "error in Line " + totalLines + " : missing parenthesses or qoution in FileStreamInput\n";
                addTotalErrors();
                return false;
            }
            if (line[line.Length - 1] != ';')
            {
                errorTextBox.Text += "error in Line " + totalLines + " : missing ; \n";
                addTotalErrors();
                return false;
            }

            return true;

        }

        //private bool checkDesFileWrite(string line)
        //{
        //    int i = 0;
        //    string trimed = line.Trim();
        //    for (int j = 0; j < FileOutputVar.Length; j++)
        //    {
        //        if (FileOutputVar[j] != trimed[i + j])
        //        {
        //            errorTextBox.Text += "error in Line " + totalLines + " : var" + trimed.Substring(i , FileOutputVar.Length) +" not found as FileOutPutStream instance \n";
        //            addTotalErrors();
        //            return false;
        //        }
        //    }

        //    i += FileOutputVar.Length+1;
        //    string sub = trimed.Substring(i, i + 5);
        //    if (!sub.Contains("write"))
        //    {
        //        errorTextBox.Text += "error in Line " + totalLines + " : method not found in FileOutPut Stream\n";
        //        addTotalErrors();
        //        return false;
        //    }
        //    Stack stack = new Stack();
        //    string temp = "";
        //    while (i < trimed.Length)
        //    {
        //        if (trimed[i] == '(')
        //        {
        //            stack.Push('(');
        //        }
                
        //        if (trimed[i] == ')')
        //        {
        //            stack.Pop();
        //        }
                
        //        if (trimed[i] != '(' || trimed[i] != ')')
        //        {
        //            temp += trimed[i];
        //        }
        //        i++;
        //    }
        //    if (stack.Count > 0)
        //    {
        //        errorTextBox.Text += "error in Line " + totalLines + " : missing parenthesses in write\n";
        //        addTotalErrors();
        //        return false;
        //    }

        //    if (!temp.Equals(ByteArrayVar))
        //    {
        //        errorTextBox.Text += "error in Line " + totalLines + " var " + temp+" doesn't declared \n";
        //        addTotalErrors();
        //        return false;
        //    }

        //    if (trimed[trimed.Length - 1] != ';')
        //    {
        //        errorTextBox.Text += "error in Line " + totalLines + " : missing ; \n";
        //        addTotalErrors();
        //        return false;
        //    }
        //    return true;
        //}

        private bool checkStackTrace(string line)
        {
            int i = 0;
            string trimmed = line.Trim();
            if (!ExceptionVar.Equals(trimmed.Substring(0 , ExceptionVar.Length)))
            {
                errorTextBox.Text += "error in Line " + totalLines + " var doesn't declared \n";
                addTotalErrors();
                return false;
            }
            
            i += ExceptionVar.Length + 1;
            Stack stack = new Stack();
            string temp = "";
            while (i < trimmed.Length)
            {
                if (trimmed[i] == '(')
                {
                    stack.Push('(');
                }

                if (trimmed[i] == ')')
                {
                    stack.Pop();
                }
            }

            if (stack.Count > 0)
            {
                errorTextBox.Text += "error in Line " + totalLines + " : missing parenthesses in write\n";
                addTotalErrors();
                return false;
            }

            if (trimmed[trimmed.Length - 1] != ';')
            {
                errorTextBox.Text += "error in Line " + totalLines + " : missing ; \n";
                addTotalErrors();
                return false;
            }

            return true;
        }
    }

}
