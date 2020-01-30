using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private string FileOutputVar = "";
        private string ByteArrayVar = "";
        private string ExceptionVar = "";

        private Label Errorlabel { get; set; }
        public Label NoErr { get; private set; }

        public ErrorCheck(string filepath, Label errors, Label noErr)
        {
            FilePath = filepath;
            Errorlabel = errors;
            NoErr = noErr;
        }


        public void startScan()
        {
            var fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read);
            var sr = new StreamReader(fs, Encoding.UTF8);

            //string line = String.Empty;

            List<string> lines = File.ReadAllLines(FilePath, Encoding.UTF8).ToList<string>();
            lines.ForEach(x =>
            {
                x = x.Trim();
                char[] tmp = x.ToCharArray();
                ExceptionVar = "ex";
                x = String.Join("", tmp);
            });
            char[] space = { ' ' };
            
            foreach (string line in lines)
            {
                bool[] isError = new bool[11];
                if (line.Contains("}") || line.Contains("{"))
                {
                    continue;
                }
                isError[0] = !checkSout(line.Trim());
                isError[1] = !checkFileClose(line.Trim(), FileInputVar);
                isError[2] = !checkReadWrite(FileOutputVar, "write", ByteArrayVar, line.Trim());
                isError[3] = !checkFileClose(line.Trim(), FileOutputVar);
                isError[4] = !checkStringWriter(line.Trim());
                isError[5] = !checkString(line.Trim());
                isError[6] = !checkByteArray(line.Trim());
                isError[7] = !checkReadWrite(FileInputVar, "read", ByteArrayVar, line.Trim());
                isError[8] = !checkFileStreams(line.Trim(), "FileOutputStream");
                isError[9] = !checkFileStreams(line.Trim(), "FileInputStream");
                isError[10] = !checkStackTrace(line.Trim());
                int counter = 0;
                foreach (bool iserror in isError)
                {
                    if (iserror)
                    {
                        counter++;
                    }
                }

                if (counter == 11) { addTotalErrors(); }
                
            }
            if (Errorlabel.Text == '0'.ToString())
            {
                NoErr.Visible = true;
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
            while (line[i] != 'S') 
            {
                i++;
                if (i >= line.Length) { return false; }
                
            }

            for (int j = 0; j < sout.Length; j++)
            {
                if (t[j] != line[i + j])
                {
                    return false;
                }
            }

            Stack temp = new Stack();
            int counter = 0;
            while (i < line.Length)
            {
                if (line[i] == '(') { temp.Push(line[i]); }
                if (line[i] == '"') { counter++; }

                if (line[i] == ')') { temp.Pop(); }
                i++;
            }

            if (temp.Count > 0 || counter % 2 == 1)
            {
                return false;
            }

            if (line[line.Length - 1] != ';')
            {
                return false;
            }

            return true;
        }

        private Boolean checkStringWriter(string line)
        {
            line = line.Trim();
            line = Regex.Replace(line, @"\s+", "");
            string sw = "StringWriter";
            string newString = "new";
            int i = 0;
            while (line[i] != 'S')
            {
                i++;
                if (i >= line.Length) { return false; }
                
            }
            for (int j = 0; j < sw.Length; j++)
            {
                if (line[i + j] != sw[j])
                {
                    return false;
                }

            }

            i += sw.Length;
            while (line[i] != '=')
            {
                if (i > line.Length)
                {
                    return false;
                }
                StringWriterVar += line[i];                                                   //make the variable
                i++;
            }

            i++;
            for (int j = 0; j < newString.Length; j++)
            {
                if (line[i + j] != newString[j])
                {
                    return false;
                }
            }

            i += newString.Length;
            for (int j = 0; j < sw.Length; j++)
            {
                if (line[i] != sw[j])
                {
                    return false;
                }
                i++;
            }

            if (line[line.Length - 1] != ';')
            {
                return false;
            }

            return true;
        }

        private Boolean checkFileStreams(string line, string error)
        {
            string fi = error;
            string nString = "new";
            int i = 0;

            for (int j = 0; j < fi.Length; j++)
            {
                if (line[i + j] != fi[j])
                {
                    return false;
                }
            }
            i += fi.Length;
            if (!line.Contains('='))
            {
               return false;
            }

            while (line[i] != '=')
            {
                if (error.Contains("Input"))
                {
                    FileInputVar += line[i];
                }
                else
                {
                    FileOutputVar += line[i];
                }

                i++;
            }

            while (line[i] != 'n') { i++; }

            for (int j = 0; j < nString.Length; j++)
            {
                if (line[i + j] != nString[j])
                {
                    return false;
                }
            }
            i += nString.Length;
            while (line[i] != 'F') { i++; }

            for (int j = 0; j < fi.Length; j++)
            {
                if (line[i + j] != fi[j])
                {
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

            if (temp.Count > 0 || counter % 2 == 1)
            {
                return false;
            }
            if (line[line.Length - 1] != ';')
            {
                return false;
            }

            return true;

        }

        private Boolean checkReadWrite(string varFile, string readOrWrite, string varArray, string line)
        {
            int i = 0;
            char[] s = { ' ' };
            varArray = varArray.Trim(s);
            varFile = varFile.Trim();
            for (int j = 0; j < varFile.Length; j++)
            {
                if (line[j] != varFile[j])
                {
                    return false;
                }
                i = j;
            }

            i++;
            if (line[i] != '.')
            {
                return false;
            }


            i++;


            for (int j = 0; j < readOrWrite.Length; j++)
            {
                if (line[i] != readOrWrite[j])
                {
                    return false;
                }
                i++;
            }

            if (line[i] != '(')
            {
                return false;
            }


            i++;


            for (int j = 0; j < varArray.Length; j++)
            {
                if (line[i] != varArray[j])
                {
                    return false;
                }
                i++;
            }



            if (line[i] != ')' || line[i + 1] != ';')
            {
                return false;
            }
            return true;

        }

        private bool checkStackTrace(string line)
        {
            int i = 0;
            string trimmed = line.Trim();

            if (!ExceptionVar.Equals(trimmed.Substring(0, ExceptionVar.Length)))
            {
                return false;
            }

            if (!trimmed.Substring(ExceptionVar.Length + 1, 15).Equals("printStackTrace"))
            {
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
                i++;
            }

            if (stack.Count > 0)
            {
                return false;
            }

            if (trimmed[trimmed.Length - 1] != ';')
            {
                return false;
            }

            return true;
        }

        private bool checkByteArray(string line)
        {
            string ar = "byte[]";
            string type = line.Substring(0, ar.Length);
            if (!ar.Equals(type))
            {
                return false;
            }
            int i = ar.Length;

            while (line[i] != '=')
            {
                if (i >= line.Length)
                {
                    return false;
                }

                ByteArrayVar += line[i];
                i++;
            }
            string nw = "new";
            while (line[i] != 'n')
            {
                i++;
            }
            if (!nw.Equals(line.Substring(i, 3)))
            {
                return false;
            }

            i += 3;
            while (line[i] != 'b') { i++; }
            string tmp = "byte";
            for (int j = 0; j < tmp.Length; j++)
            {
                if (line[i + j] != tmp[j])
                {
                    return false;
                }
            }

            Stack stack = new Stack();
            while (i < line.Length)
            {
                if (line[i] == '(')
                {
                    stack.Push('(');
                }

                if (line[i] == ')')
                {
                    stack.Pop();
                }
                i++;
            }

            if (line[line.Length - 1] != ';')
            {
                return false;
            }

            if (stack.Count > 0)
            {
                return false;
            }

            return true;
        }

        private bool checkFileClose(string line, string var)
        {
            var = var.Trim();
            if (!var.Equals(line.Substring(0, var.Length)))
            {
                return false;
            }

            string cls = "close";

            if (!cls.Equals(line.Substring(var.Length + 1, cls.Length)))
            {
                return false;
            }
            return true;
        }

        private bool checkString(string line)
        {
            line = line.Trim();
            line = Regex.Replace(line, @"\s+", "");
            string st = "String";
            if (!st.Equals(line.Substring(0, st.Length)))
            {
                return false;
            }
            int i = 0;
            while (line[i] != '=')
            {
                if (i >= line.Length) { return false; }
                i++;
            }

            if (!StringWriterVar.Equals(line.Substring(i + 1, StringWriterVar.Length)))
            {
                return false;
            }
            i += StringWriterVar.Length + 1;
            if (String.Compare(".toString();", line.Substring(i, 12)) != 0)
            {
                return false;
            }
            return true;
        }
    }

}
