namespace tla_proj
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.Errorlabel = new System.Windows.Forms.Label();
            this.Start = new System.Windows.Forms.Button();
            this.FilePath = new System.Windows.Forms.TextBox();
            this.OpenDialogBox = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.NoErr = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MV Boli", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(9, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "File Path : ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("MV Boli", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 163);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(140, 18);
            this.label2.TabIndex = 2;
            this.label2.Text = "Total Error Found :";
            // 
            // Errorlabel
            // 
            this.Errorlabel.AutoSize = true;
            this.Errorlabel.ForeColor = System.Drawing.Color.Red;
            this.Errorlabel.Location = new System.Drawing.Point(146, 163);
            this.Errorlabel.Name = "Errorlabel";
            this.Errorlabel.Size = new System.Drawing.Size(16, 17);
            this.Errorlabel.TabIndex = 3;
            this.Errorlabel.Text = "0";
            // 
            // Start
            // 
            this.Start.Location = new System.Drawing.Point(146, 114);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(176, 32);
            this.Start.TabIndex = 4;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.Start_Click);
            // 
            // FilePath
            // 
            this.FilePath.Location = new System.Drawing.Point(81, 74);
            this.FilePath.Name = "FilePath";
            this.FilePath.Size = new System.Drawing.Size(341, 22);
            this.FilePath.TabIndex = 5;
            // 
            // OpenDialogBox
            // 
            this.OpenDialogBox.Location = new System.Drawing.Point(440, 73);
            this.OpenDialogBox.Name = "OpenDialogBox";
            this.OpenDialogBox.Size = new System.Drawing.Size(36, 28);
            this.OpenDialogBox.TabIndex = 6;
            this.OpenDialogBox.Text = "...";
            this.OpenDialogBox.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.OpenDialogBox.UseVisualStyleBackColor = true;
            this.OpenDialogBox.Click += new System.EventHandler(this.OpenDialogBox_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("MV Boli", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(111, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(305, 18);
            this.label4.TabIndex = 7;
            this.label4.Text = "Select Your Java File and then Press Start";
            // 
            // NoErr
            // 
            this.NoErr.AutoSize = true;
            this.NoErr.Font = new System.Drawing.Font("MV Boli", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NoErr.ForeColor = System.Drawing.Color.LimeGreen;
            this.NoErr.Location = new System.Drawing.Point(183, 201);
            this.NoErr.Name = "NoErr";
            this.NoErr.Size = new System.Drawing.Size(105, 18);
            this.NoErr.TabIndex = 9;
            this.NoErr.Text = "NO ERRORS !";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(488, 226);
            this.Controls.Add(this.NoErr);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.OpenDialogBox);
            this.Controls.Add(this.FilePath);
            this.Controls.Add(this.Start);
            this.Controls.Add(this.Errorlabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "JavaSyntaxErrorFinder";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.Button Start;
        public System.Windows.Forms.Button OpenDialogBox;
        public System.Windows.Forms.TextBox FilePath;
        public System.Windows.Forms.Label Errorlabel;
        private System.Windows.Forms.Label NoErr;
    }
}

