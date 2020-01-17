using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tla_proj
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int totalErrors = 0;

        private void OpenDialogBox_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            if ( openFile.ShowDialog() == System.Windows.Forms.DialogResult.OK )
            {
                FilePath.Text = openFile.FileName;
            }
        }

     

        private void Start_Click(object sender, EventArgs e)
        {
            ErrorCheck errorCheck = new ErrorCheck(FilePath.Text , Errorlabel , errorTextBox);

            errorCheck.startScan();
        }
    }
}
